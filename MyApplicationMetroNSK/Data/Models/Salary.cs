using System.ComponentModel.DataAnnotations;

namespace MyApplicationMetroNSK.Data.Models;

public class Salary
{
    public int Id { get; set; }
    //public DateTime MounthYear { get; set; }
    public decimal HourlyPayment { get; set; }
    public decimal HolidayPayment { get; set; }
    public decimal EveningPayment { get; set; }
    public decimal NightPayment { get; set; }
    public decimal PaymentForQualification { get; set; }
    public decimal LongServicePay { get; set; }
    public decimal PremiumPay { get; set; }
    public decimal TNPpremiymPay { get; set; }
    public decimal PaymentForGapInShift { get; set; }
    public decimal PaymentForTheRegionalCoefficient { get; set; }
    public decimal TotalSalary { get; set; }
    public decimal ProfessionalFees { get; set; }
    public decimal Taxes { get; set; }
    public decimal FinalSalary { get; set; }
    
}