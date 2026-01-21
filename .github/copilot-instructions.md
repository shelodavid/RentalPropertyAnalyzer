You are working on a .NET (ASP.NET Core MVC) solution: RentalPropertyAnalyzer.

Priorities:
- Make and keep the solution compiling locally and in GitHub Actions.
- Prefer incremental fixes with clear root-cause explanations.
- Avoid new dependencies unless required to restore/build.

Rules:
- Use the SDK pinned by global.json.
- Create a new branch for each change and open a PR.
- Provide verification steps: dotnet restore/build/test.
