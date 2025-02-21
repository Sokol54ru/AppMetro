using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.Service;

public interface ISalaryCalculationService
{
    Task<List<ModelDataForCalculation>> GetAllCoefficient();
    Task<List<ModelSalary>> GetSalary();
}
