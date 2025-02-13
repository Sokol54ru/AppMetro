using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApplicationMetroNSK.Data;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Controllers;

public class SalaryController(IMapper mapper) : Controller
{
    public IActionResult Index(ModelDataForCalculation dataForCalculation)
    {
        return View(mapper.Map<ModelDataForCalculation> (dataForCalculation));
    }
}
