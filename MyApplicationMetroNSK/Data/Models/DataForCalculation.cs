namespace MyApplicationMetroNSK.Data.Models;

public class DataForCalculation
{
    public int Id { get; set; }
    public decimal TariffRate { get; set; }
    public decimal CoefficientEvening { get; set; }
    public decimal CoefficientNight { get; set; }
    public decimal BonusTNP { get; set; }
    public decimal Bonus { get; set; }
    public decimal RegionalCoefficient { get; set; }
    public decimal LengthOfService { get; set; }
    public decimal Qualification { get; set; }
    public decimal Taxes { get; set; }
    public decimal ProfessionalFees { get; set; }
}
