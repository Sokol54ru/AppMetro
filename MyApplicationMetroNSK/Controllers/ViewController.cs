﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Service;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Controllers;

public class ViewController(IMapper mapper, ITimeCardService timeCardService) : Controller
{
    public IActionResult Main()
    {
        return View();
    }

    public IActionResult Viewing()
    {
        return View();
    }

    public IActionResult Addition()
    {
        return View();
    }
    public IActionResult Deletion()
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
    public IActionResult ViewChangeCoefficients(ModelDataForCalculation modelData)
    {
        return View(mapper.Map<ModelDataForCalculation>(modelData));
    }

    public IActionResult DeleteTimeCardView(ModelWorkedTimeCard workedTimeCard)
    {
        return View(mapper.Map<ModelWorkedTimeCard>(workedTimeCard));
    }

    public IActionResult DeleteTimeCardsForMonthView(ModelWorkedTimeCard workedTimeCard)
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