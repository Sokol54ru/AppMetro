using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Service;

public class SalaryCalculationService(IDbContextFactory<AppDbContext> dbContext, IMapper mapper) : ISalaryCalculationService
{
    public async Task<List<ModelDataForCalculation>> GetAllCoefficient()
    {
        await using var context = dbContext.CreateDbContext();
        var coefficient = await context.DataForCalculation.ToListAsync();
        return mapper.Map<List<ModelDataForCalculation>>(coefficient);
    }

    public async Task<List<ModelSalary>> GetSalary()
    {
        await using var context = dbContext.CreateDbContext();

        var coefficient = await GetCoefficient(context);

        decimal hourlyPayment = await CalculateWorkHours(context, coefficient);

        decimal eveningPayment = await CalculateEveningHours(context, coefficient);

        decimal nightPayment = await CalculateNightHours(context, coefficient);

        decimal paymentForGapInShift = await CalculateForGapInShift(context, coefficient);

        decimal holydayPayment = await CalculateHolydayHours(context, coefficient);

        decimal premiumTNP = CalculatePremiumTNP(hourlyPayment, coefficient);

        decimal paymentForQualification = CalculateForQualification(hourlyPayment, coefficient);

        decimal longServicePay = CalculateLongServicePay(hourlyPayment, coefficient);

        decimal premiumPay = CalculatePremiumPay(hourlyPayment, eveningPayment, nightPayment, paymentForQualification, paymentForGapInShift, coefficient);

        decimal paymentForTheRegionalCoefficient = CalculateForRegionalCoefficient(hourlyPayment, eveningPayment, nightPayment,
                                                                                     paymentForQualification, paymentForGapInShift, holydayPayment,
                                                                                     longServicePay, premiumPay, coefficient);

        decimal totalSalary = CalculateTotalSalary(hourlyPayment, eveningPayment, nightPayment,
                                         paymentForGapInShift, holydayPayment, paymentForQualification,
                                         longServicePay, premiumPay, premiumTNP, paymentForTheRegionalCoefficient);

        decimal taxes = CalculateTaxes(totalSalary, coefficient);

        decimal professionalFees = CalculateProfessionalFees(totalSalary, coefficient);

        decimal finalSalary = CalculateFinalSalary(totalSalary, taxes, professionalFees);




        var existingSalary = await context.Salary.FirstOrDefaultAsync(s => s.Id == 1);

        if (existingSalary != null)
        {
            // Обновляем существующую запись
            existingSalary.HourlyPayment = hourlyPayment;
            existingSalary.EveningPayment = eveningPayment;
            existingSalary.NightPayment = nightPayment;
            existingSalary.PaymentForGapInShift = paymentForGapInShift;
            existingSalary.HolidayPayment = holydayPayment;
            existingSalary.TNPpremiymPay = premiumTNP;
            existingSalary.PaymentForQualification = paymentForQualification;
            existingSalary.LongServicePay = longServicePay;
            existingSalary.PremiumPay = premiumPay;
            existingSalary.PaymentForTheRegionalCoefficient = paymentForTheRegionalCoefficient;
            existingSalary.TotalSalary = totalSalary;
            existingSalary.Taxes = taxes;
            existingSalary.ProfessionalFees = professionalFees;
            existingSalary.FinalSalary = finalSalary;
        }
        else
        {
            // Добавляем новую запись
            var newSalary = new Salary
            {
                HourlyPayment = hourlyPayment,
                EveningPayment = eveningPayment,
                NightPayment = nightPayment,
                PaymentForGapInShift = paymentForGapInShift,
                HolidayPayment = holydayPayment,
                TNPpremiymPay = premiumTNP,
                PaymentForQualification = paymentForQualification,
                LongServicePay = longServicePay,
                PremiumPay = premiumPay,
                PaymentForTheRegionalCoefficient = paymentForTheRegionalCoefficient,
                TotalSalary = totalSalary,
                Taxes = taxes,
                ProfessionalFees = professionalFees
            };

            await context.Salary.AddAsync(newSalary);
        }

        await context.SaveChangesAsync();



        var salary = await context.Salary.ToListAsync();
        return mapper.Map<List<ModelSalary>>(salary);
    }

    private async Task<decimal> CalculateWorkHours(AppDbContext context, DataForCalculation coefficient)
    {
        return Math.Round(await context.WorkedTimeCards.SumAsync(w => w.WorkHours)
            * coefficient.TariffRate, 2);
    }

    private async Task<decimal> CalculateEveningHours(AppDbContext context, DataForCalculation coefficient)
    {
        return Math.Round(await context.WorkedTimeCards.SumAsync(w => w.EveningHours)
             * coefficient.TariffRate * coefficient.CoefficientEvening, 2);
    }

    private async Task<decimal> CalculateNightHours(AppDbContext context, DataForCalculation coefficient)
    {
        return Math.Round(await context.WorkedTimeCards.SumAsync(w => w.NightHours)
            * coefficient.TariffRate * coefficient.CoefficientNight, 2);
    }

    private async Task<decimal> CalculateHolydayHours(AppDbContext context, DataForCalculation coefficient)
    {
        return Math.Round(await context.WorkedTimeCards.SumAsync(w => w.HolidayHours)
            * coefficient.TariffRate, 2);
    }

    private async Task<decimal> CalculateForGapInShift(AppDbContext context, DataForCalculation coefficient)
    {
        return Math.Round(await context.WorkedTimeCards.SumAsync(w => w.ShiftGap)
            *  coefficient.TariffRate * coefficient.CoefficientEvening, 2);
    }

    private decimal CalculatePremiumTNP(decimal hourlyPayment , DataForCalculation coefficient)
    {
        return Math.Round(hourlyPayment * coefficient.BonusTNP, 2);
    }
    private decimal CalculateForQualification(decimal hourlyPayment, DataForCalculation coefficient)
    {
        return Math.Round(hourlyPayment * coefficient.Qualification, 2);
    }

    private decimal CalculateLongServicePay(decimal hourlyPayment, DataForCalculation coefficient)
    {
        return Math.Round(hourlyPayment * coefficient.LengthOfService, 2);
    }

    private decimal CalculatePremiumPay(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment, 
                                          decimal paymentForQualification, decimal paymentForGapInShift,
                                          DataForCalculation coefficient)
    {
        return Math.Round((hourlyPayment + eveningPayment + nightPayment + paymentForQualification + paymentForGapInShift) * coefficient.Bonus, 2);
    }

    private decimal CalculateForRegionalCoefficient(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment,
                                                      decimal paymentForQualification, decimal paymentForGapInShift, decimal holydayPayment,
                                                      decimal longServicePay, decimal premiumPay, DataForCalculation coefficient)
    {
        return Math.Round((hourlyPayment + eveningPayment + nightPayment +
                           paymentForQualification + paymentForGapInShift + holydayPayment +
                           longServicePay + premiumPay) * coefficient.RegionalCoefficient, 2);
    }

    private decimal CalculateTotalSalary(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment, 
                                         decimal paymentForGapInShift, decimal holydayPayment, decimal paymentForQualification,
                                         decimal longServicePay, decimal premiumPay, decimal premiumTNP, decimal paymentForTheRegionalCoefficient)
    {
        return Math.Round(hourlyPayment + eveningPayment + nightPayment +
                          paymentForGapInShift + holydayPayment + paymentForQualification +
                          longServicePay + premiumPay + premiumTNP + paymentForTheRegionalCoefficient, 2);
    }

    private decimal CalculateTaxes(decimal totalSalary, DataForCalculation coefficient)
    {
        return Math.Round(totalSalary * coefficient.Taxes, 2);
    }

    private decimal CalculateProfessionalFees(decimal totalSalary, DataForCalculation coefficient)
    {
        return Math.Round(totalSalary * coefficient.ProfessionalFees, 2);
    }

    private decimal CalculateFinalSalary(decimal totalSalary, decimal taxes, decimal professionalFees)
    {
        return Math.Round(totalSalary - taxes - professionalFees, 2);
    }

    private async Task<DataForCalculation> GetCoefficient (AppDbContext context)
    {
        var coefficient = await context.DataForCalculation.AsNoTracking().FirstOrDefaultAsync();
        if (coefficient == null)
            throw new InvalidOperationException("Коэффициенты не найдены в базе данных.");

        return coefficient;
    }
    
}
