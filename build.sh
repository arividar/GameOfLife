#!/bin/bash
set -e

echo "🔨 Building GameOfLife solution..."
dotnet build

echo "🧪 Running tests with coverage..."
dotnet test

echo "📊 Generating coverage report..."
reportgenerator \
  -reports:"GameOfLifeTests/TestResults/coverage.cobertura.xml" \
  -targetdir:"coverage-report" \
  -reporttypes:"Html;TextSummary" \
  -title:"GameOfLife Coverage Report"

echo "✅ Build pipeline completed successfully!"
echo ""
echo "📈 Coverage report generated at: coverage-report/index.html"
echo "📋 Summary:"
cat coverage-report/Summary.txt