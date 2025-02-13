namespace MyApplicationMetroNSK.Data.Models;

public class TimeCard
{
    public string NumberTimeCard { get; set; } = string.Empty;
    public TimeSpan StartTime {  get; set; }
    public TimeSpan EndTime { get; set; }
    public WorkType WorkType { get; set; }
    public DayOfWeek DayofWeek { get; set; }
}
