using RentalPropertyAnalyzer.Models;

namespace RentalPropertyAnalyzer.Models.ViewModels
{
    public class PropertyForecastViewModel
    {
        public List<SavedProperties> SavedProperties { get; set; } = new();
        public int ZipID { get; set; }
        public string? Address { get; set; }
        public string? ImgSrc { get; set; }
        public decimal Price { get; set; }
        public decimal EstimatedRent { get; set; }

        public decimal DownpaymentPercentage { get; set; }
        public decimal DownpaymentAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TermYears { get; set; }

        public decimal TotalClosingCosts { get; set; }
        public decimal TotalPrepaids { get; set; }
        public decimal CashToClose { get; set; }

        public decimal PropertyTaxRate { get; set; }
        public decimal MonthlyPropertyTaxes { get; set; }
        public decimal MonthlyInsurance { get; set; }
        public decimal MonthlyPMI { get; set; }
        public decimal MonthlyMaintenance { get; set; }
        public decimal MonthlyUtilities { get; set; }
        public decimal MonthlyHOA { get; set; }
        public decimal VacancyRate { get; set; }
        public decimal VacancyAmount { get; set; }
        public decimal PropertyManagementFeeRate { get; set; }
        public decimal PropertyManagementFeeAmount { get; set; }

        public decimal MonthlyMortgagePI { get; set; }
        public decimal MonthlyExpensesTotal { get; set; }
        public decimal MonthlyNetCashflow { get; set; }
        public decimal AnnualNetCashflow { get; set; }
        public decimal CashOnCashReturnPct { get; set; }
        public decimal CapRatePct { get; set; }
        public decimal BreakEvenRent { get; set; }

        public bool IsEstimate { get; set; }
    }
}
