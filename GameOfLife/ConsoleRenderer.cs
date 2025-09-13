using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public enum GenerationCounterPosition
    {
        Top,
        Bottom
    }

    public class ConsoleRenderer
    {
        private bool _originalCursorVisible;
        private Encoding _originalEncoding;
        private int _animationDelay = 500; // Default 500ms delay for smooth rendering
        private GenerationCounterPosition _generationCounterPosition = GenerationCounterPosition.Top; // Default to top position
        private bool _useUnicodeCharacters = true; // Default to Unicode, fallback to ASCII if needed
        private bool _useAnsiColors = true; // Default to ANSI colors, fallback to plain text if needed
        private bool _manuallyConfigured = false; // Track if settings were manually configured

        public void InitializeConsole()
        {
            try
            {
                // Store original state for restoration
                if (OperatingSystem.IsWindows())
                {
                    _originalCursorVisible = Console.CursorVisible;
                }
                _originalEncoding = Console.OutputEncoding;
                
                // Detect terminal capabilities only if not manually configured
                if (!_manuallyConfigured)
                {
                    DetectTerminalCapabilities();
                }
                
                // Set UTF-8 encoding for Unicode support
                SetEncoding();
                
                // Hide cursor for cleaner display
                HideCursor();
                
                // Clear screen for fresh start
                ClearScreen();
            }
            catch (Exception)
            {
                // Handle case where console initialization fails
                // This might happen on some terminals or when output is redirected
                // Fallback to safe ASCII mode
                _useUnicodeCharacters = false;
                _useAnsiColors = false;
            }
        }

        public void RestoreConsole()
        {
            try
            {
                // Restore original console state
                if (OperatingSystem.IsWindows())
                {
                    Console.CursorVisible = _originalCursorVisible;
                }
                Console.OutputEncoding = _originalEncoding ?? Console.OutputEncoding;
            }
            catch (Exception)
            {
                // Handle case where console restoration fails
                // This might happen on some terminals or when output is redirected
            }
        }

        public void DetectTerminalCapabilities()
        {
            try
            {
                // Check if we're running in a terminal that supports Unicode and ANSI colors
                _useUnicodeCharacters = CanUseUnicodeCharacters();
                _useAnsiColors = CanUseAnsiColors();
            }
            catch (Exception)
            {
                // If detection fails, use safe fallback
                _useUnicodeCharacters = false;
                _useAnsiColors = false;
            }
        }

        public bool CanUseUnicodeCharacters()
        {
            try
            {
                // Test if console supports UTF-8 encoding
                if (Console.OutputEncoding.EncodingName.Contains("UTF-8") || 
                    Console.OutputEncoding.EncodingName.Contains("Unicode"))
                {
                    return true;
                }

                // Check for common environment variables indicating Unicode support
                string term = Environment.GetEnvironmentVariable("TERM") ?? "";
                string termProgram = Environment.GetEnvironmentVariable("TERM_PROGRAM") ?? "";
                
                // Common terminals that support Unicode
                return term.Contains("xterm") || 
                       term.Contains("screen") || 
                       term.Contains("tmux") || 
                       termProgram.Contains("iTerm") ||
                       termProgram.Contains("Terminal") ||
                       termProgram.Contains("vscode") ||
                       OperatingSystem.IsWindows(); // Windows Terminal generally supports Unicode
            }
            catch (Exception)
            {
                return false; // Safe fallback
            }
        }

        public bool CanUseAnsiColors()
        {
            try
            {
                // Check for environment variables indicating color support
                string term = Environment.GetEnvironmentVariable("TERM") ?? "";
                string colorTerm = Environment.GetEnvironmentVariable("COLORTERM") ?? "";
                
                // Disable colors if NO_COLOR environment variable is set
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NO_COLOR")))
                {
                    return false;
                }

                // Common terminals that support ANSI colors
                return term.Contains("color") || 
                       term.Contains("xterm") || 
                       term.Contains("screen") || 
                       !string.IsNullOrEmpty(colorTerm) ||
                       OperatingSystem.IsWindows(); // Modern Windows supports ANSI
            }
            catch (Exception)
            {
                return false; // Safe fallback
            }
        }

        public (int width, int height) GetTerminalSize()
        {
            try
            {
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;
                return (width, height);
            }
            catch (Exception)
            {
                // Handle case where terminal size cannot be determined
                // Return fallback dimensions for basic functionality
                return (80, 25);
            }
        }

        public bool IsTerminalSizeValid(int width, int height)
        {
            // Define minimum requirements for Game of Life display
            const int MIN_WIDTH = 24;  // Minimum for 10x10 board (20 chars) + borders (2) + margin (2)
            const int MIN_HEIGHT = 12; // Minimum for 10x10 board + borders + generation info
            
            return width >= MIN_WIDTH && height >= MIN_HEIGHT;
        }

        public void ClearScreen()
        {
            try
            {
                Console.Clear();
            }
            catch (System.IO.IOException)
            {
                // Handle case where output is redirected to a file
                // In this case, we can't clear the screen, so we do nothing
            }
        }

        public void SetEncoding()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
            }
            catch (Exception)
            {
                // Handle case where encoding cannot be set
                // Fall back to default encoding
            }
        }

        public void HideCursor()
        {
            try
            {
                Console.CursorVisible = false;
                
                // Also try to move cursor to a position outside the visible area
                // This helps on terminals where CursorVisible doesn't work reliably
                var (width, height) = GetTerminalSize();
                Console.SetCursorPosition(0, height - 1);
            }
            catch (Exception)
            {
                // Handle case where cursor visibility cannot be controlled
                // This might happen on some terminals or when output is redirected
            }
        }

        public void ShowCursor()
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    Console.CursorVisible = true;
                }
            }
            catch (Exception)
            {
                // Handle case where cursor visibility cannot be controlled
                // This might happen on some terminals or when output is redirected
            }
        }

        public (int x, int y) CalculateCenter(int boardWidth, int boardHeight)
        {
            try
            {
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;
                
                int centerX = Math.Max(0, (consoleWidth - boardWidth) / 2);
                int centerY = Math.Max(0, (consoleHeight - boardHeight) / 2);
                
                return (centerX, centerY);
            }
            catch (Exception)
            {
                // Handle case where console dimensions cannot be determined
                // Fall back to origin (0, 0)
                return (0, 0);
            }
        }

        public void SetCursorToCenter(int boardSize)
        {
            try
            {
                var (centerX, centerY) = CalculateCenter(boardSize, boardSize);
                Console.SetCursorPosition(centerX, centerY);
            }
            catch (Exception)
            {
                // Handle case where cursor position cannot be set
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public string GetAliveCellCharacter()
        {
            return _useUnicodeCharacters ? "██" : "OO";
        }

        public string GetDeadCellCharacter()
        {
            return _useUnicodeCharacters ? "··" : "..";
        }

        public string GetAliveCellColor()
        {
            return _useAnsiColors ? "\x1b[32m" : "";
        }

        public string GetDeadCellColor()
        {
            return _useAnsiColors ? "\x1b[90m" : "";
        }

        public string GetColorReset()
        {
            return _useAnsiColors ? "\x1b[0m" : "";
        }

        public string RenderCell(CellStatus cellStatus)
        {
            if (cellStatus == CellStatus.Alive)
            {
                return GetAliveCellColor() + GetAliveCellCharacter() + GetColorReset();
            }
            else
            {
                return GetDeadCellColor() + GetDeadCellCharacter() + GetColorReset();
            }
        }

        public (int x, int y) CalculateBoardPosition(int boardWidth, int boardHeight, int terminalWidth, int terminalHeight)
        {
            int centerX = Math.Max(0, (terminalWidth - boardWidth) / 2);
            int centerY = Math.Max(0, (terminalHeight - boardHeight) / 2);
            return (centerX, centerY);
        }

        public void SetCursorToBoardPosition(int boardWidth, int boardHeight)
        {
            try
            {
                var (terminalWidth, terminalHeight) = GetTerminalSize();
                var (boardX, boardY) = CalculateBoardPosition(boardWidth, boardHeight, terminalWidth, terminalHeight);
                Console.SetCursorPosition(boardX, boardY);
            }
            catch (Exception)
            {
                // Handle case where cursor position cannot be set
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public void MoveCursorToPosition(int x, int y)
        {
            try
            {
                int safeX = Math.Max(0, x);
                int safeY = Math.Max(0, y);
                Console.SetCursorPosition(safeX, safeY);
            }
            catch (Exception)
            {
                // Handle case where cursor position cannot be set
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public string GetTopLeftBorderCharacter()
        {
            return _useUnicodeCharacters ? "┌" : "+";
        }

        public string GetTopRightBorderCharacter()
        {
            return _useUnicodeCharacters ? "┐" : "+";
        }

        public string GetBottomLeftBorderCharacter()
        {
            return _useUnicodeCharacters ? "└" : "+";
        }

        public string GetBottomRightBorderCharacter()
        {
            return _useUnicodeCharacters ? "┘" : "+";
        }

        public string GetHorizontalBorderCharacter()
        {
            return _useUnicodeCharacters ? "─" : "-";
        }

        public string GetVerticalBorderCharacter()
        {
            return _useUnicodeCharacters ? "│" : "|";
        }

        public (int width, int height) CalculateBorderDimensions(int boardWidth, int boardHeight)
        {
            return (boardWidth * 2 + 2, boardHeight + 2);
        }

        public void DrawBorder(int boardWidth, int boardHeight, int startX, int startY)
        {
            try
            {
                var (borderWidth, borderHeight) = CalculateBorderDimensions(boardWidth, boardHeight);
                
                // Draw top border
                MoveCursorToPosition(startX, startY);
                Console.Write(GetTopLeftBorderCharacter());
                for (int i = 0; i < boardWidth * 2; i++)
                {
                    Console.Write(GetHorizontalBorderCharacter());
                }
                Console.Write(GetTopRightBorderCharacter());
                
                // Draw side borders
                for (int row = 1; row < borderHeight - 1; row++)
                {
                    MoveCursorToPosition(startX, startY + row);
                    Console.Write(GetVerticalBorderCharacter());
                    MoveCursorToPosition(startX + borderWidth - 1, startY + row);
                    Console.Write(GetVerticalBorderCharacter());
                }
                
                // Draw bottom border
                MoveCursorToPosition(startX, startY + borderHeight - 1);
                Console.Write(GetBottomLeftBorderCharacter());
                for (int i = 0; i < boardWidth * 2; i++)
                {
                    Console.Write(GetHorizontalBorderCharacter());
                }
                Console.Write(GetBottomRightBorderCharacter());
            }
            catch (Exception)
            {
                // Handle case where border cannot be drawn
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public (int x, int y) CalculateCenteredBoardPosition(int boardWidth, int boardHeight)
        {
            var (terminalWidth, terminalHeight) = GetTerminalSize();
            return CalculateCenteredBoardPosition(boardWidth, boardHeight, terminalWidth, terminalHeight);
        }

        public (int x, int y) CalculateCenteredBoardPosition(int boardWidth, int boardHeight, int terminalWidth, int terminalHeight)
        {
            var (borderWidth, borderHeight) = CalculateBorderDimensions(boardWidth, boardHeight);
            
            int centerX = Math.Max(0, (terminalWidth - borderWidth) / 2);
            int centerY = Math.Max(0, (terminalHeight - borderHeight) / 2);
            
            return (centerX, centerY);
        }

        public bool IsBoardTooLargeForTerminal(int boardWidth, int boardHeight, int terminalWidth, int terminalHeight)
        {
            var (borderWidth, borderHeight) = CalculateBorderDimensions(boardWidth, boardHeight);
            return borderWidth > terminalWidth || borderHeight > terminalHeight;
        }

        public void SetCursorToCenteredBoardPosition(int boardWidth, int boardHeight)
        {
            try
            {
                var (centerX, centerY) = CalculateCenteredBoardPosition(boardWidth, boardHeight);
                Console.SetCursorPosition(centerX, centerY);
            }
            catch (Exception)
            {
                // Handle case where cursor position cannot be set
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public (int width, int height) GetOptimalBoardSizeForTerminal()
        {
            var (terminalWidth, terminalHeight) = GetTerminalSize();
            return GetOptimalBoardSizeForTerminal(terminalWidth, terminalHeight);
        }

        public (int width, int height) GetOptimalBoardSizeForTerminal(int terminalWidth, int terminalHeight)
        {
            // Leave room for borders (2 extra) plus some margin for readability
            int availableWidth = (terminalWidth - 4) / 2; // Divide by 2 for double-width cells, Border + 2 margin
            int availableHeight = terminalHeight - 4; // Border + 2 margin
            
            // Ensure minimum playable size
            int optimalWidth = Math.Max(10, Math.Min(availableWidth, 25));
            int optimalHeight = Math.Max(8, Math.Min(availableHeight, 30));
            
            return (optimalWidth, optimalHeight);
        }

        // Phase 3 Step 6: Smooth rendering methods
        
        public void RenderCellAtPosition(int cellX, int cellY, CellStatus cellStatus, int boardOffsetX, int boardOffsetY)
        {
            try
            {
                // Calculate the actual screen position for this cell
                var (screenX, screenY) = CalculateCellScreenPosition(cellX, cellY, boardOffsetX, boardOffsetY);
                
                // Move cursor to the cell position and render the cell
                MoveCursorToPosition(screenX, screenY);
                Console.Write(RenderCell(cellStatus));
            }
            catch (Exception)
            {
                // Handle case where cursor positioning or cell rendering fails
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public void ClearGameArea(int boardWidth, int boardHeight, int startX, int startY)
        {
            try
            {
                // Clear each row of the game area with spaces
                for (int row = 0; row < boardHeight; row++)
                {
                    MoveCursorToPosition(startX, startY + row);
                    
                    // Write spaces to clear the entire row
                    string clearRow = new string(' ', boardWidth);
                    Console.Write(clearRow);
                }
            }
            catch (Exception)
            {
                // Handle case where area clearing fails
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public void SetAnimationDelay(int milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentException("Animation delay cannot be negative", nameof(milliseconds));
            }
            _animationDelay = milliseconds;
        }

        public int GetAnimationDelay()
        {
            return _animationDelay;
        }

        public int GetDelayForSmoothRendering()
        {
            return _animationDelay;
        }

        public void ApplyAnimationDelay()
        {
            try
            {
                if (_animationDelay > 0)
                {
                    System.Threading.Thread.Sleep(_animationDelay);
                }
                
                // Re-hide cursor after delay in case it became visible during rendering
                EnsureCursorHidden();
            }
            catch (Exception)
            {
                // Handle case where thread sleep fails
                // Continue execution without delay
            }
        }
        
        public void EnsureCursorHidden()
        {
            try
            {
                Console.CursorVisible = false;
                
                // Move cursor to top-left corner to minimize visibility
                Console.SetCursorPosition(0, 0);
            }
            catch (Exception)
            {
                // Handle case where cursor control fails
            }
        }

        public void RenderBoardSmooth(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int startX, int startY)
        {
            try
            {
                // If no previous board, render all cells
                if (previousBoard == null)
                {
                    // Render the entire board from scratch
                    for (int x = 0; x < board.Width; x++)
                    {
                        for (int y = 0; y < board.Height; y++)
                        {
                            RenderCellAtPosition(x, y, board.GetCell(x, y), startX, startY);
                        }
                    }
                }
                else
                {
                    // Only update cells that have changed between generations
                    int maxWidth = Math.Max(board.Width, previousBoard.Width);
                    int maxHeight = Math.Max(board.Height, previousBoard.Height);
                    
                    for (int x = 0; x < maxWidth; x++)
                    {
                        for (int y = 0; y < maxHeight; y++)
                        {
                            CellStatus currentCell = (x < board.Width && y < board.Height) ? 
                                board.GetCell(x, y) : CellStatus.Dead;
                            CellStatus previousCell = (x < previousBoard.Width && y < previousBoard.Height) ? 
                                previousBoard.GetCell(x, y) : CellStatus.Dead;
                            
                            // Only render if the cell has changed
                            if (HasCellChanged(currentCell, previousCell))
                            {
                                RenderCellAtPosition(x, y, currentCell, startX, startY);
                            }
                        }
                    }
                }
                
                // Ensure cursor remains hidden after rendering
                EnsureCursorHidden();
            }
            catch (Exception)
            {
                // Handle case where smooth rendering fails
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public (int screenX, int screenY) CalculateCellScreenPosition(int boardX, int boardY, int offsetX, int offsetY)
        {
            return (boardX * 2 + offsetX, boardY + offsetY);
        }

        public bool HasCellChanged(CellStatus current, CellStatus previous)
        {
            return current != previous;
        }

        // Phase 4: Enhanced rendering methods for Program.cs integration

        public void RenderBoardWithBorder(GameOfLifeBoard board, int offsetX, int offsetY)
        {
            try
            {
                // Draw border around the board
                DrawBorder(board.Width, board.Height, offsetX, offsetY);
                
                // Render the board content inside the border using smooth rendering
                RenderBoardSmooth(board, null, offsetX + 1, offsetY + 1);
            }
            catch (Exception)
            {
                // Handle case where bordered rendering fails
                // Fall back to basic board rendering
                try
                {
                    for (int x = 0; x < board.Width; x++)
                    {
                        for (int y = 0; y < board.Height; y++)
                        {
                            MoveCursorToPosition(offsetX + y, offsetY + x);
                            Console.Write(RenderCell(board.GetCell(x, y)));
                        }
                    }
                }
                catch (Exception)
                {
                    // Ultimate fallback - do nothing
                }
            }
        }

        // Phase 3 Step 7: Generation Counter Display Methods

        public void RenderGenerationCounterAtTop(int generation, int boardOffsetX, int boardOffsetY)
        {
            try
            {
                var (x, y) = CalculateGenerationCounterTopPosition(boardOffsetX, boardOffsetY);
                MoveCursorToPosition(x, y);
                string formatted = FormatGenerationCounter(generation);
                Console.Write(formatted);
            }
            catch (Exception)
            {
                // Handle case where generation counter rendering fails
            }
        }

        public void RenderGenerationCounterAtBottom(int generation, int boardWidth, int boardHeight, int boardOffsetY)
        {
            try
            {
                var (x, y) = CalculateGenerationCounterBottomPosition(boardWidth, boardHeight, boardWidth, boardOffsetY);
                MoveCursorToPosition(x, y);
                string formatted = FormatGenerationCounter(generation);
                Console.Write(formatted);
            }
            catch (Exception)
            {
                // Handle case where generation counter rendering fails
            }
        }

        public (int x, int y) CalculateGenerationCounterTopPosition(int boardOffsetX, int boardOffsetY)
        {
            // Position counter one line above the game area
            int x = Math.Max(0, boardOffsetX);
            int y = Math.Max(0, boardOffsetY - 1);
            return (x, y);
        }

        public (int x, int y) CalculateGenerationCounterBottomPosition(int boardWidth, int boardHeight, int boardOffsetX, int boardOffsetY)
        {
            // Position counter one line below the game area
            int x = Math.Max(0, boardOffsetX);
            int y = boardOffsetY + boardHeight + 1;
            return (x, y);
        }

        public string FormatGenerationCounter(int generation)
        {
            return $"Generation {generation}";
        }

        public int GetGenerationCounterLength(int generation)
        {
            return FormatGenerationCounter(generation).Length;
        }

        public void ClearGenerationCounterArea(int x, int y, int length)
        {
            try
            {
                MoveCursorToPosition(x, y);
                Console.Write(new string(' ', length));
            }
            catch (Exception)
            {
                // Handle case where clearing fails
            }
        }

        public void SetGenerationCounterPosition(GenerationCounterPosition position)
        {
            _generationCounterPosition = position;
        }

        public GenerationCounterPosition GetGenerationCounterPosition()
        {
            return _generationCounterPosition;
        }

        public void RenderGenerationCounterWithFormatting(int generation, int x, int y, string format)
        {
            try
            {
                MoveCursorToPosition(x, y);
                string formatted = string.Format(format, generation);
                Console.Write(formatted);
            }
            catch (Exception)
            {
                // Handle case where custom formatting fails
            }
        }

        public void SetUnicodeCharactersEnabled(bool enabled)
        {
            _useUnicodeCharacters = enabled;
            _manuallyConfigured = true;
        }

        public void SetAnsiColorsEnabled(bool enabled)
        {
            _useAnsiColors = enabled;
            _manuallyConfigured = true;
        }

        public bool GetUnicodeCharactersEnabled()
        {
            return _useUnicodeCharacters;
        }

        public bool GetAnsiColorsEnabled()
        {
            return _useAnsiColors;
        }

        public string GetPlatformInfo()
        {
            try
            {
                return $"OS: {Environment.OSVersion}, " +
                       $"Encoding: {Console.OutputEncoding?.EncodingName ?? "Unknown"}, " +
                       $"Unicode: {_useUnicodeCharacters}, " +
                       $"Colors: {_useAnsiColors}";
            }
            catch (Exception ex)
            {
                return $"Platform detection failed: {ex.Message}";
            }
        }

        public void ForceAsciiMode()
        {
            _useUnicodeCharacters = false;
            _useAnsiColors = false;
        }

        public void TestUnicodeOutput()
        {
            try
            {
                Console.WriteLine($"Unicode Test: {GetAliveCellCharacter()} {GetDeadCellCharacter()} {GetTopLeftBorderCharacter()}");
                Console.WriteLine($"Color Test: {GetAliveCellColor()}Green{GetColorReset()} {GetDeadCellColor()}Gray{GetColorReset()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unicode/Color test failed: {ex.Message}");
            }
        }

        public void OptimizedClearGameArea(int boardWidth, int boardHeight, int startX, int startY)
        {
            try
            {
                // Use ANSI escape sequences for more efficient clearing if available
                if (_useAnsiColors && CanUseOptimizedClearing())
                {
                    // Move to start position and use ANSI sequences to clear efficiently
                    MoveCursorToPosition(startX, startY);
                    
                    // Clear each row using optimized method
                    string clearSequence = "\x1b[K"; // Clear to end of line
                    for (int row = 0; row < boardHeight; row++)
                    {
                        MoveCursorToPosition(startX, startY + row);
                        Console.Write(clearSequence);
                    }
                }
                else
                {
                    // Fallback to space-based clearing
                    ClearGameArea(boardWidth, boardHeight, startX, startY);
                }
            }
            catch (Exception)
            {
                // Fallback to basic clearing if optimization fails
                ClearGameArea(boardWidth, boardHeight, startX, startY);
            }
        }

        public bool CanUseOptimizedClearing()
        {
            return _useAnsiColors && !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TERM"));
        }

        public void BatchRenderCells(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int startX, int startY)
        {
            try
            {
                // Build output string in memory first to minimize console I/O
                var changes = new List<(int x, int y, string content)>();
                
                if (previousBoard == null)
                {
                    // Render all cells
                    for (int x = 0; x < board.Width; x++)
                    {
                        for (int y = 0; y < board.Height; y++)
                        {
                            string content = RenderCell(board.GetCell(x, y));
                            changes.Add((startX + x, startY + y, content));
                        }
                    }
                }
                else
                {
                    // Only render changed cells
                    int maxWidth = Math.Max(board.Width, previousBoard.Width);
                    int maxHeight = Math.Max(board.Height, previousBoard.Height);
                    
                    for (int x = 0; x < maxWidth; x++)
                    {
                        for (int y = 0; y < maxHeight; y++)
                        {
                            CellStatus currentCell = (x < board.Width && y < board.Height) ? 
                                board.GetCell(x, y) : CellStatus.Dead;
                            CellStatus previousCell = (x < previousBoard.Width && y < previousBoard.Height) ? 
                                previousBoard.GetCell(x, y) : CellStatus.Dead;
                            
                            if (HasCellChanged(currentCell, previousCell))
                            {
                                string content = RenderCell(currentCell);
                                changes.Add((startX + x, startY + y, content));
                            }
                        }
                    }
                }
                
                // Apply all changes in batches to reduce cursor movement
                ApplyBatchedChanges(changes);
            }
            catch (Exception)
            {
                // Fallback to standard rendering
                RenderBoardSmooth(board, previousBoard, startX, startY);
            }
        }

        public void ApplyBatchedChanges(List<(int x, int y, string content)> changes)
        {
            try
            {
                // Sort changes by row then column to minimize cursor jumps
                var sortedChanges = changes.OrderBy(c => c.y).ThenBy(c => c.x).ToList();
                
                int lastX = -1, lastY = -1;
                
                foreach (var (x, y, content) in sortedChanges)
                {
                    // Only move cursor if we're not already at the right position
                    if (x != lastX + 1 || y != lastY)
                    {
                        MoveCursorToPosition(x, y);
                    }
                    
                    Console.Write(content);
                    lastX = x;
                    lastY = y;
                }
                
                // Ensure cursor remains hidden after batch operations
                EnsureCursorHidden();
            }
            catch (Exception)
            {
                // If batching fails, fall back to individual writes
                foreach (var (x, y, content) in changes)
                {
                    MoveCursorToPosition(x, y);
                    Console.Write(content);
                }
                EnsureCursorHidden();
            }
        }

        public void OptimizedRenderBoardWithBorder(GameOfLifeBoard board, GameOfLifeBoard previousBoard, int offsetX, int offsetY)
        {
            try
            {
                // Only redraw border if this is the first render or board size changed
                bool needsBorderRedraw = previousBoard == null || 
                                       board.Width != previousBoard.Width || 
                                       board.Height != previousBoard.Height;
                
                if (needsBorderRedraw)
                {
                    DrawBorder(board.Width, board.Height, offsetX, offsetY);
                }
                
                // Use optimized batch rendering for the board content
                BatchRenderCells(board, previousBoard, offsetX + 1, offsetY + 1);
            }
            catch (Exception)
            {
                // Fallback to standard rendering
                RenderBoardWithBorder(board, offsetX, offsetY);
            }
        }

        public void MinimizeScreenFlicker()
        {
            try
            {
                // Techniques to reduce screen flicker
                EnsureCursorHidden();
                
                // If supported, try to enable alternate screen buffer
                if (CanUseAlternateScreen())
                {
                    Console.Write("\x1b[?1049h"); // Enable alternate screen
                }
            }
            catch (Exception)
            {
                // If flicker minimization fails, continue with standard rendering
            }
        }

        public void RestoreScreenBuffer()
        {
            try
            {
                if (CanUseAlternateScreen())
                {
                    Console.Write("\x1b[?1049l"); // Restore main screen
                }
            }
            catch (Exception)
            {
                // If restoration fails, continue normally
            }
        }

        public bool CanUseAlternateScreen()
        {
            // Check if terminal supports alternate screen buffer
            string term = Environment.GetEnvironmentVariable("TERM") ?? "";
            return _useAnsiColors && (term.Contains("xterm") || term.Contains("screen") || term.Contains("tmux"));
        }

    }
}