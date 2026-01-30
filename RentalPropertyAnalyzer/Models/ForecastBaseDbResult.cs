namespace RentalPropertyAnalyzer.Models
{
    public class ForecastBaseDbResult
    {
        public int ZipID { get; set; }
        public decimal Price { get; set; }
        public decimal TaxAssessedValue { get; set; }
        public decimal DownpaymentPercentage { get; set; }
        public decimal MortgageInterestRate { get; set; }
        public int Term { get; set; }
        public decimal HOAEstimate { get; set; }
    }
}
