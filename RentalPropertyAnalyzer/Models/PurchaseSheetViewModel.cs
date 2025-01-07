using System.Reflection.Emit;

namespace RentalPropertyAnalyzer.Models
{
    public class PurchaseSheetViewModel
    {
        public int ZipID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PropertyType { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
        public decimal Price { get; set; }
        public decimal TaxAssessedValue { get; set; }     
        public decimal Downpayment { get; set; }
        //public decimal EstimatedMortgageCosts { get; set; }
        public decimal EstimatedInsuranceCost { get; set; }

    }
}


