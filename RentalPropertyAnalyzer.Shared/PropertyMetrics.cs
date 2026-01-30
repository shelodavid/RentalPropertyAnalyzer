namespace RentalPropertyAnalyzer.Shared;

public sealed record PropertyMetrics(decimal MonthlyRent, decimal MonthlyExpenses)
{
    public decimal MonthlyCashflow => MonthlyRent - MonthlyExpenses;
}
