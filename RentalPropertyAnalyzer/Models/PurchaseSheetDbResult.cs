namespace RentalPropertyAnalyzer.Models
{
    public class PurchaseSheetDbResult
    {
        public int InvestmentProfileID { get; set; }
        public int ZipID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PropertyType { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }
        public string? ImgSrc { get; set; }
        public decimal Price { get; set; }
        public decimal TaxAssessedValue { get; set; }
        public decimal EstimatedRent { get; set; }
        public decimal DownpaymentPercentage { get; set; }
        public decimal MortgageInterestRate { get; set; }
        public int Term { get; set; }
        public decimal Downpayment { get; set; }
        public decimal MortgageAmount { get; set; }
        public decimal EstimatedMortgageCost { get; set; }
        public decimal AnnualHomeownersInsurance { get; set; }
        public int EscrowMonthsInsurance { get; set; }
        public decimal PropertyTaxRatePercent { get; set; }
        public decimal EstimatedMonthlyPropertyTaxes { get; set; }
        public int EscrowMonthsTaxes { get; set; }
        public decimal MonthlyPMI { get; set; }
        public decimal HOAEstimate { get; set; }
        public decimal LoanOriginationFee { get; set; }
        public decimal LoanClosingCosts { get; set; }
        public decimal RealtorsCost { get; set; }
        public decimal AppraisalFee { get; set; }
        public decimal CreditReportFee { get; set; }
        public decimal TitleInsuranceCost { get; set; }
        public decimal TitleSearchFee { get; set; }
        public decimal EscrowFee { get; set; }
        public decimal FloodInspectionFee { get; set; }
        public decimal MiscellaneousFees { get; set; }
        public decimal RecordingFees { get; set; }
        public decimal TransferTaxes { get; set; }
        public int PrepaidInterestDays { get; set; }
        public decimal TotalClosingCostsExcludingDownpayment { get; set; }
    }
}
