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
        [HttpPost]
        public IActionResult SaveChanges(PurchaseSheetViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Return the view with validation errors
                    return View("GeneratePurchaseSheet", model);
                }

                // Additional validation checks
                if (model.Term <= 0)
                {
                    ModelState.AddModelError("Term", "Mortgage term must be greater than 0 years");
                    return View("GeneratePurchaseSheet", model);
                }

                if (model.MortgageInterestRate < 0)
                {
                    ModelState.AddModelError("MortgageInterestRate", "Interest rate cannot be negative");
                    return View("GeneratePurchaseSheet", model);
                }

                // Calculate downpayment and validate
                model.Downpayment = model.Price * (model.DownpaymentPercentage / 100m);
                if (model.Downpayment <= 0)
                {
                    ModelState.AddModelError("DownpaymentPercentage", "Downpayment must be greater than $0");
                    return View("GeneratePurchaseSheet", model);
                }

                // Calculate mortgage amount
                model.MortgageAmount = model.Price - model.Downpayment;
                if (model.MortgageAmount <= 0)
                {
                    ModelState.AddModelError("DownpaymentPercentage", "Mortgage amount must be greater than $0");
                    return View("GeneratePurchaseSheet", model);
                }

                // Calculate loan closing costs
                model.LoanClosingCosts = CalculateLoanClosingCosts(model);

                // Calculate monthly payment only if we have valid inputs
                decimal monthlyRate = (model.MortgageInterestRate / 100m) / 12m;
                int totalPayments = model.Term * 12;

                if (monthlyRate > 0 && totalPayments > 0)
                {
                    try
                    {
                        decimal factor = (decimal)Math.Pow(1 + (double)monthlyRate, totalPayments);
                        if (factor > 1) // Ensure we don't divide by zero
                        {
                            model.EstimatedMortgageCost = model.MortgageAmount * (monthlyRate * factor) / (factor - 1);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid calculation parameters. Please check your inputs.");
                            return View("GeneratePurchaseSheet", model);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error calculating mortgage payment");
                        ModelState.AddModelError("", "Error calculating mortgage payment. Please check your inputs.");
                        return View("GeneratePurchaseSheet", model);
                    }
                }
                else
                {
                    // Simple division for 0% interest
                    model.EstimatedMortgageCost = model.MortgageAmount / totalPayments;
                }

                // Validate the final monthly payment is reasonable
                if (model.EstimatedMortgageCost <= 0 || model.EstimatedMortgageCost > model.Price)
                {
                    ModelState.AddModelError("", "Calculated monthly payment is invalid. Please check your inputs.");
                    return View("GeneratePurchaseSheet", model);
                }

                return View("GeneratePurchaseSheet", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveChanges");
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("GeneratePurchaseSheet", model);
            }
        }

        private decimal CalculateLoanClosingCosts(PurchaseSheetViewModel model)
        {
            // Sum up all the closing costs
            return model.LoanOriginationFee +
                   model.AppraisalFee +
                   model.CreditReportFee +
                   model.TitleInsuranceCost +
                   model.TitleSearchFee +
                   model.EscrowFee +
                   model.FloodInspectionFee +
                   model.MiscellaneousFees;
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

                // Pass the data to the view
                return View(purchaseSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating purchase sheet for ZipID: {ZipID}", zipID);
                return RedirectToAction("ErrorView");
            }
        }




    }

}
