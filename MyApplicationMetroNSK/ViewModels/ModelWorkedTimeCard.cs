using MyApplicationMetroNSK.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApplicationMetroNSK.Models;

public class ModelWorkedTimeCard
{
    public int Id { get; set; }
    public string NumberTimeCard { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public WorkType WorkType { get; set; }
    public Data.Models.DayOfWeek DayOfWeek { get; set; }
    public decimal WorkHours { get; set; }
    public decimal EveningHours { get; set; }
    public decimal NightHours { get; set; }
    public decimal ShiftGap { get; set; }
    public decimal HolidayHours { get; set; }
    public Month WorkDate { get; set; }
}
