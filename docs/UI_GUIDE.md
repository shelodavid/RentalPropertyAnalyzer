# UI Guide

## Typography
- Base font: Nunito (fallback to system sans-serif).
- Default body size: 16px with a 1.6 line-height.
- Headings use Bootstrap sizing (`h1`–`h6`) for hierarchy.

## Spacing
- Use Bootstrap spacing utilities (`m*`, `p*`, `gap-*`) for layout rhythm.
- Prefer `g-4` for card grids and `mb-3`/`mb-4` for section spacing.
- Avoid inline styles; add shared rules to `wwwroot/css/site.css`.

## Buttons
- Use Bootstrap button variants for consistent intent:
  - Primary: `.btn.btn-primary`
  - Secondary: `.btn.btn-secondary`
  - Danger: `.btn.btn-danger`
- Keep buttons aligned with `.d-flex` + `.gap-2` or `.w-100` where needed.

## Tables
- Use `.table`, `.table-striped`, `.table-hover`, and `.align-middle`.
- Add `.table-app` for shared UI polish (rounded corners and header styling).
- Wrap in `.table-responsive` for smaller screens.

## Layout regions
- **Sidebar**: `.app-sidebar` with fixed width (260px) and stacked nav links.
- **Top bar**: `.app-topbar` for page title/secondary context.
- **Main content**: `.app-main` provides consistent padding and background.

## Guidance for future PRs
- Keep UI changes centralized in `_Layout.cshtml` and `site.css` first.
- Prefer Bootstrap utilities before introducing new custom classes.
- Validate pages visually after layout changes (Home, Rental Listings, My Properties, Purchase Sheet).
