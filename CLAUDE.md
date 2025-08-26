# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a C# implementation of Conway's Game of Life using .NET 9.0 with a console application and comprehensive test suite.

**Core Components:**
- `GameOfLife/GameOfLifeBoard.cs` - Main game logic with `GameOfLifeBoard` class implementing Conway's Game of Life rules
- `GameOfLife/Program.cs` - Console application entry point with predefined initial pattern and 20-generation simulation
- `GameOfLifeTests/GameOfLifeTests.cs` - MSTest unit tests covering all game rules and edge cases

**Key Architecture:**
- `CellStatus` enum: `Alive` or `Dead` cell states
- `GameOfLifeBoard` class: Square grid with methods for cell manipulation, neighbor counting, and generation advancement
- Immutable generation progression: `NextGenerationBoard()` returns new board instance

## Development Commands

**Build and Run:**
```bash
dotnet build                                           # Build entire solution
dotnet run --project GameOfLife/GameOfLife.csproj     # Run console simulation
dotnet test                                           # Run tests with coverage (95% threshold)
```

**Coverage and Quality:**
```bash
./build.sh                                            # Complete build pipeline (Unix/macOS)
build.bat                                             # Complete build pipeline (Windows)
reportgenerator -reports:GameOfLifeTests/TestResults/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html  # Generate coverage report only
```

**Coverage Integration:**
- Coverlet automatically collects coverage during `dotnet test`
- 95% line coverage threshold enforced (build fails if not met)
- Program.cs (console entry point) excluded from coverage analysis
- HTML coverage reports generated in `coverage-report/index.html`
- Coverage artifacts excluded from version control

## Development Workflow

**This project follows strict Test-Driven Development (TDD)** as specified in AGENTS.md:

1. **Add failing test first** - Write test before implementation
2. **Run tests** - Verify new test fails (`dotnet test`)
3. **Write minimal code** - Make test pass with minimum changes
4. **Run tests again** - Ensure all tests pass
5. **Refactor** - Improve code structure while keeping tests green

**TDD Rules:**
- No new features without corresponding tests
- Fix bugs by first writing tests that reproduce them
- All tests must pass before submitting changes

## Test Framework

Uses MSTest with these key patterns:
- `[TestClass]` and `[TestMethod]` attributes
- `Assert.AreEqual()`, `Assert.IsTrue()`, `Assert.IsFalse()` for assertions
- Tests cover all Game of Life rules: underpopulation, survival, overpopulation, reproduction