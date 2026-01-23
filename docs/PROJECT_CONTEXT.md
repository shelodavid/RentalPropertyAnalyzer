# Project Context

## Purpose
The Rental Property Analyzer is a web application that helps users filter, sort, and analyze rental property listings to evaluate rental potential. The project aims to ingest data from a database populated by an external ETL that calls a Zillow API, then present analysis in the web UI.

## Primary user flows
- Filter properties by state, county, property type, and price range.
- Sort listings (e.g., by zip code or rent price ratio).
- Paginate through results while retaining filters/sorts.
- Compare selected properties side-by-side.

## Current features vs. planned
**Current (implemented in repo):**
- Filtering, sorting, pagination, comparison, and dynamic county dropdowns.

**Planned (per product context):**
- “My Properties” page to save properties.
- “Generate Purchase Sheet” page with configurable inputs (e.g., down payment % slider).
- UI refactor for usability.
- Social login (Facebook) and a Settings page for user preferences.
- Future portability to Android/iOS.

## Data sources
- Primary data source is a SQL database populated by a separate ETL that calls a Zillow API. The ETL is external to this repository (TBD: ETL repo/location, job schedule, and schema details).

## How to run locally
- Prerequisites (from README): .NET SDK 6.0+ and Visual Studio 2022.
- Configure database connection strings in `appsettings.json`.
- Run Entity Framework migrations (TBD: confirm migrations and setup steps).
- Start the app via Visual Studio (TBD: CLI run instructions/ports).

## Configuration points
- `appsettings.json` contains connection strings for `DefaultConnection` and `UsersConnection` (update for your local SQL Server instance).
- Logging settings live in `appsettings.json` and `appsettings.Development.json`.

## Where key code lives
- **Entry point**: `Program.cs`.
- **Controllers**: `RentalPropertyAnalyzer/Controllers` (e.g., `RentalListingsController.cs`).
- **Models**: `RentalPropertyAnalyzer/Models` (view models and filter parameters).
- **Views/UI**: `RentalPropertyAnalyzer/Views` (Razor views like listing and comparison pages).
- **Services**: `RentalPropertyAnalyzer/Services` (e.g., Rentometer integration, state lookup, password hashing).
- **Data access**: `RentalPropertyAnalyzer/DataAccessLayer` (TBD: confirm repositories/contexts and intended boundaries).
