using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalPropertyAnalyzer.Migrations
{
    public partial class AddGetForecastBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE OR ALTER PROCEDURE dbo.GetForecastBase
                    @ZipID INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT
                        CONVERT(INT, rl.Zpid) AS ZipID,
                        CONVERT(DECIMAL(18, 2), ISNULL(rl.Price, 0)) AS Price,
                        CONVERT(DECIMAL(18, 2), ISNULL(rl.EstimatedRent, 0)) AS EstimatedRent,
                        CONVERT(DECIMAL(18, 2), ISNULL(rl.TaxAssessedValue, 0)) AS TaxAssessedValue,
                        CONVERT(DECIMAL(18, 2), ISNULL(ip.DownpaymentPercentage, 0)) AS DownpaymentPercentage,
                        CONVERT(DECIMAL(18, 4), ISNULL(ip.MortgageInterestRate, 0)) AS MortgageInterestRate,
                        ISNULL(ip.Term, 0) AS Term,
                        CONVERT(DECIMAL(18, 2), ISNULL(ip.HOAEstimate, 0)) AS HOAEstimate
                    FROM dbo.RentalListings rl
                    INNER JOIN dbo.InvestmentProfiles ip
                        ON ip.Id = 1
                    WHERE rl.Zpid = CONVERT(VARCHAR(50), @ZipID);
                END
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DROP PROCEDURE IF EXISTS dbo.GetForecastBase;
                """);
        }
    }
}
