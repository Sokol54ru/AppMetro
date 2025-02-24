using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            ModelState.AddModelError("", $"Маршрут не найден, повтори ввод");
            return View("AddTimeCard", new ModelWorkedTimeCard { });
        }
        TempData["SuccessMessage"] = "Маршрут успешно добавлен!";
        return RedirectToAction("Index", "View");
    }

    [HttpPost]
    public async Task<IActionResult> SaveCustomTimeCard(ModelWorkedTimeCard timeCard)
    {
        var result = await timeCardService.SaveCustomTimeCard(timeCard);
        TempData["SuccessMessage"] = "Маршрут успешно добавлен!";
        return RedirectToAction("Index", "View");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        var result = await timeCardService.DeleteTimeCard(workedTimeCard);
        if (result == null)
        {
            ModelState.AddModelError("", $"Маршрут не найден");
            return View("DeleteTimeCardView", workedTimeCard);
        }
        TempData["SuccessMessage"] = "Маршрут успешно удалён!";
        return RedirectToAction("Index", "View");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}