using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;

namespace RentalPropertyAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentalListingContext _rentalListingContext;
        private readonly SavedPropertiesContext _savedPropertiesContext;


        // Update the constructor to inject YourDbContext
        public HomeController(
             ILogger<HomeController> logger,
             RentalListingContext rentalListingContext,
             SavedPropertiesContext savedPropertiesContext)
        {
            _logger = logger;
            _rentalListingContext = rentalListingContext;
            _savedPropertiesContext = savedPropertiesContext;
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

                    int savedPropertiesCount = (int)command.ExecuteScalar();

                    // Pass the count to the view using ViewBag
                    ViewBag.SavedPropertiesCount = savedPropertiesCount;

                    // Close the connection
                    connection.Close();

                }
                catch (Exception ex)
                {

                }
              

                return View(savedProperties);
            }

            
            catch (Exception ex) 
            {
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



    }

}
