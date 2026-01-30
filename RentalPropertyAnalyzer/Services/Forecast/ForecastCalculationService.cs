using RentalListing = RentalPropertyAnalyzer.Models.DBEntites.RentalListings;

namespace RentalPropertyAnalyzer.Services.Forecast
{
    public class ForecastCalculationService
    {
        public RoiMetrics Calculate(RentalListing listing, ForecastBaseResult profile)
        {
            var price = Convert.ToDecimal(listing.Price);
            var grossRent = Convert.ToDecimal(listing.EstimatedRent);
            var taxAssessedValue = Convert.ToDecimal(listing.TaxAssessedValue);

            var downpaymentPercent = NormalizePercent(profile.DownpaymentPercentage);
            var downpaymentAmount = price * (downpaymentPercent / 100m);
            var loanAmount = price - downpaymentAmount;

            var interestRatePercent = NormalizePercent(profile.MortgageInterestRate);
            var monthlyRate = interestRatePercent / 100m / 12m;
            var totalPayments = profile.Term * 12;
            var monthlyPrincipalAndInterest = CalculateMonthlyPayment(loanAmount, monthlyRate, totalPayments);

            var monthlyPmi = 0m;
            var pmiRatePercent = NormalizePercent(profile.PMIRate);
            if (downpaymentPercent < 20m && pmiRatePercent > 0m)
            {
                monthlyPmi = (loanAmount * (pmiRatePercent / 100m)) / 12m;
            }

            var vacancyRatePercent = NormalizePercent(profile.VacancyRate ?? 0m);
            var vacancyLoss = grossRent * (vacancyRatePercent / 100m);
            var effectiveRent = grossRent - vacancyLoss;

            var propertyTaxRatePercent = NormalizePercent(profile.PropertyTaxRate);
            var monthlyPropertyTaxes = (taxAssessedValue * (propertyTaxRatePercent / 100m)) / 12m;
            var monthlyInsurance = profile.HomeownersInsurance / 12m;

            var propertyManagementFeePercent = NormalizePercent(profile.PropertyManagementFee ?? 0m);
            var monthlyPropertyManagementFee = effectiveRent * (propertyManagementFeePercent / 100m);

            var monthlyOperatingExpenses = monthlyPropertyTaxes
                + monthlyInsurance
                + (profile.HOAEstimate ?? 0m)
                + monthlyPropertyManagementFee
                + (profile.MonthlyMaintenanceBudget ?? 0m)
                + (profile.MonthlyUtilitiesCost ?? 0m)
                + (profile.OtherExpenses ?? 0m);

            var monthlyNoi = effectiveRent - monthlyOperatingExpenses;
            var monthlyDebtService = monthlyPrincipalAndInterest + monthlyPmi;
            var monthlyNetCashflow = monthlyNoi - monthlyDebtService;

            var annualNoi = monthlyNoi * 12m;
            var annualNetCashflow = monthlyNetCashflow * 12m;
            var annualDebtService = monthlyDebtService * 12m;

            var realtorFeePercent = NormalizePercent(profile.RealtorClosingFeePercentage ?? 0m);
            var closingCostsUpfront = (profile.ClosingCosts ?? 0m)
                + (profile.LoanOriginationFee ?? 0m)
                + (price * realtorFeePercent / 100m);

            var totalCashInvested = downpaymentAmount + closingCostsUpfront + (profile.RenovationCosts ?? 0m);

            var capRatePct = price > 0m ? (annualNoi / price) * 100m : 0m;
            var cashOnCashReturnPct = totalCashInvested > 0m ? (annualNetCashflow / totalCashInvested) * 100m : 0m;
            var dscr = annualDebtService > 0m ? (annualNoi / annualDebtService) : 0m;

            return new RoiMetrics
            {
                PurchasePrice = Round2(price),
                DownpaymentAmount = Round2(downpaymentAmount),
                ClosingCostsUpfront = Round2(closingCostsUpfront),
                TotalCashInvested = Round2(totalCashInvested),
                MonthlyGrossRent = Round2(grossRent),
                MonthlyEffectiveRent = Round2(effectiveRent),
                MonthlyOperatingExpenses = Round2(monthlyOperatingExpenses),
                MonthlyNOI = Round2(monthlyNoi),
                MonthlyDebtService = Round2(monthlyDebtService),
                MonthlyNetCashflow = Round2(monthlyNetCashflow),
                AnnualNOI = Round2(annualNoi),
                AnnualNetCashflow = Round2(annualNetCashflow),
                CashOnCashReturnPct = Round4(cashOnCashReturnPct),
                CapRatePct = Round4(capRatePct),
                DSCR = Round4(dscr)
            };
        }

        public decimal NormalizePercent(decimal value)
        {
            if (value > 0m && value <= 1m)
            {
                return value * 100m;
            }

            return value;
        }

        private static decimal CalculateMonthlyPayment(decimal principal, decimal monthlyRate, int totalPayments)
        {
            if (principal <= 0m || totalPayments <= 0)
            {
                return 0m;
            }

            if (monthlyRate <= 0m)
            {
                return principal / totalPayments;
            }

            var rateFactor = (decimal)Math.Pow(1 + (double)monthlyRate, totalPayments);
            return principal * monthlyRate * rateFactor / (rateFactor - 1m);
        }

        private static decimal Round2(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

        private static decimal Round4(decimal value) => Math.Round(value, 4, MidpointRounding.AwayFromZero);
    }
}
