using System.ComponentModel.DataAnnotations;

namespace MyApplicationMetroNSK.Data.Enums;

public enum DayOfWeek
{
    [Display(Name = "рабочий день")]
    WorkDay = 1,

    [Display(Name = "пятница")]
    Friday,

    [Display(Name = "суббота")]
    Saturday,

    [Display(Name = "воскресенье")]
    Sunday
}
