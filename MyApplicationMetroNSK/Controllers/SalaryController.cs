using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyApplicationMetroNSK.Data;
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


    public async Task<IActionResult> GetSalary()
    {
        await using var context = dbContext.CreateDbContext();
        var modelSalary = await context.Salary.ToListAsync();
        return View(mapper.Map<List<ModelSalary>>(modelSalary));
    }
}
