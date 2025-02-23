using MyApplicationMetroNSK.Data.Enums;

namespace MyApplicationMetroNSK.Data.Models;

public class TimeCard
{
    public string NumberTimeCard { get; set; } = string.Empty;
    public TimeSpan StartTime {  get; set; }
    public TimeSpan EndTime { get; set; }
    public WorkType WorkType { get; set; }
    public Enums.DayOfWeek DayofWeek { get; set; }
}
