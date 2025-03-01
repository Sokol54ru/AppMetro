using System.ComponentModel.DataAnnotations;

namespace MyApplicationMetroNSK.Data.Enums
{
    public enum Qualification
    {
        [Display(Name ="Без класса")]
        NoQualification = 1,

        [Display(Name = "Третий класс")]
        ThirdClassQualification,

        [Display(Name = "Второй класс")]
        SecondClassQualification,

        [Display(Name = "Первый класс")]
        FirstClassQualification
    }
}