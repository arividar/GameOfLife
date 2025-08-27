using System;
using System.Text;

namespace GameOfLife
{
    public class ConsoleRenderer
    {
        private bool _originalCursorVisible;
        private Encoding _originalEncoding;

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
    }
}