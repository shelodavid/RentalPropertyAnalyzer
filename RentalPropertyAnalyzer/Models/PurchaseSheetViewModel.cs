using System.Reflection.Emit;

namespace RentalPropertyAnalyzer.Models
{
    public class PurchaseSheetViewModel
    {
    
        public int InvestmentProfileID { get; set; }
        public decimal CreditReportFee { get; set; }  
        public decimal TitleSearchFee { get; set; }

        public decimal MiscellaneousFees { get; set; }
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
        public decimal PropertyTaxRatePercent { get; set; }
        public int PrepaidInterestDays { get; set; } = 15;
        public decimal AnnualHomeownersInsurance { get; set; }
        public int EscrowMonthsTaxes { get; set; } = 3;
        public int EscrowMonthsInsurance { get; set; } = 2;
        public decimal RecordingFees { get; set; } = 150;
        public decimal TransferTaxes { get; set; }

        public decimal EstimatedMonthlyPropertyTaxes
        {
            get
            {
                if (Price <= 0 || PropertyTaxRatePercent <= 0)
                {
                    return 0;
                }

                return (Price * (PropertyTaxRatePercent / 100m)) / 12m;
            }
        }

        public decimal PrepaidInterestAmount
        {
            get
            {
                if (MortgageAmount <= 0 || MortgageInterestRate <= 0 || PrepaidInterestDays <= 0)
                {
                    return 0;
                }

                var dailyRate = (MortgageInterestRate / 100m) / 365m;
                return MortgageAmount * dailyRate * PrepaidInterestDays;
            }
        }

        public decimal EscrowSeedAmount
        {
            get
            {
                var monthlyTaxes = EstimatedMonthlyPropertyTaxes;
                var monthlyInsurance = AnnualHomeownersInsurance / 12m;
                return (monthlyTaxes * EscrowMonthsTaxes) + (monthlyInsurance * EscrowMonthsInsurance);
            }
        }

        public decimal TotalPrepaids
        {
            get
            {
                return PrepaidInterestAmount + EscrowSeedAmount;
            }
        }

        public decimal TotalGovernmentFees
        {
            get
            {
                return RecordingFees + TransferTaxes;
            }
        }

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
                     + FloodInspectionFee
                     + TotalPrepaids
                     + TotalGovernmentFees;
            }
        }

    }
}
