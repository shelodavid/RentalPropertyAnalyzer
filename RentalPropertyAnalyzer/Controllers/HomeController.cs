using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;
using RentalPropertyAnalyzer.Models.ViewModels;
using RentalPropertyAnalyzer.Services;

namespace RentalPropertyAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentalListingContext _rentalListingContext;
        private readonly SavedPropertiesContext _savedPropertiesContext;
        private readonly InvestmentProfileContext _investmentProfileContext;
        private readonly ForecastCalculator _forecastCalculator;


        // Update the constructor to inject YourDbContext
        public HomeController(
             ILogger<HomeController> logger,
             RentalListingContext rentalListingContext,
             SavedPropertiesContext savedPropertiesContext,
             InvestmentProfileContext investmentProfileContext,
             ForecastCalculator forecastCalculator)
        {
            _logger = logger;
            _rentalListingContext = rentalListingContext;
            _savedPropertiesContext = savedPropertiesContext;
            _investmentProfileContext = investmentProfileContext;
            _forecastCalculator = forecastCalculator;
        }

        [HttpGet]
        public IActionResult Index()
        {


            try
            {
                // Fetch saved properties using a stored procedure
                var savedProperties = _savedPropertiesContext.SavedProperties
                    .FromSqlRaw("EXEC dbo.ReturnSavedProperties")
                    .ToList();

                try
                {
                    // Get the count of saved properties using another stored procedure
                    var connection = _savedPropertiesContext.Database.GetDbConnection();
                    var command = connection.CreateCommand();
                    command.CommandText = "EXEC dbo.GetSavedPropertiesCount";
                    connection.Open();

                    var scalarResult = command.ExecuteScalar();
                    if (scalarResult == null || scalarResult == DBNull.Value)
                    {
                        throw new InvalidOperationException("Saved properties count query returned no value.");
                    }

                    int savedPropertiesCount = Convert.ToInt32(scalarResult);

                    // Pass the count to the view using ViewBag
                    ViewBag.SavedPropertiesCount = savedPropertiesCount;

                    // Close the connection
                    connection.Close();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while retrieving saved properties count.");
                }
              

                return View(savedProperties);
            }

            
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while retrieving saved properties.");
                return RedirectToAction("ErrorView"); // Redirect to a view that shows an error message
            }


        }

        public IActionResult DeleteProperty(int zipID)
        {
            try
            {
                // Call the stored procedure to delete the property
                _savedPropertiesContext.Database.ExecuteSqlRaw("EXEC dbo.DeleteProperty @ZipID", new SqlParameter("ZipID", zipID));

                // Redirect to the Index action after deletion
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting property with ZipID: {ZipID}", zipID);
                // Handle the error, show a message to the user or redirect to an error page
                return RedirectToAction("ErrorView");
            }
        }

        [HttpGet]
        public IActionResult GeneratePurchaseSheet(int zipID)
        {
            try
            {
                // Execute stored procedure to fetch purchase sheet data
                var purchaseSheet = _savedPropertiesContext.PurchaseSheetResults
                    .FromSqlRaw("EXEC dbo.GetPurchaseSheet @ZipID", new SqlParameter("ZipID", zipID))
                    .AsEnumerable()
                    .FirstOrDefault();

                if (purchaseSheet == null)
                {
                    return RedirectToAction("Index"); // Redirect if no data is returned
                }

                // Initialize new closing cost defaults for display.
                purchaseSheet.PropertyTaxRatePercent = 0;
                purchaseSheet.AnnualHomeownersInsurance = 0;

                // Pass the data to the view
                return View(purchaseSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating purchase sheet for ZipID: {ZipID}", zipID);
                return RedirectToAction("ErrorView");
            }
        }

        [HttpPost]
        public IActionResult GeneratePurchaseSheet(PurchaseSheetViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult Forecast(int? zipID)
        {
            var savedProperties = _savedPropertiesContext.SavedProperties.ToList();
            if (!savedProperties.Any())
            {
                return View(new PropertyForecastViewModel());
            }

            var selectedProperty = zipID.HasValue
                ? savedProperties.FirstOrDefault(p => p.ZipID == zipID.Value)
                : savedProperties.First();

            if (selectedProperty == null)
            {
                return View(new PropertyForecastViewModel());
            }

            var profile = _investmentProfileContext.InvestmentProfile.FirstOrDefault();
            var downpaymentPercent = profile?.DownpaymentPercentage ?? 20m;
            var interestRate = profile?.MortgageInterestRate ?? 6.5m;
            var termYears = profile?.Term ?? 30;

            var price = selectedProperty.Price ?? 0m;
            var downpaymentAmount = price * (downpaymentPercent / 100m);

            var closingCosts = profile?.ClosingCosts ?? 0m;
            var prepaids = 0m;
            var cashToClose = downpaymentAmount + closingCosts + prepaids;

            var monthlyTaxes = price > 0 && profile?.PropertyTaxRate != null
                ? (price * (profile.PropertyTaxRate / 100m)) / 12m
                : 0m;

            var annualInsurance = profile?.HomeownersInsurance ?? 0m;
            var monthlyInsurance = annualInsurance / 12m;
            var pmiRate = profile?.PMIRate ?? 0m;
            var monthlyPmi = downpaymentPercent < 20m && pmiRate > 0m
                ? (price * (pmiRate / 100m)) / 12m
                : 0m;

            var forecast = new PropertyForecastViewModel
            {
                ZipID = selectedProperty.ZipID,
                Address = selectedProperty.StreetAddress,
                ImgSrc = selectedProperty.ImgSrc,
                Price = price,
                EstimatedRent = selectedProperty.EstimatedRent ?? 0m,
                DownpaymentPercentage = downpaymentPercent,
                DownpaymentAmount = downpaymentAmount,
                InterestRate = interestRate,
                TermYears = termYears,
                TotalClosingCosts = closingCosts,
                TotalPrepaids = prepaids,
                CashToClose = cashToClose,
                PropertyTaxRate = profile?.PropertyTaxRate ?? 0m,
                MonthlyPropertyTaxes = monthlyTaxes,
                MonthlyInsurance = monthlyInsurance,
                MonthlyPMI = monthlyPmi,
                MonthlyMaintenance = profile?.MonthlyMaintenanceBudget ?? 0m,
                MonthlyUtilities = profile?.MonthlyUtilitiesCost ?? 0m,
                MonthlyHOA = profile?.HOAEstimate ?? 0m,
                VacancyRate = profile?.VacancyRate ?? 0m,
                PropertyManagementFeeRate = profile?.PropertyManagementFee ?? 0m,
                IsEstimate = true
            };

            var computed = _forecastCalculator.Calculate(forecast);
            ViewBag.SavedProperties = savedProperties;
            return View(computed);
        }




    }

}
