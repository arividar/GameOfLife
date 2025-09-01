# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a C# implementation of Conway's Game of Life using .NET 9.0 with a console application and comprehensive test suite.

**Core Components:**
- `GameOfLife/GameOfLifeBoard.cs` - Main game logic with `GameOfLifeBoard` class implementing Conway's Game of Life rules
- `GameOfLife/ConsoleRenderer.cs` - Advanced terminal rendering system with cross-platform support, Unicode fallbacks, ANSI colors, performance optimization, and smooth animation
- `GameOfLife/Program.cs` - Console application entry point with predefined initial pattern and 20-generation simulation
- `GameOfLifeTests/GameOfLifeTests.cs` - MSTest unit tests covering all game rules and edge cases
- `GameOfLifeTests/ConsoleRendererTests.cs` - Comprehensive MSTest unit tests for terminal rendering functionality (209+ tests)

**Key Architecture:**
- `CellStatus` enum: `Alive` or `Dead` cell states
- `GameOfLifeBoard` class: Rectangular/square grid with methods for cell manipulation, neighbor counting, and generation advancement
- `ConsoleRenderer` class: Advanced terminal display system with:
  - **Cross-platform compatibility**: Automatic detection of terminal capabilities
  - **Unicode with ASCII fallbacks**: `██` `··` → `OO` `..` when Unicode unavailable
  - **ANSI colors with fallbacks**: Green/gray colors with plain text fallback
  - **Performance optimization**: Batch rendering, differential updates, optimized screen clearing
  - **Professional presentation**: Centered game area, Unicode borders, smooth animation
- Immutable generation progression: `NextGenerationBoard()` returns new board instance
- **Visual Rendering**: Double-width cells for better proportions and visual clarity

## Visual Features & Terminal Compatibility

### Modern Terminal Graphics
The ConsoleRenderer provides a modern, visually appealing Game of Life experience:

**Unicode Mode (Modern Terminals):**
```
┌────────────────────┐
│██··██··············│
│··████··············│
│██████··············│
│··················  │
└────────────────────┘
Generation 5
```

**ASCII Fallback Mode (Legacy Terminals):**
```
+--------------------+
|OO..OO..............|
|..OOOO..............|
|OOOOOO..............|
|..................  |
+--------------------+
Generation 5
```

### Cross-Platform Compatibility

**Automatic Terminal Detection:**
- **Unicode Support**: Detects UTF-8 encoding and modern terminal capabilities
- **Color Support**: Checks TERM, COLORTERM environment variables and NO_COLOR compliance
- **Graceful Fallbacks**: Automatically switches to ASCII characters and plain text when needed

**Supported Platforms:**
- ✅ **Windows**: Windows Terminal, PowerShell, Command Prompt
- ✅ **macOS**: Terminal.app, iTerm2, VS Code integrated terminal
- ✅ **Linux**: xterm, gnome-terminal, konsole, tmux, screen

**Environment Variable Support:**
- `TERM` - Terminal type detection (xterm, screen, tmux)
- `COLORTERM` - Color capability indication
- `NO_COLOR` - Accessibility compliance (disables colors when set)
- `TERM_PROGRAM` - Modern terminal detection (iTerm, VS Code)

### Performance Features

**Optimized Rendering:**
- **Batch Updates**: Collects all changes before applying to minimize I/O
- **Differential Rendering**: Only updates cells that changed between generations
- **Smart Cursor Movement**: Sorts updates by position to reduce cursor jumps
- **ANSI Optimizations**: Uses escape sequences like `\x1b[K` for faster screen clearing
- **Flicker Reduction**: Alternate screen buffer support and enhanced cursor hiding

**Animation Control:**
- **Configurable Delay**: Default 500ms between generations (adjustable)
- **Smooth Transitions**: Cell-by-cell updates instead of full screen refreshes
- **Centered Display**: Automatically centers game area in terminal window

### Visual Configuration Options

The ConsoleRenderer supports multiple configuration modes:

```csharp
var renderer = new ConsoleRenderer();

// Force ASCII mode for compatibility testing
renderer.ForceAsciiMode();

// Manual capability override
renderer.SetUnicodeCharactersEnabled(false);
renderer.SetAnsiColorsEnabled(false);

// Animation speed control
renderer.SetAnimationDelay(250); // Faster animation

// Test terminal capabilities
renderer.TestUnicodeOutput();
string info = renderer.GetPlatformInfo();
```

## Development Commands

**Build and Run:**
```bash
dotnet build                                           # Build entire solution
dotnet run --project GameOfLife/GameOfLife.csproj     # Run console simulation
dotnet test                                           # Run tests with coverage (80% threshold)
```

**Coverage and Quality:**
```bash
./build.sh                                            # Complete build pipeline (Unix/macOS)
build.bat                                             # Complete build pipeline (Windows)
reportgenerator -reports:GameOfLifeTests/TestResults/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html  # Generate coverage report only
```

**Coverage Integration:**
- Coverlet automatically collects coverage during `dotnet test`
- 80% line coverage threshold enforced (build fails if not met)
- Coverage includes all source files (GameOfLifeBoard.cs, ConsoleRenderer.cs, and Program.cs)
- Program.Main() method excluded from coverage analysis via ExcludeFromCodeCoverage attribute
- Build scripts generate text summary reports (`Summary.txt`)
- HTML coverage reports can be generated with: `reportgenerator -reports:GameOfLifeTests/TestResults/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html`
- Coverage artifacts excluded from version control

## Usage Examples

### Running in Different Terminal Environments

**Modern Terminal (Unicode + Colors):**
```bash
# Run with full visual features
dotnet run --project GameOfLife/GameOfLife.csproj

# Output: Beautiful Unicode graphics with colors
# ┌████████████████┐
# │██··██··········│ (Green alive cells, gray dead cells)
# │··████··········│
# └████████████████┘
```

**Legacy Terminal (ASCII fallback):**
```bash
# Disable colors and Unicode
NO_COLOR=1 TERM=dumb dotnet run --project GameOfLife/GameOfLife.csproj

# Output: Plain ASCII graphics
# +----------------+
# |OO..OO..........|
# |..OOOO..........|
# +----------------+
```

**Testing Terminal Compatibility:**
```bash
# Test in different terminal configurations
TERM=xterm-256color dotnet run --project GameOfLife/GameOfLife.csproj
TERM=screen dotnet run --project GameOfLife/GameOfLife.csproj  
TERM=dumb dotnet run --project GameOfLife/GameOfLife.csproj
```

### Performance Testing

**Animation Speed Testing:**
```bash
# Default speed (500ms delay)
dotnet run --project GameOfLife/GameOfLife.csproj

# The ConsoleRenderer automatically optimizes for:
# - Minimal screen refreshes (only changed cells)
# - Efficient cursor movement patterns
# - ANSI escape sequence optimizations
# - Reduced flicker through cursor management
```

### Terminal Size Adaptation

The game automatically adapts to different terminal sizes:
- **Large terminals**: Uses optimal board size with centering
- **Small terminals**: Scales down appropriately while maintaining minimum playable size
- **Minimum requirements**: 24x12 characters (for 10x10 board + borders + UI)

**Testing Different Sizes:**
```bash
# Resize terminal window and run - game auto-centers
resize -s 30 80  # 30 rows, 80 columns (if resize command available)
dotnet run --project GameOfLife/GameOfLife.csproj
```

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

## ConsoleRenderer API Reference

### Core Rendering Methods
```csharp
// Screen management
public void InitializeConsole()           // Setup terminal with optimal settings
public void RestoreConsole()             // Restore original terminal state
public void ClearScreen()                // Clear entire screen
public void DetectTerminalCapabilities() // Auto-detect Unicode/color support

// Cell rendering
public string RenderCell(CellStatus status)                    // Format cell with color/character
public void RenderCellAtPosition(int x, int y, CellStatus status, int offsetX, int offsetY)

// Board rendering
public void RenderBoardSmooth(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int startX, int startY)
public void BatchRenderCells(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int startX, int startY)
public void OptimizedRenderBoardWithBorder(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int offsetX, int offsetY)

// Border and layout
public void DrawBorder(int boardWidth, int boardHeight, int startX, int startY)
public (int x, int y) CalculateCenteredBoardPosition(int boardWidth, int boardHeight)
public (int width, int height) GetOptimalBoardSizeForTerminal(int terminalWidth, int terminalHeight)

// Performance optimization
public void OptimizedClearGameArea(int boardWidth, int boardHeight, int startX, int startY)
public void MinimizeScreenFlicker()      // Enable alternate screen buffer
public void RestoreScreenBuffer()       // Restore main screen buffer
```

### Configuration and Capability Testing
```csharp
// Manual configuration
public void SetUnicodeCharactersEnabled(bool enabled)  // Force Unicode on/off
public void SetAnsiColorsEnabled(bool enabled)        // Force colors on/off
public void ForceAsciiMode()                          // Disable Unicode and colors
public void SetAnimationDelay(int milliseconds)       // Control animation speed

// Capability detection
public bool CanUseUnicodeCharacters()    // Check Unicode support
public bool CanUseAnsiColors()          // Check ANSI color support
public string GetPlatformInfo()         // Get detailed platform info
public void TestUnicodeOutput()         // Output test characters

// Character and color retrieval
public string GetAliveCellCharacter()    // Returns "██" or "OO"
public string GetDeadCellCharacter()     // Returns "··" or ".."
public string GetAliveCellColor()        // Returns ANSI green or ""
public string GetDeadCellColor()         // Returns ANSI gray or ""
```

## Testing Capabilities

**Comprehensive Test Suite (209+ Tests):**
- ✅ Cross-platform compatibility testing
- ✅ Unicode/ASCII fallback verification
- ✅ ANSI color on/off functionality
- ✅ Performance optimization validation
- ✅ Error handling and graceful degradation
- ✅ Terminal size adaptation testing
- ✅ Environment variable detection

**Testing Commands:**
```bash
# Run all tests with coverage
dotnet test

# Generate detailed coverage report
reportgenerator -reports:GameOfLifeTests/TestResults/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html

# Test specific functionality areas
dotnet test --filter "TestCategory=CrossPlatform"
dotnet test --filter "TestCategory=Performance"
dotnet test --filter "TestCategory=Rendering"
```