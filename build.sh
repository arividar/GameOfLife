#!/bin/bash
set -e

echo "🔨 Building GameOfLife solution..."
dotnet build

echo "🧪 Running tests with coverage..."
dotnet test

echo "📊 Generating coverage report..."
reportgenerator \
    -reports:"GameOfLifeTests/TestResults/coverage.cobertura.xml" \
    -targetdir:"./" \
    -reporttypes:"TextSummary" \
    -title:"GameOfLife Coverage Report"

echo "✅ Build pipeline completed successfully!"
echo "📋 Test Coverage Summary:"
cat ./Summary.txt
