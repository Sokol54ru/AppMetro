using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.Data.Models;

namespace MyApplicationMetroNSK.Service;

public interface ITimeCardService
{
    Task<List<ModelWorkedTimeCard>> GetAllTimeCard();
    Task<List<ModelWorkedTimeCard>> GetTimeCardsForTheSelectedMonth(ModelWorkedTimeCard workedTimeCard);
    Task<string?> SaveTimeCard(ModelWorkedTimeCard workedTimeCard);
    Task<string?> SaveCustomTimeCard(ModelWorkedTimeCard timeCard);
    Task<string?> DeleteTimeCard(ModelWorkedTimeCard workedTimeCard);
}
