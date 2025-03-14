﻿using MyApplicationMetroNSK.Data.Enums;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Service;

public interface ISalaryCalculationService
{
    Task<List<ModelDataForCalculation>> GetAllCoefficient();
    Task<List<ModelSalary>> CalculatedSalary(Month month);
    Task<CombinedViewModel> GetSalaryAndWorkHoursForMonth(int month);

}