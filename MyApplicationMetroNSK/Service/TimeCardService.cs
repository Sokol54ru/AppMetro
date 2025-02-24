using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Extensions;
using MyApplicationMetroNSK.ViewModels;
using MyApplicationMetroNSK.Data.Enums;

namespace MyApplicationMetroNSK.Service;

public class TimeCardService (IDbContextFactory<AppDbContext> dbContext, IMapper mapper) : ITimeCardService
{
    [HttpPost]
    public async Task<string?> SaveTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        await using var сontext = dbContext.CreateDbContext();
        var existTimeCard = await сontext.TimeCards.
            FirstOrDefaultAsync(tc =>
                tc.NumberTimeCard == workedTimeCard.NumberTimeCard &&
                tc.WorkType == workedTimeCard.WorkType &&
                tc.DayofWeek == workedTimeCard.DayOfWeek);
        
            if (existTimeCard != null) 
            {
                var workHours = CalculateWorkHours(existTimeCard.StartTime, existTimeCard.EndTime);
                var eveningHourse = CalculateEvningHours(existTimeCard.StartTime, existTimeCard.EndTime);
                var nightHourse = CalculateNightHours(existTimeCard.StartTime, existTimeCard.EndTime);
                
                
                var shiftGap = workHours;
                сontext.Add(new WorkedTimeCard
                {
                    NumberTimeCard = workedTimeCard.NumberTimeCard,
                    WorkType = workedTimeCard.WorkType,
                    DayOfWeek = workedTimeCard.DayOfWeek,
                    WorkHours = workHours,
                    EveningHours = eveningHourse,
                    NightHours = nightHourse,
                    ShiftGap = (workedTimeCard.WorkType == WorkType.Night || workedTimeCard.WorkType == WorkType.Morning) ? workHours : default,
                    WorkDate = workedTimeCard.WorkDate,
                });
                await сontext.SaveChangesAsync();

                return "save";
            }
            else
            {
                return null;
            }
    }

    public async Task<string?> SaveCustomTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        await using var сontext = dbContext.CreateDbContext();
        var workHours = CalculateWorkHours(workedTimeCard.StartTime, workedTimeCard.EndTime);
        var eveningHourse = CalculateEvningHours(workedTimeCard.StartTime, workedTimeCard.EndTime);
        var nightHourse = CalculateNightHours(workedTimeCard.StartTime, workedTimeCard.EndTime);
        if (workedTimeCard.WorkType == WorkType.Night)
        {
            var shiftGap = workHours;
            сontext.Add(new WorkedTimeCard
            {
                NumberTimeCard = workedTimeCard.NumberTimeCard,
                WorkType = workedTimeCard.WorkType,
                DayOfWeek = workedTimeCard.DayOfWeek,
                WorkHours = workHours,
                EveningHours = eveningHourse,
                NightHours = nightHourse,
                ShiftGap = shiftGap,
                WorkDate = workedTimeCard.WorkDate,
            });
            await сontext.SaveChangesAsync();
        }
        return "save";
    }

    public async Task<List<ModelWorkedTimeCard>> GetAllTimeCard()
    {
        await using var context = dbContext.CreateDbContext();
        var workedTimeCard =  await context.WorkedTimeCards.ToListAsync();
        return mapper.Map<List<ModelWorkedTimeCard>>(workedTimeCard);
    }
    [HttpPost]
    public async Task<string?> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        await using var сontext = dbContext.CreateDbContext();
        var existTimeCard = await сontext.WorkedTimeCards.
            FirstOrDefaultAsync(wtc =>
            wtc.NumberTimeCard == workedTimeCard.NumberTimeCard &&
            wtc.DayOfWeek == workedTimeCard.DayOfWeek &&
            wtc.WorkType == workedTimeCard.WorkType &&
            wtc.WorkDate == workedTimeCard.WorkDate);

        if (existTimeCard != null) 
        {
            сontext.WorkedTimeCards.Remove(existTimeCard);
            await сontext.SaveChangesAsync();
            return "маршрут удалён";
        }
        return null;
    }

    public async Task<List<ModelWorkedTimeCard>> GetTimeCardsForTheSelectedMonth(ModelWorkedTimeCard workedTimeCard)
    {
        await using var context = dbContext.CreateDbContext();
        var workedTimeCards = await context.WorkedTimeCards.
            Where(wtc =>
            wtc.WorkDate == workedTimeCard.WorkDate).ToListAsync();
        return mapper.Map<List<ModelWorkedTimeCard>>(workedTimeCards);
    }

    //public async Task<List<ModelSalary>> GetSalaryForTheSelectedMonth(ModelSalary modelSalary)
    //{
    //    await using var context = dbContext.CreateDbContext()
    //}

    private decimal CalculateWorkHours(TimeSpan startTime, TimeSpan endTime)
    {
        decimal workHours;
        if (endTime < startTime) 
        { 
            workHours =(decimal)(endTime.Add(new TimeSpan(1, 0, 0, 0)) - startTime).TotalHours;
        }
        else
        {
            workHours = (decimal)(endTime - startTime).TotalHours;
        }
        return Math.Round(workHours, 1);
    }

    private decimal CalculateEvningHours(TimeSpan startTime, TimeSpan endTime)
    {
        decimal evningHours = 0;
        TimeSpan eveningStart = new TimeSpan(16, 0, 0);  // Начало вечера - 16:00
        TimeSpan eveningEnd = new TimeSpan(22, 0, 0);    // Конец вечера - 22:00

        // Если смена не пересекает полночь (обычный случай)
        if (startTime < endTime)
        {
            evningHours = CalculateOverlap(startTime, endTime, eveningStart, eveningEnd);
        }
        else // Если смена пересекает полночь
        {
            // Рассчитываем вечерние часы до полуночи (с 16:00 до 22:00)
            evningHours += CalculateOverlap(startTime, new TimeSpan(23, 59, 59), eveningStart, eveningEnd);

            // Рассчитываем вечерние часы на следующий день, если они есть
            evningHours += CalculateOverlap(TimeSpan.Zero, endTime, eveningStart, eveningEnd);
        }

        return Math.Round(evningHours, 1);
    }

    private decimal CalculateNightHours(TimeSpan startTime, TimeSpan endTime)
    {
        TimeSpan nightStart = new TimeSpan(22, 0, 0);
        TimeSpan nightEnd = new TimeSpan(6, 0, 0);

        decimal nightHours = default;

        if (endTime < startTime) // Если рабочая смена пересекает полночь
        {
            nightHours += CalculateHoursInRange(startTime, new TimeSpan(23, 59, 59), nightStart, new TimeSpan(23, 59, 59));
            nightHours += CalculateHoursInRange(TimeSpan.Zero, endTime, TimeSpan.Zero, nightEnd);
        }
        else
        {
            nightHours += CalculateHoursInRange(startTime, endTime, nightStart, nightEnd);
        }

        return Math.Round(nightHours, 1);
    }
    private decimal CalculateHoursInRange(TimeSpan startTime, TimeSpan endTime, TimeSpan rangeStart, TimeSpan rangeEnd)
    {
        decimal totalHours = default;

        // Если диапазон НЕ пересекает полночь (обычный случай)
        if (rangeStart <= rangeEnd)
        {
            totalHours = CalculateOverlap(startTime, endTime, rangeStart, rangeEnd);
        }
        else // Если диапазон пересекает полночь (например, 22:00 - 06:00)
        {
            // Считаем часы ДО полуночи (например, 22:00 - 00:00)
            totalHours += CalculateOverlap(startTime, endTime, rangeStart, new TimeSpan(23, 59, 59));

            // Считаем часы ПОСЛЕ полуночи (например, 00:00 - 06:00)
            totalHours += CalculateOverlap(startTime, endTime, TimeSpan.Zero, rangeEnd);
        }

        return Math.Round(totalHours, 1);
    }

    private decimal CalculateOverlap(TimeSpan startTime, TimeSpan endTime, TimeSpan rangeStart, TimeSpan rangeEnd)
    {
        // Вычисление эффективного начала и конца
        TimeSpan effectiveStart = new TimeSpan(Math.Max(startTime.Ticks, rangeStart.Ticks));
        TimeSpan effectiveEnd = new TimeSpan(Math.Min(endTime.Ticks, rangeEnd.Ticks));

        // Проверка на пересечение
        if (effectiveStart < effectiveEnd)
        {
            // Возвращаем пересечение в часах, приводим к decimal
            return (decimal)(effectiveEnd - effectiveStart).TotalHours;
        }

        // Если нет пересечения, возвращаем 0
        return 0;
    }
}