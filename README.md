# Rental Property Analyzer

## Description
The **Rental Property Analyzer** is a web application designed to help users filter, sort, and analyze rental property listings based on various criteria. It supports pagination, advanced filtering, and sorting capabilities, making it easy to evaluate properties based on metrics like rent price ratio, property type, location, and price range.

## Features
- **Filter Properties**: Filter listings by state, county, property type, and price range.
- **Dynamic Sorting**: Sort results by zip code or rent price ratio (ascending or descending).
- **Pagination**: View results across multiple pages while preserving filter and sorting states.
- **Comparison**: Select properties to compare side-by-side for in-depth analysis.
- **Dynamic Dropdowns**: Populate county options dynamically based on selected state.

## Technology Stack
- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Views, Bootstrap for styling, jQuery for dynamic behavior
- **Database**: Entity Framework Core with a SQL database
- **API Integration**: County data dynamically loaded via AJAX calls.

## Setup Instructions

### Prerequisites
1. .NET Core SDK 6.0 or later
2. Visual Studio 2022 (Community or higher)
3. SQL Server or compatible database
4. Git for version control

### Clone the Repository
```bash
git clone https://github.com/your-username/rental-property-analyzer.git
cd rental-property-analyzer
# RentalPropertyAnalyzer

### Database Configuration
1. Update the appsettings.json file with your database connection string:

json
Copy code
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;"
    }
}

2. Run migrations to set up the database:

bash
Copy code
dotnet ef database update
Run the Application
Open the solution in Visual Studio.
Set RentalPropertyAnalyzer as the startup project.
Press F5 or run the application.

### Usage Instructions
Filtering Properties
Use the filters on the top of the page:
State: Select a state from the dropdown.
County: Counties are dynamically loaded based on the selected state.
Property Type: Choose from options like Townhouse, Condo, Single Family, etc.
Price Range: Enter minimum and maximum prices.
Sorting Properties
Click on column headers (e.g., Rent Price Ratio) to sort properties.
Sorting preserves the currently applied filters.
Pagination
Use the Previous and Next buttons to navigate through pages while maintaining filter and sort states.
Compare Properties
Select up to three properties using the checkboxes and click Compare Selected Properties to view detailed side-by-side comparisons.

### Folder Structure
bash
Copy code
├── Controllers
│   ├── RentalListingsController.cs    # Handles business logic for property listings
├── Models
│   ├── RentalViewModel.cs             # View model for property data
│   ├── PaginatedRentalViewModel.cs    # View model for pagination and filters
│   ├── FilterParameters.cs            # Encapsulates filter criteria
├── Views
│   ├── RentalListings
│       ├── Index.cshtml               # Main view for property listings
│       ├── ComparisonView.cshtml      # View for comparing selected properties
├── wwwroot
│   ├── js                             # Custom JavaScript
│   ├── css                            # Custom CSS


### Key Classes
RentalListingsController
Handles filtering, sorting, and pagination.
Retrieves property data from the database using LINQ queries.
PaginatedRentalViewModel
Combines property data with pagination and filter metadata.
FilterParameters
Encapsulates filtering inputs (e.g., minPrice, maxPrice, etc.) for consistent usage across the application.
Contributing
We welcome contributions to enhance this project! Follow these steps to get started:

Fork the repository.
Create a feature branch:
bash
Copy code
git checkout -b feature/your-feature
Commit your changes and push the branch:
bash
Copy code
git push origin feature/your-feature
Create a pull request.
License
This project is licensed under the MIT License. See the LICENSE file for more details.

Acknowledgments
Special thanks to the contributors and open-source libraries that made this project possible:

Bootstrap: For responsive design
jQuery: For dynamic dropdowns and AJAX functionality
Entity Framework Core: For database management
