using Microsoft.AspNetCore.Mvc;
using MyApplicationMetroNSK.Data.Enums;
using MyApplicationMetroNSK.Service;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Controllers;

public class SalaryController(ISalaryCalculationService salaryCalculation) : Controller
{
    public async Task<IActionResult> GetCoefficient(ModelDataForCalculation dataForCalculation)
    {
        return View(await salaryCalculation.GetAllCoefficient());
    }

    [HttpGet]
    public async Task<IActionResult> GetSalary(Month month)
    {
        var salaries = await salaryCalculation.CalculatedSalary(month);
        ViewBag.SelectedMonth = month;
        return View(salaries);
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkedTimeCardForTheSelectedMonth(int month)
    {
        var viewModel = await salaryCalculation.GetSalaryAndWorkHoursForMonth(month);
        return View(viewModel);
    }
}