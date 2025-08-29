using System;
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

        public void InitializeConsole()
        {
            try
            {
                // Store original state for restoration
                _originalCursorVisible = Console.CursorVisible;
                _originalEncoding = Console.OutputEncoding;
                
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
            }
        }

        public void RestoreConsole()
        {
            try
            {
                // Restore original console state
                Console.CursorVisible = _originalCursorVisible;
                Console.OutputEncoding = _originalEncoding ?? Console.OutputEncoding;
            }
            catch (Exception)
            {
                // Handle case where console restoration fails
                // This might happen on some terminals or when output is redirected
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
            const int MIN_WIDTH = 15;  // Minimum for 10x10 board + borders
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
                Console.CursorVisible = true;
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
            return "█";
        }

        public string GetDeadCellCharacter()
        {
            return "·";
        }

        public string GetAliveCellColor()
        {
            return "\x1b[32m";
        }

        public string GetDeadCellColor()
        {
            return "\x1b[90m";
        }

        public string GetColorReset()
        {
            return "\x1b[0m";
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
            return "┌";
        }

        public string GetTopRightBorderCharacter()
        {
            return "┐";
        }

        public string GetBottomLeftBorderCharacter()
        {
            return "└";
        }

        public string GetBottomRightBorderCharacter()
        {
            return "┘";
        }

        public string GetHorizontalBorderCharacter()
        {
            return "─";
        }

        public string GetVerticalBorderCharacter()
        {
            return "│";
        }

        public (int width, int height) CalculateBorderDimensions(int boardWidth, int boardHeight)
        {
            return (boardWidth + 2, boardHeight + 2);
        }

        public void DrawBorder(int boardWidth, int boardHeight, int startX, int startY)
        {
            try
            {
                var (borderWidth, borderHeight) = CalculateBorderDimensions(boardWidth, boardHeight);
                
                // Draw top border
                MoveCursorToPosition(startX, startY);
                Console.Write(GetTopLeftBorderCharacter());
                for (int i = 0; i < boardWidth; i++)
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
                for (int i = 0; i < boardWidth; i++)
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
            int availableWidth = terminalWidth - 4; // Border + 2 margin
            int availableHeight = terminalHeight - 4; // Border + 2 margin
            
            // Ensure minimum playable size
            int optimalWidth = Math.Max(10, Math.Min(availableWidth, 50));
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
            }
            catch (Exception)
            {
                // Handle case where thread sleep fails
                // Continue execution without delay
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
            }
            catch (Exception)
            {
                // Handle case where smooth rendering fails
                // This might happen when output is redirected or on unsupported terminals
            }
        }

        public (int screenX, int screenY) CalculateCellScreenPosition(int boardX, int boardY, int offsetX, int offsetY)
        {
            return (boardX + offsetX, boardY + offsetY);
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

    }
}