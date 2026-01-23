# Architecture

## High-level flow
UI (Razor Views + JavaScript) → Controllers → Services → Data Access → SQL Database.

- UI renders property listings, filters, and comparison pages.
- Controllers orchestrate requests and map to view models.
- Services encapsulate domain logic (TBD: define boundaries for external APIs).
- Data access layer and EF Core manage persistence to SQL databases.

## Mermaid diagram
```mermaid
flowchart LR
  UI[UI: Razor Views + JS] --> C[Controllers]
  C --> S[Services]
  S --> DAL[Data Access Layer / EF Core]
  DAL --> DB[(SQL Database)]

  %% External ETL is outside this repo
  ETL[External ETL (Zillow API ingestion)] --> DB
```

## TBD / Unknowns
- Exact service boundaries and any external API clients present in this repo.
- Detailed DB schema and how ETL jobs map Zillow data into tables.
- Hosting/deployment architecture (Azure/App Service/etc.).
