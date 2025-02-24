using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.Data.Enums;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.Models;
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

    public async Task<List<ModelSalary>> CalculatedSalary(Month month)  // посчитанная зарплата(норм обозвать)
    {
        await using var context = dbContext.CreateDbContext();

        var dataForCalculation = await GetDataForSalary(context, month);

        var hourlyPayment = CalculateWorkHours(dataForCalculation);

        var eveningPayment = CalculateEveningHours(dataForCalculation);

        var nightPayment = CalculateNightHours(dataForCalculation);

        var paymentForGapInShift = CalculateForGapInShift(dataForCalculation);

        var holydayPayment = CalculateHolydayHours(dataForCalculation);

        decimal premiumTNP = CalculatePremiumTNP(hourlyPayment, dataForCalculation.Coefficient!);

        decimal paymentForQualification = CalculateForQualification(hourlyPayment, dataForCalculation.Coefficient!);

        decimal longServicePay = CalculateLongServicePay(hourlyPayment, dataForCalculation.Coefficient!);

        decimal premiumPay = CalculatePremiumPay(hourlyPayment, eveningPayment, nightPayment, paymentForQualification, paymentForGapInShift, dataForCalculation.Coefficient!);

        decimal paymentForTheRegionalCoefficient = CalculateForRegionalCoefficient(hourlyPayment, eveningPayment, nightPayment,
                                                                                     paymentForQualification, paymentForGapInShift, holydayPayment,
                                                                                     longServicePay, premiumPay, dataForCalculation.Coefficient!);

        decimal totalSalary = CalculateTotalSalary(hourlyPayment, eveningPayment, nightPayment,
                                         paymentForGapInShift, holydayPayment, paymentForQualification,
                                         longServicePay, premiumPay, premiumTNP, paymentForTheRegionalCoefficient);

        decimal taxes = CalculateTaxes(totalSalary, dataForCalculation.Coefficient!);

        decimal professionalFees = CalculateProfessionalFees(totalSalary, dataForCalculation.Coefficient!);

        decimal finalSalary = CalculateFinalSalary(totalSalary, taxes, professionalFees);

        var existingSalary = await context.Salary.FirstOrDefaultAsync();

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

    private decimal CalculateWorkHours(DataForCalculationModel data) => 
        Math.Round(data.TimeCards!.Select(x => x.WorkHours).Sum() * data.Coefficient!.TariffRate, 2);
    

    private decimal CalculateEveningHours(DataForCalculationModel data) =>  
        Math.Round(data.TimeCards!.Select(x => x.EveningHours).Sum() * data.Coefficient!.TariffRate * data.Coefficient.CoefficientEvening, 2);
    

    private decimal CalculateNightHours(DataForCalculationModel data) =>
        Math.Round(data.TimeCards!.Select(x => x.NightHours).Sum() * data.Coefficient!.TariffRate * data.Coefficient.CoefficientNight, 2);
    

    private decimal CalculateHolydayHours(DataForCalculationModel data) =>
        Math.Round(data.TimeCards!.Select(x => x.HolidayHours).Sum() * data.Coefficient!.TariffRate, 2);

    private decimal CalculateForGapInShift(DataForCalculationModel data) =>
        Math.Round(data.TimeCards!.Select(x => x.ShiftGap).Sum() * data.Coefficient!.TariffRate * data.Coefficient.CoefficientEvening, 2);

    private decimal CalculatePremiumTNP(decimal hourlyPayment , DataForCalculation coefficient) =>
        Math.Round(hourlyPayment * coefficient.BonusTNP, 2);
    
    private decimal CalculateForQualification(decimal hourlyPayment, DataForCalculation coefficient) =>
        Math.Round(hourlyPayment * coefficient.Qualification, 2);

    private decimal CalculateLongServicePay(decimal hourlyPayment, DataForCalculation coefficient) =>
        Math.Round(hourlyPayment * coefficient.LengthOfService, 2);

    private decimal CalculatePremiumPay(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment, 
                                          decimal paymentForQualification, decimal paymentForGapInShift,
                                          DataForCalculation coefficient) =>
        Math.Round((hourlyPayment + eveningPayment + nightPayment + paymentForQualification + paymentForGapInShift) * coefficient.Bonus, 2);
    

    private decimal CalculateForRegionalCoefficient(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment,
                                                      decimal paymentForQualification, decimal paymentForGapInShift, decimal holydayPayment,
                                                      decimal longServicePay, decimal premiumPay, DataForCalculation coefficient) =>
        Math.Round((hourlyPayment + eveningPayment + nightPayment +
                    paymentForQualification + paymentForGapInShift + holydayPayment +
                    longServicePay + premiumPay) * coefficient.RegionalCoefficient, 2);
    

    private decimal CalculateTotalSalary(decimal hourlyPayment, decimal eveningPayment, decimal nightPayment, 
                                         decimal paymentForGapInShift, decimal holydayPayment, decimal paymentForQualification,
                                         decimal longServicePay, decimal premiumPay, decimal premiumTNP, decimal paymentForTheRegionalCoefficient) =>
        Math.Round(hourlyPayment + eveningPayment + nightPayment +
                   paymentForGapInShift + holydayPayment + paymentForQualification +
                   longServicePay + premiumPay + premiumTNP + paymentForTheRegionalCoefficient, 2);
    

    private decimal CalculateTaxes(decimal totalSalary, DataForCalculation coefficient) =>
        Math.Round(totalSalary * coefficient.Taxes, 2);
    

    private decimal CalculateProfessionalFees(decimal totalSalary, DataForCalculation coefficient) =>
        Math.Round(totalSalary * coefficient.ProfessionalFees, 2);
    

    private decimal CalculateFinalSalary(decimal totalSalary, decimal taxes, decimal professionalFees) =>
        Math.Round(totalSalary - taxes - professionalFees, 2);
    
    private async Task<DataForCalculationModel> GetDataForSalary(AppDbContext dbContext, Month date)
    {
        var coefficient = await dbContext.DataForCalculation.AsNoTracking().FirstOrDefaultAsync();
        
        var timeCards = await dbContext.WorkedTimeCards
            .AsNoTracking()
            .Where(x => x.WorkDate == date)
            .ToListAsync();

        return new DataForCalculationModel(coefficient, timeCards);
    }
}