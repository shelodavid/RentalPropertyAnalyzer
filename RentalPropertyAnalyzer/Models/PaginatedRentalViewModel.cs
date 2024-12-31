using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using static RentalPropertyAnalyzer.Controllers.RentalListingsController;

namespace RentalPropertyAnalyzer.Models
{
    public class PaginatedRentalViewModel
    {
        public List<RentalViewModel> Rentals { get; set; }
        public PaginationModel Pagination { get; set; }
        public SelectList StateList { get; set; }
        public string SelectedState { get; set; }
        public SelectList CountyList { get; set; } // Added for counties
        public string SelectedCounty { get; set; } // Added for selected county


        // **Start of Update**: Add CurrentFilters property
        public FilterParameters CurrentFilters { get; set; }
        // **End of Update**

        // Updated constructor to include counties and selectedCounty
        public PaginatedRentalViewModel(
            List<RentalViewModel> rentals,
            PaginationModel pagination,
            IEnumerable<SelectListItem> states,
            string selectedState = null,
            IEnumerable<SelectListItem> counties = null, // Optional, can be null if no state is selected
            string selectedCounty = null) // Optional, can be null if no county is selected or available
        {
            Rentals = rentals;
            Pagination = pagination;
            StateList = new SelectList(states, "Value", "Text", selectedState);
            SelectedState = selectedState;
            CountyList =  new SelectList(counties, "Value", "Text", selectedCounty); 
            SelectedCounty = selectedCounty;
        }
    }


}



