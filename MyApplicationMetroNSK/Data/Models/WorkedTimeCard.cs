using MyApplicationMetroNSK.Data.Enums;

namespace MyApplicationMetroNSK.Data.Models;

public class WorkedTimeCard
{
    public int Id { get; set; }
    public string NumberTimeCard { get; set; } = string.Empty;
    public WorkType WorkType { get; set; }
    public Enums.DayOfWeek DayOfWeek { get; set; }
    public decimal WorkHours { get; set; }
    public decimal EveningHours { get; set; }
    public decimal NightHours { get; set; }
    public decimal ShiftGap { get; set; }
    public decimal HolidayHours { get; set; }
    public Month WorkDate { get; set; }

}
