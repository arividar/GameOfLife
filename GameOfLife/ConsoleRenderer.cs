using System;
using System.Text;

namespace GameOfLife
{
    public class ConsoleRenderer
    {
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
    }
}