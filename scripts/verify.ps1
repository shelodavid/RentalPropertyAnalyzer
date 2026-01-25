Write-Host "Running: dotnet restore"
dotnet restore
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Running: dotnet build -c Release"
dotnet build -c Release
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "Running: dotnet test -c Release"
dotnet test -c Release
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
