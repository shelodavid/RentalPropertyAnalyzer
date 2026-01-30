namespace RentalPropertyAnalyzer.Services.Forecast
{
    public class ForecastBaseResult
    {
        public decimal DownpaymentPercentage { get; set; }
        public int Term { get; set; }
        public decimal MortgageInterestRate { get; set; }
        public decimal PMIRate { get; set; }
        public decimal PropertyTaxRate { get; set; }
        public decimal HomeownersInsurance { get; set; }
        public decimal? VacancyRate { get; set; }
        public decimal? PropertyManagementFee { get; set; }
        public decimal? MonthlyMaintenanceBudget { get; set; }
        public decimal? MonthlyUtilitiesCost { get; set; }
        public decimal? OtherExpenses { get; set; }
        public decimal? HOAEstimate { get; set; }
        public decimal? ClosingCosts { get; set; }
        public decimal? LoanOriginationFee { get; set; }
        public decimal? RealtorClosingFeePercentage { get; set; }
        public decimal? RenovationCosts { get; set; }
    }
}
