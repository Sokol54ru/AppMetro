using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.Data.Enums;

namespace MyApplicationMetroNSK.Service;

public interface ITimeCardService
{
    Task<List<ModelWorkedTimeCard>> GetAllTimeCard();
    Task<List<ModelWorkedTimeCard>> GetTimeCardsForTheSelectedMonth(ModelWorkedTimeCard workedTimeCard);
    Task<string?> SaveTimeCard(ModelWorkedTimeCard workedTimeCard);
    Task<string?> SaveCustomTimeCard(ModelWorkedTimeCard timeCard);
    Task<string?> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard);
    Task<string?> DeleteTimeCardsForMonth(Month month);
    Task<string?> DeleteAllTimeCards();
}