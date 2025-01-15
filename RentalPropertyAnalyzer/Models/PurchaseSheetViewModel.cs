using System.Reflection.Emit;

namespace RentalPropertyAnalyzer.Models
{
    public class PurchaseSheetViewModel
    {
        public int ZipID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PropertyType { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }
        public decimal Price { get; set; }
        public decimal TaxAssessedValue { get; set; }     
        public decimal Downpayment { get; set; }
        public decimal EstimatedMortgageCost { get; set; }
        public decimal EstimatedInsuranceCost { get; set; }
        public decimal LoanClosingCosts { get; set; }   
        public decimal LoanOriginationFee { get; set; }
        public decimal MortgageAmount { get; set; }
        public decimal RealtorsCost { get; set; }
        public decimal AppraisalFee { get; set; }
        public decimal EscrowFee { get; set; }
        public decimal DownpaymentPercentage { get; set; }
        public decimal TitleInsuranceCost { get; set; }
        public decimal FloodInspectionFee { get; set; }
        public string? ImgSrc { get; set; }
        public int Term { get; set; }
        public decimal MortgageInterestRate { get; set; }

        public decimal TotalCost
        {
            get
            {
                // Add up the downpayment and fees
                return Downpayment
                     + LoanClosingCosts
                     + LoanOriginationFee
                     + RealtorsCost
                     + AppraisalFee
                     + EscrowFee
                     + TitleInsuranceCost
                     + FloodInspectionFee;
            }
        }

    }
}


