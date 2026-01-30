CREATE OR ALTER PROCEDURE [dbo].[GetPurchaseSheet]
    @ZipID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InvestmentProfileID INT = 1;

    SELECT
        @InvestmentProfileID AS InvestmentProfileID,
        sp.ZipID,
        sp.StreetAddress,
        sp.PropertyType,
        sp.Bathrooms,
        sp.Bedrooms,
        sp.ImgSrc,
        base.PriceValue AS Price,
        base.TaxAssessedValue AS TaxAssessedValue,
        base.EstimatedRent AS EstimatedRent,
        base.DownpaymentPercentage AS DownpaymentPercentage,
        base.MortgageInterestRate AS MortgageInterestRate,
        base.TermYears AS Term,
        base.DownpaymentAmount AS Downpayment,
        base.MortgageAmount AS MortgageAmount,
        base.EstimatedMortgageCost AS EstimatedMortgageCost,
        base.AnnualHomeownersInsurance AS AnnualHomeownersInsurance,
        base.EscrowMonthsInsurance AS EscrowMonthsInsurance,
        base.PropertyTaxRatePercent AS PropertyTaxRatePercent,
        base.EstimatedMonthlyPropertyTaxes AS EstimatedMonthlyPropertyTaxes,
        base.EscrowMonthsTaxes AS EscrowMonthsTaxes,
        base.MonthlyPMI AS MonthlyPMI,
        base.HOAEstimate AS HOAEstimate,
        base.LoanOriginationFee AS LoanOriginationFee,
        base.LoanClosingCosts AS LoanClosingCosts,
        base.RealtorsCost AS RealtorsCost,
        base.AppraisalFee AS AppraisalFee,
        base.CreditReportFee AS CreditReportFee,
        base.TitleInsuranceCost AS TitleInsuranceCost,
        base.TitleSearchFee AS TitleSearchFee,
        base.EscrowFee AS EscrowFee,
        base.FloodInspectionFee AS FloodInspectionFee,
        base.MiscellaneousFees AS MiscellaneousFees,
        base.RecordingFees AS RecordingFees,
        base.TransferTaxes AS TransferTaxes,
        base.PrepaidInterestDays AS PrepaidInterestDays,
        totals.TotalClosingCostsExcludingDownpayment AS TotalClosingCostsExcludingDownpayment
    FROM dbo.SavedPropertyListings sp
    CROSS APPLY
    (
        SELECT
            ISNULL(CONVERT(DECIMAL(18, 2), sp.Price), 0) AS PriceValue,
            ISNULL(CONVERT(DECIMAL(18, 2), sp.TaxAssessedValue), 0) AS TaxAssessedValue,
            ISNULL(CONVERT(DECIMAL(18, 2), sp.EstimatedRent), 0) AS EstimatedRent,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.DownpaymentPercentage), 0) AS DownpaymentPercentage,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.MortgageInterestRate), 0) AS MortgageInterestRate,
            ISNULL(CONVERT(INT, ip.Term), 0) AS TermYears,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.PMIRate), 0) AS PMIRate,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.PropertyTaxRate), 0) AS PropertyTaxRatePercent,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.HomeownersInsurance), 0) AS AnnualHomeownersInsurance,
            2 AS EscrowMonthsInsurance,
            3 AS EscrowMonthsTaxes,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.HOAEstimate), 0) AS HOAEstimate,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.LoanOriginationFee), 0) AS LoanOriginationFeePercent,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.ClosingCosts), 0) AS LoanClosingCosts,
            ISNULL(CONVERT(DECIMAL(18, 4), ip.RealtorClosingFeePercentage), 0) AS RealtorClosingFeePercentage,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.AppraisalFee), 0) AS AppraisalFee,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.CreditReportFee), 0) AS CreditReportFee,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.TitleInsuranceCost), 0) AS TitleInsuranceCost,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.TitleSearchFee), 0) AS TitleSearchFee,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.EscrowFee), 0) AS EscrowFee,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.FloodInspectionFee), 0) AS FloodInspectionFee,
            ISNULL(CONVERT(DECIMAL(18, 2), ip.MiscellaneousFees), 0) AS MiscellaneousFees,
            CONVERT(DECIMAL(18, 2), 150) AS RecordingFees,
            CONVERT(DECIMAL(18, 2), 0) AS TransferTaxes,
            CONVERT(INT, 15) AS PrepaidInterestDays
        FROM dbo.InvestmentProfile ip
        WHERE ip.Id = @InvestmentProfileID
    ) profile
    CROSS APPLY
    (
        SELECT
            profile.PriceValue AS PriceValue,
            profile.TaxAssessedValue AS TaxAssessedValue,
            profile.EstimatedRent AS EstimatedRent,
            profile.DownpaymentPercentage AS DownpaymentPercentage,
            profile.MortgageInterestRate AS MortgageInterestRate,
            profile.TermYears AS TermYears,
            profile.PMIRate AS PMIRate,
            profile.PropertyTaxRatePercent AS PropertyTaxRatePercent,
            profile.AnnualHomeownersInsurance AS AnnualHomeownersInsurance,
            profile.EscrowMonthsInsurance AS EscrowMonthsInsurance,
            profile.EscrowMonthsTaxes AS EscrowMonthsTaxes,
            profile.HOAEstimate AS HOAEstimate,
            profile.LoanOriginationFeePercent AS LoanOriginationFeePercent,
            profile.LoanClosingCosts AS LoanClosingCosts,
            profile.RealtorClosingFeePercentage AS RealtorClosingFeePercentage,
            profile.AppraisalFee AS AppraisalFee,
            profile.CreditReportFee AS CreditReportFee,
            profile.TitleInsuranceCost AS TitleInsuranceCost,
            profile.TitleSearchFee AS TitleSearchFee,
            profile.EscrowFee AS EscrowFee,
            profile.FloodInspectionFee AS FloodInspectionFee,
            profile.MiscellaneousFees AS MiscellaneousFees,
            profile.RecordingFees AS RecordingFees,
            profile.TransferTaxes AS TransferTaxes,
            profile.PrepaidInterestDays AS PrepaidInterestDays,
            ISNULL(CONVERT(DECIMAL(18, 2), profile.PriceValue * (profile.DownpaymentPercentage / 100.0)), 0) AS DownpaymentAmount,
            ISNULL(CONVERT(DECIMAL(18, 2), profile.PriceValue - (profile.PriceValue * (profile.DownpaymentPercentage / 100.0))), 0) AS MortgageAmount,
            ISNULL(CONVERT(DECIMAL(18, 2), (profile.TaxAssessedValue * (profile.PropertyTaxRatePercent / 100.0)) / 12.0), 0) AS EstimatedMonthlyPropertyTaxes
    ) amounts
    CROSS APPLY
    (
        SELECT
            amounts.PriceValue AS PriceValue,
            amounts.TaxAssessedValue AS TaxAssessedValue,
            amounts.EstimatedRent AS EstimatedRent,
            amounts.DownpaymentPercentage AS DownpaymentPercentage,
            amounts.MortgageInterestRate AS MortgageInterestRate,
            amounts.TermYears AS TermYears,
            amounts.PMIRate AS PMIRate,
            amounts.PropertyTaxRatePercent AS PropertyTaxRatePercent,
            amounts.AnnualHomeownersInsurance AS AnnualHomeownersInsurance,
            amounts.EscrowMonthsInsurance AS EscrowMonthsInsurance,
            amounts.EscrowMonthsTaxes AS EscrowMonthsTaxes,
            amounts.HOAEstimate AS HOAEstimate,
            amounts.LoanOriginationFeePercent AS LoanOriginationFeePercent,
            amounts.LoanClosingCosts AS LoanClosingCosts,
            amounts.RealtorClosingFeePercentage AS RealtorClosingFeePercentage,
            amounts.AppraisalFee AS AppraisalFee,
            amounts.CreditReportFee AS CreditReportFee,
            amounts.TitleInsuranceCost AS TitleInsuranceCost,
            amounts.TitleSearchFee AS TitleSearchFee,
            amounts.EscrowFee AS EscrowFee,
            amounts.FloodInspectionFee AS FloodInspectionFee,
            amounts.MiscellaneousFees AS MiscellaneousFees,
            amounts.RecordingFees AS RecordingFees,
            amounts.TransferTaxes AS TransferTaxes,
            amounts.PrepaidInterestDays AS PrepaidInterestDays,
            amounts.DownpaymentAmount AS DownpaymentAmount,
            amounts.MortgageAmount AS MortgageAmount,
            amounts.EstimatedMonthlyPropertyTaxes AS EstimatedMonthlyPropertyTaxes,
            CASE
                WHEN amounts.MortgageAmount <= 0 OR amounts.TermYears <= 0 THEN 0
                WHEN amounts.MortgageInterestRate <= 0 THEN amounts.MortgageAmount / NULLIF(amounts.TermYears * 12, 0)
                ELSE
                    amounts.MortgageAmount
                    * ((amounts.MortgageInterestRate / 100.0 / 12.0)
                    * POWER(1 + (amounts.MortgageInterestRate / 100.0 / 12.0), amounts.TermYears * 12))
                    / NULLIF(POWER(1 + (amounts.MortgageInterestRate / 100.0 / 12.0), amounts.TermYears * 12) - 1, 0)
            END AS EstimatedMortgageCost,
            CASE
                WHEN amounts.DownpaymentPercentage < 20 AND amounts.PMIRate > 0
                    THEN (amounts.MortgageAmount * (amounts.PMIRate / 100.0)) / 12.0
                ELSE 0
            END AS MonthlyPMI,
            ISNULL(CONVERT(DECIMAL(18, 2), amounts.MortgageAmount * (amounts.LoanOriginationFeePercent / 100.0)), 0) AS LoanOriginationFee,
            ISNULL(CONVERT(DECIMAL(18, 2), amounts.PriceValue * (amounts.RealtorClosingFeePercentage / 100.0)), 0) AS RealtorsCost
    ) base
    CROSS APPLY
    (
        SELECT
            ISNULL(CONVERT(DECIMAL(18, 2),
                base.LoanOriginationFee
                + base.LoanClosingCosts
                + base.RealtorsCost
                + base.AppraisalFee
                + base.CreditReportFee
                + base.TitleInsuranceCost
                + base.TitleSearchFee
                + base.EscrowFee
                + base.FloodInspectionFee
                + base.MiscellaneousFees
                + base.RecordingFees
                + base.TransferTaxes
            ), 0) AS TotalClosingCostsExcludingDownpayment
    ) totals
    WHERE sp.ZipID = @ZipID;
END;
GO
