using RentalPropertyAnalyzer.Models.ViewModels;

namespace RentalPropertyAnalyzer.Services
{
    public class ForecastCalculator
    {
        public PropertyForecastViewModel Calculate(PropertyForecastViewModel model)
        {
            var loanAmount = model.Price - model.DownpaymentAmount;
            model.MonthlyMortgagePI = CalculateMonthlyPayment(loanAmount, model.InterestRate, model.TermYears);

            model.VacancyAmount = model.EstimatedRent * (model.VacancyRate / 100m);
            model.PropertyManagementFeeAmount = model.EstimatedRent * (model.PropertyManagementFeeRate / 100m);

            model.MonthlyExpensesTotal = model.MonthlyMortgagePI
                + model.MonthlyPropertyTaxes
                + model.MonthlyInsurance
                + model.MonthlyPMI
                + model.MonthlyMaintenance
                + model.MonthlyUtilities
                + model.MonthlyHOA
                + model.PropertyManagementFeeAmount
                + model.VacancyAmount;

            model.MonthlyNetCashflow = model.EstimatedRent - model.MonthlyExpensesTotal;
            model.AnnualNetCashflow = model.MonthlyNetCashflow * 12m;

            model.CashOnCashReturnPct = model.CashToClose > 0 ? (model.AnnualNetCashflow / model.CashToClose) * 100m : 0m;
            model.CapRatePct = model.Price > 0 ? ((model.EstimatedRent * 12m - (model.MonthlyExpensesTotal - model.MonthlyMortgagePI) * 12m) / model.Price) * 100m : 0m;
            model.BreakEvenRent = model.MonthlyExpensesTotal;

            return model;
        }

        private static decimal CalculateMonthlyPayment(decimal principal, decimal annualRatePercent, int termYears)
        {
            if (principal <= 0 || annualRatePercent <= 0 || termYears <= 0)
            {
                return 0m;
            }

            var monthlyRate = (annualRatePercent / 100m) / 12m;
            var totalPayments = termYears * 12;
            var factor = (decimal)Math.Pow((double)(1 + monthlyRate), totalPayments);
            return principal * monthlyRate * factor / (factor - 1);
        }
    }
}
