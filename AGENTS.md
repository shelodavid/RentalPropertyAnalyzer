# RentalPropertyAnalyzer — Agent Instructions

## Operating rules
- Always work in a new branch; never commit directly to master for code changes.
- Keep PRs small and reviewable (<300 LOC when possible).
- Preserve app behavior unless the task is explicitly to change behavior.
- Always keep the solution building.

## Required checks
- dotnet restore
- dotnet build -c Release
- dotnet test -c Release (if tests exist)

## Build-first policy
If the solution does not compile, fix compilation first before adding features.

## PR expectations
- Summary of changes
- Why the change was necessary
- Exact commands to verify locally
