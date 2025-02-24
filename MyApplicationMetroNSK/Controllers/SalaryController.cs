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
        var salaries = await salaryCalculation.CalculatedSalary(month);
        ViewBag.SelectedMonth = month;
        return View(salaries);
    }
}