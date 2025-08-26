@echo off
setlocal enabledelayedexpansion

echo 🔨 Building GameOfLife solution...
dotnet build
if !errorlevel! neq 0 (
    echo ❌ Build failed
    exit /b !errorlevel!
)

echo 🧪 Running tests with coverage...
dotnet test
if !errorlevel! neq 0 (
    echo ❌ Tests failed or coverage threshold not met
    exit /b !errorlevel!
)

echo 📊 Generating coverage report...
reportgenerator ^
  -reports:"GameOfLifeTests\TestResults\coverage.cobertura.xml" ^
  -targetdir:".\" ^
  -reporttypes:"TextSummary" ^
  -title:"GameOfLife Coverage Report"
if !errorlevel! neq 0 (
    echo ❌ Report generation failed
    exit /b !errorlevel!
)

echo ✅ Build pipeline completed successfully!
echo.
echo 📈 Coverage report generated at: coverage-report\index.html
echo 📋 Summary:
type .\Summary.txt
