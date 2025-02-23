using MyApplicationMetroNSK.Data.Enums;

namespace MyApplicationMetroNSK.Models;

public class ModelTimeCard
{
    public string NumberTimeCard { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public WorkType WorkType { get; set; }
    public Data.Enums.DayOfWeek DayofWeek { get; set; }
}
