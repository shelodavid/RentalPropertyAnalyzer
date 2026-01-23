# ROI Forecast

## Overview
The ROI Forecast page provides a high-level view of monthly and annual returns for a saved property. It uses the property’s estimated rent, financing inputs, and the monthly recurring costs configured in the Investment Profile to compute cashflow and return metrics.

## Forecast assumptions
- **Rent**: Uses the saved property’s estimated monthly rent.
- **Financing**: Uses the first Investment Profile’s downpayment %, interest rate, and term (defaults to 20%, 6.5%, 30 years if missing).
- **Recurring costs**: Property taxes (rate), homeowners insurance (annual), PMI (rate), maintenance, utilities, HOA, vacancy %, and management fee.
- **Cash to close**: Downpayment + closing costs (from Investment Profile) + prepaids (defaults to 0 if missing). If a Purchase Sheet is not available, the value is marked as estimated.

## Outputs
- Monthly mortgage principal + interest (P&I)
- Total monthly expenses
- Monthly and annual net cashflow
- Cash-on-cash return %
- Cap rate %
- Break-even rent

## Purchase Sheet additions
The Purchase Sheet now includes common **Prepaids & Escrow** and **Government Fees** inputs:
- Prepaid interest days
- Annual homeowners insurance
- Escrow months for taxes and insurance
- Recording fees
- Transfer taxes

These values are included in the cash-to-close total.

## Manual smoke test
1. Start the app.
2. Navigate to **Home**, **Property Search**, **Investment Profile**, **Purchase Sheet**, and **ROI Forecast**.
3. Confirm each page renders without runtime exceptions.
4. In ROI Forecast, select a saved property and verify the summary metrics update.
