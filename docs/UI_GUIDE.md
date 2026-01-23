# UI Guide

## Typography
- Base font: Nunito with system sans-serif fallback.
- Base size: 16px, line-height 1.6 for readability.
- Headings use Bootstrap sizing (`h1`–`h6`); prefer `h4`/`h5` for page headers.

## Spacing
- Use Bootstrap spacing utilities (`m*`, `p*`, `gap-*`) to keep rhythm consistent.
- Standard section spacing: `mb-4` for major blocks, `mb-3` for minor blocks.
- Avoid inline styles; extend `wwwroot/css/site.css` with scoped rules when needed.

## Buttons
- Use Bootstrap variants:
  - Primary: `.btn.btn-primary`
  - Secondary: `.btn.btn-secondary`
  - Danger: `.btn.btn-danger`
- Keep button groups aligned with `.d-flex` + `.gap-2` or `.w-100`.

## Tables
- Use `.table`, `.table-striped`, `.table-hover`, and `.align-middle`.
- Apply `.table-app` for shared table styling (rounded corners and header styling).
- Wrap tables with `.table-responsive` for mobile.

## Cards and sections
- Cards use `.card` with `.shadow-sm` for subtle elevation.
- Use `.stat-card` for KPI tiles and `.app-section` for layout sections.
- Keep content in cards grouped by purpose (filters, results, summaries).

## Layout regions
- **Sidebar**: `.app-sidebar` with fixed width and stacked nav links.
- **Top bar**: `.app-topbar` for page titles and context.
- **Main content**: `.app-main` provides consistent padding and background.

## Optional layout sections
The layout supports optional sections to avoid runtime errors when pages omit them:
- `@section StatsSection { ... }`
- `@section FilterSection { ... }`
- `@section PurchaseOverviewSection { ... }`
- `@section ControlsSection { ... }`
- `@section SlidersSection { ... }`

When a section is not defined in a view, the layout skips it safely.

## Shared partials
- `_PageHeader.cshtml`: title + subtitle + optional action slot.
- `_StatCard.cshtml`: label/value KPI tile with optional icon.
- `_FormRow.cshtml`: label + input + validation wrapper.

## Guidance for future PRs
- Start with `_Layout.cshtml` and `site.css` when adjusting layout or typography.
- Prefer Bootstrap utilities before adding new CSS rules.
- Validate pages visually after layout changes: Home, Property Search, My Properties, Purchase Sheet.
