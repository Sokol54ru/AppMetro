using MyApplicationMetroNSK.Data.Models;

namespace MyApplicationMetroNSK.Models;

public class ModelTimeCard
{
    public string NumberTimeCard { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public WorkType WorkType { get; set; }
    public Data.Models.DayOfWeek DayofWeek { get; set; }
}
