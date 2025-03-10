using MyApplicationMetroNSK.Models;

namespace MyApplicationMetroNSK.ViewModels
{
    public class CombinedViewModel
    {
        public List<ModelSalary> Salaries { get; set; } = new ();
        public List<ModelWorkedTimeCard> WorkedTimeCards { get; set;} = new ();
        public int SelectedMonth { get; set; }
    }
}
