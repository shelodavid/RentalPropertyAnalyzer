# Definition of Done (v1)

## v1 Complete criteria (measurable)
- CI pipeline is green on `master` (restore/build/test all pass).
- App runs locally from a clean clone using documented steps.
- Core workflows validated:
  - Filtering and search of properties.
  - Sorting and pagination.
  - Property comparison.
- “Generate Purchase Sheet” page is implemented and produces an end-to-end cost document.
- Purchase sheet inputs are configurable (e.g., down payment percentage slider).
- Basic auth/login is implemented, including social login (Facebook).
- Settings page exists for user preferences and persists changes.
- UI refactor baseline completed (agreed UX checklist or before/after review completed).

## Porting-readiness checklist
- All UI routes have documented endpoints and navigation map.
- API/service boundaries are defined and can be called without Razor views (TBD: confirm API surfaces).
- Data models and DTOs are documented and versioned.
- Authentication approach is portable (token-based or equivalent) and documented.
- Mobile-friendly design baseline verified (responsive layout or API-first design).
- Build artifacts and environment configuration documented for Android/iOS handoff.
