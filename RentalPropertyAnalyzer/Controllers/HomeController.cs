using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;
using RentalPropertyAnalyzer.Models;

namespace RentalPropertyAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentalListingContext _rentalListingContext;
        private readonly SavedPropertiesContext _savedPropertiesContext;
        private readonly InvestmentProfileContext _investmentProfileContext;
        // Update the constructor to inject YourDbContext
        public HomeController(
             ILogger<HomeController> logger,
             RentalListingContext rentalListingContext,
             SavedPropertiesContext savedPropertiesContext,
             InvestmentProfileContext investmentProfileContext)
        {
            _logger = logger;
            _rentalListingContext = rentalListingContext;
            _savedPropertiesContext = savedPropertiesContext;
            _investmentProfileContext = investmentProfileContext;
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
                var profile = _investmentProfileContext.InvestmentProfile.FirstOrDefault();
                purchaseSheet.PropertyTaxRatePercent = profile?.PropertyTaxRate ?? 0m;
                purchaseSheet.AnnualHomeownersInsurance = profile?.HomeownersInsurance ?? 0m;

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
            if (!zipID.HasValue)
            {
                return RedirectToAction("Index");
            }

            var forecastBase = _savedPropertiesContext.ForecastBaseRows
                .FromSqlRaw("EXEC dbo.GetForecastBase @ZipID", new SqlParameter("ZipID", zipID))
                .AsEnumerable()
                .FirstOrDefault();

            if (forecastBase == null)
            {
                return RedirectToAction("Index");
            }

            return View(forecastBase);
        }




    }

}
