using System.ComponentModel.DataAnnotations;

namespace RentalPropertyAnalyzer.Models
{
    public class SavedProperties
    {
        public SavedProperties() 
        { 
            StreetAddress = null;
            ImgSrc = null;

        }
        [Key]
        public int ZipID { get; set; }
        public string? StreetAddress {  get; set; }
        public string? PropertyType { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }
        public string? ImgSrc { get; set; }
        public decimal? Price { get; set; }
        public decimal? TaxAssessedValue { get; set; }
        public decimal? Downpayment { get; set; }
        public decimal? EstimatedMortgageCost { get; set; }
        public decimal? EstimatedInsuranceCost { get; set; }
        public decimal? EstimatedRent { get; set; }
        public decimal? HOAEstimate { get; set; }
        public decimal? MonthlyPMI { get; set; }
        public decimal? EstimatedMonthlyProfit
        {
            get
            {
                return EstimatedRent - (EstimatedMortgageCost + EstimatedInsuranceCost + HOAEstimate + MonthlyPMI);
            }
        }

        public decimal? EstimatedMonthlyCost
        {
            get
            {
                return (EstimatedMortgageCost + EstimatedInsuranceCost + HOAEstimate);
            }
        }

        public decimal? TVSPRatio
        {
            get
            {
                if (TaxAssessedValue is null || Price is null)
                {
                    return null;
                }

                if (TaxAssessedValue == 0)
                {
                    // Option 1: Return null if TaxAssessedValue is 0
                    return null;

                    // Option 2: Return a specific value indicating an invalid or undefined ratio
                    // return 0; // or any other value you deem appropriate
                }
                else
                {
                    return Math.Round(Price.Value / TaxAssessedValue.Value, 3, MidpointRounding.AwayFromZero);
                }
            }
        }
    }
}
