namespace MyApplicationMetroNSK.ViewModels;

public class ModelSalary
{
    decimal HourlyPayment { get; set; }
    decimal HolidayPayment { get; set; }
    decimal EveningPayment { get; set; }
    decimal NightPayment { get; set; }
    decimal PaymentForQualification { get; set; }
    decimal LongServicePay { get; set; }
    decimal PremiumPay { get; set; }
    decimal PaymentForGapInShift { get; set; }
    decimal PaymentForTheRegionalCoefficient { get; set; }
    decimal ProfessionalFees { get; set; }
    decimal Taxes { get; set; }
    decimal TotalSalary { get; set; }
}
