using AutoMapper;
using MyApplicationMetroNSK.Data.Models;
using MyApplicationMetroNSK.Models;
using MyApplicationMetroNSK.ViewModels;

namespace MyApplicationMetroNSK.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<TimeCard, ModelTimeCard>();
        CreateMap<WorkedTimeCard, ModelWorkedTimeCard>();
        CreateMap<Salary, ModelSalary>();
        CreateMap<DataForCalculation, ModelDataForCalculation>();
    }
}