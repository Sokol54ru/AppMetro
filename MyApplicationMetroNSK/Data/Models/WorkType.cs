using System.ComponentModel.DataAnnotations;

namespace MyApplicationMetroNSK.Data.Models;

public enum WorkType
{
    [Display(Name = "в день")]
    Day = 1, 

    [Display(Name = "без ночи")]
    NoNight, 

    [Display(Name = "в ночь")]
    Night, 

    [Display(Name = "с ночи")]
    Morning
}
