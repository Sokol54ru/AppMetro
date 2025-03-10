using System.Diagnostics;
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
        return HandleServiceResult(result, "Маршрут успешно добавлен!", "Маршрут не найден!");
    }

    [HttpPost]
    public async Task<IActionResult> SaveCustomTimeCard(ModelWorkedTimeCard timeCard)
    {
        var result = await timeCardService.SaveCustomTimeCard(timeCard);
        return HandleServiceResult(result, "Маршрут успешно добавлен!", "Маршрут не добавлен!");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard)
    {
        var result = await timeCardService.DeleteTimeCard(workedTimeCard);
        return HandleServiceResult(result, "Маршрут успешно удалён!", "Маршрут не найден!");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteTimeCardsForMonth(Month month)
    {
        var result = await timeCardService.DeleteTimeCardsForMonth(month);
        return HandleServiceResult(result, "Месяц очищен!", "Месяц не найден!");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAllTimeCards()
    {
        var result = await timeCardService.DeleteAllTimeCards();
        return HandleServiceResult(result, "Маршруты удалены!", "Маршруты не найдены!");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult HandleServiceResult(object? result, string successMessage, string errorMessage)
    {
        if (result == null)
        {
            TempData["ErrorMessage"] = errorMessage;
        }
        else
        {
            TempData["SuccessMessage"] = successMessage;
        }
        return RedirectToAction("Main", "View");
    }
}