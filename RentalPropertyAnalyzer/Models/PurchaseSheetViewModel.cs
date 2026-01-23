using System.ComponentModel.DataAnnotations;

namespace RentalPropertyAnalyzer.Models
{
    public class PurchaseSheetViewModel
    {
    
        public int InvestmentProfileID { get; set; }

        [Range(0, 500, ErrorMessage = "Credit Report Fee must be between $0 and $500")]
        [Display(Name = "Credit Report Fee")]
        public decimal CreditReportFee { get; set; }

        [Range(0, 1000, ErrorMessage = "Title Search Fee must be between $0 and $1,000")]
        [Display(Name = "Title Search Fee")]
        public decimal TitleSearchFee { get; set; }

        [Range(0, 5000, ErrorMessage = "Miscellaneous Fees must be between $0 and $5,000")]
        [Display(Name = "Miscellaneous Fees")]
        public decimal MiscellaneousFees { get; set; }

        public int ZipID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PropertyType { get; set; }
        public string? Bathrooms { get; set; }
        public string? Bedrooms { get; set; }

        [Required]
        [Range(1000, 100000000, ErrorMessage = "Price must be between $1,000 and $100,000,000")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100000000, ErrorMessage = "Tax Assessed Value must be between $0 and $100,000,000")]
        [Display(Name = "Tax Assessed Value")]
        public decimal TaxAssessedValue { get; set; }

        public decimal Downpayment { get; set; }
        public decimal EstimatedMortgageCost { get; set; }
        public decimal EstimatedInsuranceCost { get; set; }
        public decimal LoanClosingCosts { get; set; }

        [Range(0, 100, ErrorMessage = "Loan Origination Fee must be between 0% and 100%")]
        [Display(Name = "Loan Origination Fee (%)")]
        public decimal LoanOriginationFee { get; set; }

        public decimal MortgageAmount { get; set; }
        public decimal RealtorsCost { get; set; }

        [Range(0, 5000, ErrorMessage = "Appraisal Fee must be between $0 and $5,000")]
        [Display(Name = "Appraisal Fee")]
        public decimal AppraisalFee { get; set; }

        [Range(0, 5000, ErrorMessage = "Escrow Fee must be between $0 and $5,000")]
        [Display(Name = "Escrow Fee")]
        public decimal EscrowFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Downpayment Percentage must be between 0% and 100%")]
        [Display(Name = "Downpayment Percentage")]
        public decimal DownpaymentPercentage { get; set; }

        [Range(0, 3000, ErrorMessage = "Title Insurance Cost must be between $0 and $3,000")]
        [Display(Name = "Title Insurance Cost")]
        public decimal TitleInsuranceCost { get; set; }

        [Range(0, 1000, ErrorMessage = "Flood Inspection Fee must be between $0 and $1,000")]
        [Display(Name = "Flood Inspection Fee")]
        public decimal FloodInspectionFee { get; set; }

        public string? ImgSrc { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage = "Mortgage Term must be between 1 and 30 years")]
        [Display(Name = "Mortgage Term (Years)")]
        public int Term { get; set; }

        [Required]
        [Range(0.1, 20, ErrorMessage = "Mortgage Interest Rate must be between 0.1% and 20%")]
        [Display(Name = "Mortgage Interest Rate")]
        public decimal MortgageInterestRate { get; set; }

        public decimal TotalCost
        {
            get
            {
                // Add up the downpayment and fees
                return Downpayment                    
                     + AppraisalFee
                    +CreditReportFee
                    +LoanOriginationFee
                     + TitleInsuranceCost
                     + TitleSearchFee
                     + EscrowFee
                     + FloodInspectionFee
                     + MiscellaneousFees;
            }
        }

        private decimal CalculateOriginationFee()
        {
            return MortgageAmount * (LoanOriginationFee / 100m);
        }
    }
}


