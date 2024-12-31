namespace RentalPropertyAnalyzer.Models
{
    public class InvestmentProfile
    {
        public InvestmentProfile()
        {
            InvestmentProfileName = string.Empty;

        }
        public int Id { get; set; } // Primary key
        public string InvestmentProfileName { get; set; } // Name or description of the Investment Profile
        public decimal DownpaymentPercentage { get; set; } // Initial down payment amount
        public int Term { get; set; } // Mortgage Term in Years
        public decimal MortgageInterestRate { get; set; } // Mortgage interest rate (annual)
        public decimal PMIRate { get; set; } // Average Private Mortgage Insurance Rate for State
        public decimal PropertyTaxRate { get; set; } // Annual property tax rate (percentage)
        public decimal HomeownersInsurance { get; set; } // Annual homeowners insurance cost
        public decimal BalloonInsurance { get; set; } // Annual Balloon insurance cost
        public decimal RealtorClosingFeePercentage { get; set; } // Realtor commission fee percentage
        public decimal ClosingCosts { get; set; } // Estimated closing costs
        public decimal RenovationCosts { get; set; } // Estimated renovation costs
        public decimal OtherExpenses { get; set; } // Any other one-time expenses
        public decimal AnnualAppreciationRate { get; set; } // Expected annual appreciation rate (percentage)
        public decimal VacancyRate { get; set; } // Expected vacancy rate (percentage)
        public decimal PropertyManagementFee { get; set; } // Monthly property management fee
        public decimal MonthlyMaintenanceBudget { get; set; } // Monthly maintenance and repairs cost
        public decimal MonthlyUtilitiesCost { get; set; } // Monthly utilities cost (e.g., water, electricity)
        public decimal HOAEstimate { get; set; } // Monthly homeowner association fees
        public decimal LoanOriginationFee { get; set; }
        public decimal AppraisalFee { get; set; }
        public decimal CreditReportFee { get; set; }
        public decimal TitleInsuranceCost { get; set; }
        public decimal TitleSearchFee { get; set; }
        public decimal EscrowFee { get; set; }
        public decimal FloodInspectionFee { get; set; }
        public decimal MiscellaneousFees { get; set; }




    }
}
