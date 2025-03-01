using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApplicationMetroNSK.Data.Enums;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Service;

namespace MyApplicationMetroNSK.Controllers;

public class TimeCardController(ITimeCardService timeCardService) : Controller 
{
    [HttpPost]
    public async Task<IActionResult> GetTimeCardsForTheSelectedMonth(ModelWorkedTimeCard workedTimeCard)
    {
        var timeCards = await timeCardService.GetTimeCardsForTheSelectedMonth(workedTimeCard);
        return View(timeCards);
    }

    [HttpPost]
    public async Task<IActionResult> SaveTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        var result = await timeCardService.SaveTimeCard(workedTimeCard);
        if(result == null)
        {
            TempData["ErrorMessage"] = "Маршрут не найден!";
            return RedirectToAction("Main", "View");
        }
        TempData["SuccessMessage"] = "Маршрут успешно добавлен!";
        return RedirectToAction("Main", "View");
    }

    [HttpPost]
    public async Task<IActionResult> SaveCustomTimeCard(ModelWorkedTimeCard timeCard)
    {
        var result = await timeCardService.SaveCustomTimeCard(timeCard);
        TempData["SuccessMessage"] = "Маршрут успешно добавлен!";
        return RedirectToAction("Main", "View");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        var result = await timeCardService.DeleteTimeCard(workedTimeCard);
        if (result == null)
        {
            TempData["ErrorMessage"] = "Маршрут не найден!";
            return RedirectToAction("Main", "View");
        }
        TempData["SuccessMessage"] = "Маршрут успешно удалён!";
        return RedirectToAction("Main", "View");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteTimeCardsForMonth(Month month)
    {
        var result = await timeCardService.DeleteTimeCardsForMonth(month);
        if (result == null)
        {
            TempData["ErrorMessage"] = "Месяц не найден!";
            return RedirectToAction("Main", "View");
        }
        TempData["SuccessMessage"] = "Месяц очищен!";
        return RedirectToAction("Main", "View");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAllTimeCards()
    {
        var result = await timeCardService.DeleteAllTimeCards();
        if (result == null)
        {
            TempData["ErrorMessage"] = "Месяц не найден!";
            return RedirectToAction("Main", "View");
        }
        TempData["SuccessMessage"] = "Месяц очищен!";
        return RedirectToAction("Main", "View");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}