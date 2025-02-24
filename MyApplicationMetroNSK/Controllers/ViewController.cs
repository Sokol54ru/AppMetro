using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Service;

namespace MyApplicationMetroNSK.Controllers;

public class ViewController(IMapper mapper, ITimeCardService timeCardService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AddTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        return View(mapper.Map<ModelWorkedTimeCard>(workedTimeCard));
    }

    public IActionResult AddCustomTimeCard(ModelWorkedTimeCard timeCard)
    {
        return View(mapper.Map<ModelWorkedTimeCard>(timeCard));
    }

    public async Task<IActionResult> GetAllTimeCard()
    {
        return View(await timeCardService.GetAllTimeCard());
    }

    public IActionResult ViewGetTimeCardsForTheSelectedMonth()
    {
        return View();
    }


    public IActionResult DeleteTimeCardView(ModelWorkedTimeCard workedTimeCard)
    {
        return View(mapper.Map<ModelWorkedTimeCard>(workedTimeCard));
    }

    public IActionResult ViewForTheSelectedMonth()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}