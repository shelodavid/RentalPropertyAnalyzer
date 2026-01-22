using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RentalPropertyAnalyzer.DataAccessLayer;
using RentalPropertyAnalyzer.Models;
using RentalPropertyAnalyzer.Services;

namespace RentalPropertyAnalyzer.Controllers
{
    public class RentalListingsController : Controller
    {
        private readonly RentalListingContext _context;
        private readonly ILogger<RentalListingsController> _logger;
        private readonly StateService _stateService;

        public RentalListingsController(
            RentalListingContext context,
            ILogger<RentalListingsController> logger,
            StateService stateService)
        {
            _context = context;
            _logger = logger;
            _stateService = stateService;
        }


        [HttpGet]
        public IActionResult Index(string? propertyTypeFilter, string? stateFilter, string? countyFilter, string? ZipCodeSortParam, int minPrice, int maxPrice, int pageNumber = 1, int pageSize = 40, string? sortOrder = "")
        {
            ViewBag.StateFilter = stateFilter;
            ViewBag.CountyFilter = countyFilter;
            ViewBag.CurrentFilter = propertyTypeFilter;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RatioSortParam = sortOrder == "Ratio_Asc" ? "Ratio_Desc" : "Ratio_Asc";
            ViewBag.ZipCodeSortParam = sortOrder == "ZipCode_Asc" ? "ZipCode_Desc" : "ZipCode_Asc";

            var query = _context.RentalListings.AsQueryable();

            //Filter by State Filter

            if (!string.IsNullOrEmpty(stateFilter))
            {
                query = query.Where(r => r.State.Equals(stateFilter));
            }

            //Filter by County Filter

            if (!string.IsNullOrEmpty(countyFilter))
            {
                query = query.Where(r => r.County.Equals(countyFilter));
            }

            // Filter by property type if applicable
            if (!string.IsNullOrEmpty(propertyTypeFilter))
            {
                query = query.Where(r => r.PropertyType.Equals(propertyTypeFilter));
            }

            //Filter by minimum and maximum price
            if (minPrice > 0)
            {
                query = query.Where(r => r.Price >= minPrice);
            }
            if (maxPrice > 0)
            {
                query = query.Where(r => r.Price <= maxPrice);
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "Ratio_Desc":
                    query = query.OrderByDescending(r => r.Price != 0 ? r.EstimatedRent / r.Price : 0);
                    break;
                case "Ratio_Asc":
                    query = query.OrderBy(r => r.Price != 0 ? r.EstimatedRent / r.Price : 0);
                    break;
                case "ZipCode_Asc":
                    query = query.OrderBy(r => r.ZipCode); // Sort by ZipCode in ascending order
                    break;
                case "ZipCode_Desc":
                    query = query.OrderByDescending(r => r.ZipCode); // Sort by ZipCode in descending order
                    break;
                default:
                    query = query.OrderBy(r => r.ID); // Default sorting
                    break;
            }

            // Pagination
            var totalCount = query.Count();
            var rentallistings = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            // Map to RentalViewModel
            List<RentalViewModel> rentalViews = rentallistings.Select(rentalisting => new RentalViewModel
            {
                ID = rentalisting.ID,
                Zpid = rentalisting.Zpid,
                StreetAddress = rentalisting.StreetAddress,
                City = rentalisting.City,
                State = rentalisting.State,
                ZipCode = rentalisting.ZipCode,
                PropertyType = rentalisting.PropertyType,
                Bathrooms = rentalisting.Bathrooms,
                Bedrooms = rentalisting.Bedrooms,
                ImgSrc = rentalisting.ImgSrc,
                Price = rentalisting.Price,
                TaxAssessedValue = rentalisting.TaxAssessedValue,
                EstimatedRent = rentalisting.EstimatedRent,
                //Latitude = rentalisting.Latitude,
                //Longitude = rentalisting.Longitude,
                AnalysisDate = rentalisting.AnalysisDate,
                County=rentalisting.County,
                RentalPriceRatio = rentalisting.Price != 0 ? rentalisting.EstimatedRent / rentalisting.Price : 0
            }).ToList();

            // Fetch the list of states using the StateService
            var states = _stateService.GetStates();
            string? selectedState = Request.Query["stateFilter"];

            //Fetch County

            IEnumerable<SelectListItem> countySelectListItems = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(selectedState))
            {
                countySelectListItems = _stateService.GetCountiesByState(selectedState).Select(county => new SelectListItem
                {
                    Value = "County", // Assuming county name is used as the value
                    Text = "County"
                });
            }

            // var viewModel = new PaginatedRentalViewModel(rentalViews,new PaginationModel(totalCount, pageNumber, pageSize),states,selectedState, countySelectListItems, countyFilter); 

            // **Modified Section**: Include current filters in the view model
            var viewModel = new PaginatedRentalViewModel(rentalViews, new PaginationModel(totalCount, pageNumber, pageSize), states, selectedState, countySelectListItems, countyFilter)
            {
                CurrentFilters = new FilterParameters
                {
                    PropertyType = propertyTypeFilter,
                    State = stateFilter,
                    County = countyFilter,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    SortOrder = sortOrder
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CompareProperties(int[] selectedProperties)
        {
            if (selectedProperties == null || selectedProperties.Length > 3)
            {
                return RedirectToAction("Index");
            }

            List<ComparableProperty> propertiesToCompare = new List<ComparableProperty>();

            foreach (var Zpid in selectedProperties)
            {
                var rentalListing = _context.RentalListings.Find(Zpid);
                if (rentalListing != null)
                {
                    var calculatedData = GetCalculatedPropertyData(Zpid); // Call to stored procedure

                    var comparableProperty = new ComparableProperty
                    {
                        ZipID = calculatedData!.ZipID,
                        StreetAddress = calculatedData.StreetAddress,
                        PropertyType = calculatedData.PropertyType,
                        Bathrooms = calculatedData.Bathrooms,
                        Bedrooms = calculatedData.Bedrooms,
                        ImgSrc = calculatedData.ImgSrc,
                        Price = calculatedData.Price,
                        TaxAssessedValue = calculatedData.TaxAssessedValue,
                        Downpayment = calculatedData.Downpayment,
                        EstimatedMortgageCost = calculatedData.EstimatedMortgageCost,
                        EstimatedInsuranceCost = calculatedData.EstimatedInsuranceCost,
                        EstimatedRent = calculatedData.EstimatedRent,
                        HOAEstimate = calculatedData.HOAEstimate,
                        MonthlyPMI = calculatedData.MonthlyPMI

                    };

                    propertiesToCompare.Add(comparableProperty);
                }
            }

            var comparisonViewModel = new ComparisonViewModel(propertiesToCompare);
            return View("ComparisonView", comparisonViewModel);
        }

        private ComparableProperty GetCalculatedPropertyData(int Zpid)
        {
            try
            {
            var calculatedData = _context.Database.SqlQueryRaw<ComparableProperty>
            ("EXEC dbo.CalculatedPropertyData @Zpid", new SqlParameter("Zpid", Zpid))
            .AsEnumerable() // or AsNoTracking()
            .FirstOrDefault();

                return calculatedData!;


            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching calculated property data for Zpid: {Zpid}", Zpid);
                return null!;
            }
           
        }

        [HttpPost]
        public ActionResult SaveSelectedProperties(int[] selectedProperties, decimal[] insuranceCosts, decimal[] hoa, decimal[] estimatedRent)
        {
            if (selectedProperties == null || selectedProperties.Length == 0)
            {
                // Handle the case when no properties are selected
                return RedirectToAction("ComparisonView"); // Replace "ComparisonView" with your view's name
            }

            if (insuranceCosts.Length != selectedProperties.Length || hoa.Length != selectedProperties.Length|| estimatedRent.Length!=selectedProperties.Length)
            {
                // Handle the case when the lengths of the arrays do not match
                // This is important to avoid mismatches between the IDs and values
                return RedirectToAction("ComparisonView"); // Replace with the appropriate error handling view
            }

            try
            {
                for (int i = 0; i < selectedProperties.Length; i++)
                {
                    int propertyId = selectedProperties[i];
                    decimal insuranceCost = insuranceCosts[i];
                    decimal hoaValue = hoa[i];
                    decimal estimatedRentvalue = estimatedRent[i];

                    // Construct the parameters for the stored procedure call
                    var parameters = new[]
                    {
                new SqlParameter("@ZipID", propertyId),
                new SqlParameter("@InsuranceCosts", insuranceCost),
                new SqlParameter("@HOA", hoaValue),
                 new SqlParameter("@EstimatedRent", estimatedRentvalue)
            };

                    // Call the stored procedure for each property ID with the new HOA and Insurance Costs values
                    _context.Database.ExecuteSqlRaw("EXEC dbo.UpdateSelectedProperties @ZipID, @InsuranceCosts, @HOA,@EstimatedRent", parameters);
                }

                // Optionally, add a success message to TempData or ViewBag
                return RedirectToAction("Index", "Home"); // Redirect to a view that shows a success message
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or simply output the message)
                Console.WriteLine(ex.Message);

                // Optionally, add an error message to TempData or ViewBag
                return RedirectToAction("ErrorView"); // Redirect to a view that shows an error message
            }
        }

        public IActionResult GetStates()
        {
            // Assuming _stateService is your service that retrieves state information
            var states = _stateService.GetStates();

            // Populate the ViewData or ViewBag with the states list
            ViewData["stateSelector"] = new SelectList(states, "Value", "Text");

            // Return your view here
            return View();
        }

        public IActionResult GetCounties(string stateCode)
        {
            var counties = _stateService.GetCountiesByState(stateCode);
            return Json(counties);
        }



    }


}
