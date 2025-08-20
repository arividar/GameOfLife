# Plan to Upgrade to .NET 9.0

This document outlines the plan to upgrade the codebase from .NET 8.0 to .NET 9.0.

## 1. Identify Projects to Upgrade

The following projects were identified in the solution:

- `GameOfLife/GameOfLife.csproj`
- `GameOfLifeTests/GameOfLifeTests.csproj`

Both projects were targeting `net8.0`.

## 2. Update Project Files

The `TargetFramework` property in both `.csproj` files needs to be updated from `net8.0` to `net9.0`.

```xml
<PropertyGroup>
  <TargetFramework>net9.0</TargetFramework>
</PropertyGroup>
```

## 3. Build and Test

After updating the project files, the solution must be built and all tests must be run to ensure that the upgrade did not introduce any breaking changes or regressions.

- **Build the solution:** `dotnet build`
- **Run tests:** `dotnet test`

In the case of this project, the build was successful and all tests passed after the upgrade.

## 4. Potential Blockers

For this specific project, no blockers were identified. The upgrade was seamless.

For more complex projects, potential blockers could include:

- **Breaking Changes in .NET 9.0:** The official .NET documentation should be consulted for any breaking changes between .NET 8.0 and .NET 9.0 that might affect the project.
- **Third-party Dependencies:** If the project has dependencies on third-party libraries, it's important to check if they are compatible with .NET 9.0. Some libraries might need to be updated to a newer version.
- **Deprecated APIs:** The code might be using APIs that are deprecated in .NET 9.0. These would need to be replaced with the recommended alternatives.

## 5. Environment Setup

To build and run the project with .NET 9.0, the .NET 9.0 SDK must be installed. This can be done using the `dotnet-install` scripts provided by Microsoft.
