using RentalPropertyAnalyzer.Models.DBEntites;

namespace RentalPropertyAnalyzer.Models
{
    public class ComparisonViewModel
    {
        public List<ComparableProperty> PropertiesToCompare { get; set; }

        public ComparisonViewModel(List<ComparableProperty> propertiesToCompare)
        {
            PropertiesToCompare = propertiesToCompare;
        }
    }

    public class ComparableProperty
    {
        public int ZipID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PropertyType { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }
        public string? ImgSrc { get; set; }
        public decimal Price { get; set; }
        public decimal TaxAssessedValue { get; set; } // Retrieved from DB
        public decimal Downpayment { get; set; } // Retrieved from DB
        public decimal EstimatedMortgageCost { get; set; } // Retrieved from DB
        public decimal EstimatedInsuranceCost { get; set; } // Retrieved from DB
        public decimal EstimatedRent { get; set; } // Retrieved from DB 
                
        public decimal HOAEstimate { get; set; } // Retrieved from DB
        
        // Calculated property
        public decimal EstimatedMonthlyProfit
        {
            get
            {
                return EstimatedRent - (EstimatedMortgageCost + EstimatedInsuranceCost + HOAEstimate+MonthlyPMI);
            }
        }

        public decimal EstimatedMonthlyCost
        {
            get
            {
                return (EstimatedMortgageCost + EstimatedInsuranceCost + HOAEstimate);
            }
        }
        public decimal MonthlyPMI { get; set; } // Retrieved from DB 
    }
}

