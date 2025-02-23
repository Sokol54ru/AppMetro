using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.Data.Enums;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Service;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Controllers;

public class SalaryController(ISalaryCalculationService salaryCalculation, IDbContextFactory<AppDbContext> dbContext, IMapper mapper) : Controller
{
    public async Task<IActionResult> Index(ModelDataForCalculation dataForCalculation)
    {
        return View(await salaryCalculation.GetAllCoefficient());
    }

    [HttpGet]
    public async Task<IActionResult> GetSalary(Month month)
    {
        //await using var context = dbContext.CreateDbContext();
        //var calculateSalary = await salaryCalculation.CalculatedSalary(month);
        //var modelSalary = await context.Salary.ToListAsync();
        //return View(mapper.Map<List<ModelSalary>>(modelSalary));
        var salaries = await salaryCalculation.CalculatedSalary(month);
        ViewBag.SelectedMonth = month; // Передаем выбранный месяц
        return View(salaries);
    }

    //[HttpGet]
    //public async Task<IActionResult> CalculateSalary(int month)
    //{
    //    var result = await salaryCalculation.CalculatedSalary((Month)month);
    //    return View(result);
    //}
 
}
