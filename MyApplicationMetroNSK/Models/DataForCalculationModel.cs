using MyApplicationMetroNSK.Data.Models;

namespace MyApplicationMetroNSK.Models;

public record DataForCalculationModel(DataForCalculation? Coefficient, List<WorkedTimeCard>? TimeCards);
