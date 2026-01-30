namespace RentalPropertyAnalyzer.Services.Forecast
{
    public class RoiMetrics
    {
        public decimal PurchasePrice { get; set; }
        public decimal DownpaymentAmount { get; set; }
        public decimal ClosingCostsUpfront { get; set; }
        public decimal TotalCashInvested { get; set; }
        public decimal MonthlyGrossRent { get; set; }
        public decimal MonthlyEffectiveRent { get; set; }
        public decimal MonthlyOperatingExpenses { get; set; }
        public decimal MonthlyNOI { get; set; }
        public decimal MonthlyDebtService { get; set; }
        public decimal MonthlyNetCashflow { get; set; }
        public decimal AnnualNOI { get; set; }
        public decimal AnnualNetCashflow { get; set; }
        public decimal CashOnCashReturnPct { get; set; }
        public decimal CapRatePct { get; set; }
        public decimal DSCR { get; set; }
    }
}
