using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System;

namespace GameOfLifeTests
{
    [TestClass]
    public class ConsoleRendererTests
    {
        [TestMethod]
        public void ConsoleRendererCanBeInstantiated()
        {
            var renderer = new ConsoleRenderer();
            Assert.IsNotNull(renderer);
        }

        [TestMethod]
        public void ClearScreenDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearScreen();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("ClearScreen should not throw exceptions");
            }
        }

        [TestMethod]
        public void SetEncodingDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetEncoding();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("SetEncoding should not throw exceptions");
            }
        }

        [TestMethod]
        public void HideCursorDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.HideCursor();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("HideCursor should not throw exceptions");
            }
        }

        [TestMethod]
        public void ShowCursorDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ShowCursor();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("ShowCursor should not throw exceptions");
            }
        }

        [TestMethod]
        public void CalculateCenterReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenter(10, 10);
            
            Assert.IsTrue(x >= 0, "Center X should be non-negative");
            Assert.IsTrue(y >= 0, "Center Y should be non-negative");
        }

        [TestMethod]
        public void CalculateCenterHandlesSmallerBoardCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenter(5, 5);
            
            Assert.IsTrue(x >= 0, "Center X should be non-negative for small board");
            Assert.IsTrue(y >= 0, "Center Y should be non-negative for small board");
        }

        [TestMethod]
        public void SetCursorToCenterDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetCursorToCenter(10);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("SetCursorToCenter should not throw exceptions");
            }
        }

        // Console initialization tests (Phase 1, Step 2)
        [TestMethod]
        public void InitializeConsoleDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.InitializeConsole();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("InitializeConsole should not throw exceptions");
            }
        }

        [TestMethod]
        public void RestoreConsoleDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RestoreConsole();
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("RestoreConsole should not throw exceptions");
            }
        }

        [TestMethod]
        public void GetTerminalSizeReturnsValidDimensions()
        {
            var renderer = new ConsoleRenderer();
            
            var (width, height) = renderer.GetTerminalSize();
            
            Assert.IsTrue(width > 0, "Terminal width should be positive");
            Assert.IsTrue(height > 0, "Terminal height should be positive");
        }

        [TestMethod]
        public void GetTerminalSizeHandlesExceptionsGracefully()
        {
            var renderer = new ConsoleRenderer();
            
            // This test ensures GetTerminalSize doesn't throw exceptions
            // even if console properties are not accessible
            try 
            {
                var (width, height) = renderer.GetTerminalSize();
                Assert.IsTrue(width >= 0, "Width should be non-negative even on failure");
                Assert.IsTrue(height >= 0, "Height should be non-negative even on failure");
            }
            catch
            {
                Assert.Fail("GetTerminalSize should handle exceptions internally and not throw");
            }
        }

        [TestMethod]
        public void IsTerminalSizeValidReturnsBooleanForValidSize()
        {
            var renderer = new ConsoleRenderer();
            
            bool isValid = renderer.IsTerminalSizeValid(80, 25);
            Assert.IsTrue(isValid, "Standard terminal size 80x25 should be valid");
        }

        [TestMethod]
        public void IsTerminalSizeValidReturnsFalseForInvalidSize()
        {
            var renderer = new ConsoleRenderer();
            
            bool isValid = renderer.IsTerminalSizeValid(5, 3);
            Assert.IsFalse(isValid, "Very small terminal size should be invalid");
        }

        [TestMethod]
        public void IsTerminalSizeValidHandlesEdgeCases()
        {
            var renderer = new ConsoleRenderer();
            
            Assert.IsFalse(renderer.IsTerminalSizeValid(0, 0), "Zero dimensions should be invalid");
            Assert.IsFalse(renderer.IsTerminalSizeValid(-1, 10), "Negative width should be invalid");
            Assert.IsFalse(renderer.IsTerminalSizeValid(10, -1), "Negative height should be invalid");
        }

        [TestMethod]
        public void ConsoleInitializationSequenceWorksCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            // Test full initialization sequence
            try 
            {
                renderer.InitializeConsole();
                renderer.ClearScreen();
                renderer.SetEncoding();
                renderer.HideCursor();
                
                // Verify terminal size is accessible after initialization
                var (width, height) = renderer.GetTerminalSize();
                Assert.IsTrue(width >= 0 && height >= 0, "Terminal size should be accessible after initialization");
                
                // Cleanup
                renderer.ShowCursor();
                renderer.RestoreConsole();
                
                Assert.IsTrue(true); // Complete sequence succeeded
            }
            catch
            {
                Assert.Fail("Complete console initialization sequence should not throw exceptions");
            }
        }

        // Enhanced cell rendering tests (Phase 1, Step 3)
        [TestMethod]
        public void GetAliveCellCharacterReturnsUnicodeBlock()
        {
            var renderer = new ConsoleRenderer();
            
            string aliveChar = renderer.GetAliveCellCharacter();
            Assert.AreEqual("█", aliveChar, "Alive cell should use full block Unicode character");
        }

        [TestMethod]
        public void GetDeadCellCharacterReturnsUnicodeMiddleDot()
        {
            var renderer = new ConsoleRenderer();
            
            string deadChar = renderer.GetDeadCellCharacter();
            Assert.AreEqual("·", deadChar, "Dead cell should use middle dot Unicode character");
        }

        [TestMethod]
        public void GetAliveCellColorReturnsGreenAnsiCode()
        {
            var renderer = new ConsoleRenderer();
            
            string aliveColor = renderer.GetAliveCellColor();
            Assert.AreEqual("\x1b[32m", aliveColor, "Alive cell should use green ANSI color code");
        }

        [TestMethod]
        public void GetDeadCellColorReturnsGrayAnsiCode()
        {
            var renderer = new ConsoleRenderer();
            
            string deadColor = renderer.GetDeadCellColor();
            Assert.AreEqual("\x1b[90m", deadColor, "Dead cell should use bright black (gray) ANSI color code");
        }

        [TestMethod]
        public void GetColorResetReturnsResetAnsiCode()
        {
            var renderer = new ConsoleRenderer();
            
            string resetCode = renderer.GetColorReset();
            Assert.AreEqual("\x1b[0m", resetCode, "Color reset should use ANSI reset code");
        }

        [TestMethod]
        public void RenderCellReturnsFormattedAliveCellString()
        {
            var renderer = new ConsoleRenderer();
            
            string formattedCell = renderer.RenderCell(CellStatus.Alive);
            string expected = "\x1b[32m█\x1b[0m";
            Assert.AreEqual(expected, formattedCell, "Alive cell should be formatted with green color and reset");
        }

        [TestMethod]
        public void RenderCellReturnsFormattedDeadCellString()
        {
            var renderer = new ConsoleRenderer();
            
            string formattedCell = renderer.RenderCell(CellStatus.Dead);
            string expected = "\x1b[90m·\x1b[0m";
            Assert.AreEqual(expected, formattedCell, "Dead cell should be formatted with gray color and reset");
        }

        // Board positioning tests (Phase 1, Step 4)
        [TestMethod]
        public void CalculateBoardPositionReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(10, 10, 80, 25);
            
            Assert.IsTrue(x >= 0, "Board X position should be non-negative");
            Assert.IsTrue(y >= 0, "Board Y position should be non-negative");
        }

        [TestMethod]
        public void CalculateBoardPositionCentersCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(10, 10, 80, 25);
            
            Assert.AreEqual(35, x, "Board should be centered horizontally (80-10)/2 = 35");
            Assert.AreEqual(7, y, "Board should be centered vertically (25-10)/2 = 7");
        }

        [TestMethod]
        public void CalculateBoardPositionHandlesLargeBoardGracefully()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(100, 100, 80, 25);
            
            Assert.AreEqual(0, x, "Large board X position should be clamped to 0");
            Assert.AreEqual(0, y, "Large board Y position should be clamped to 0");
        }

        [TestMethod]
        public void SetCursorToBoardPositionDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetCursorToBoardPosition(10, 10);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("SetCursorToBoardPosition should not throw exceptions");
            }
        }

        [TestMethod]
        public void MoveCursorToPositionDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.MoveCursorToPosition(10, 5);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("MoveCursorToPosition should not throw exceptions");
            }
        }

        [TestMethod]
        public void MoveCursorToPositionHandlesNegativeCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.MoveCursorToPosition(-5, -3);
                Assert.IsTrue(true); // Should handle negative coordinates gracefully
            }
            catch
            {
                Assert.Fail("MoveCursorToPosition should handle negative coordinates gracefully");
            }
        }

        // Border system tests (Phase 2, Step 3)
        [TestMethod]
        public void GetTopBorderCharacterReturnsCorrectUnicode()
        {
            var renderer = new ConsoleRenderer();
            
            string topLeft = renderer.GetTopLeftBorderCharacter();
            string topRight = renderer.GetTopRightBorderCharacter();
            string horizontal = renderer.GetHorizontalBorderCharacter();
            
            Assert.AreEqual("┌", topLeft, "Top left border should be corner character");
            Assert.AreEqual("┐", topRight, "Top right border should be corner character");
            Assert.AreEqual("─", horizontal, "Horizontal border should be line character");
        }

        [TestMethod]
        public void GetBottomBorderCharacterReturnsCorrectUnicode()
        {
            var renderer = new ConsoleRenderer();
            
            string bottomLeft = renderer.GetBottomLeftBorderCharacter();
            string bottomRight = renderer.GetBottomRightBorderCharacter();
            
            Assert.AreEqual("└", bottomLeft, "Bottom left border should be corner character");
            Assert.AreEqual("┘", bottomRight, "Bottom right border should be corner character");
        }

        [TestMethod]
        public void GetVerticalBorderCharacterReturnsCorrectUnicode()
        {
            var renderer = new ConsoleRenderer();
            
            string vertical = renderer.GetVerticalBorderCharacter();
            Assert.AreEqual("│", vertical, "Vertical border should be pipe character");
        }

        [TestMethod]
        public void CalculateBorderDimensionsReturnsCorrectSize()
        {
            var renderer = new ConsoleRenderer();
            
            var (width, height) = renderer.CalculateBorderDimensions(10, 10);
            
            Assert.AreEqual(12, width, "Border width should be board width + 2");
            Assert.AreEqual(12, height, "Border height should be board height + 2");
        }

        [TestMethod]
        public void CalculateBorderDimensionsHandlesVariousSizes()
        {
            var renderer = new ConsoleRenderer();
            
            var (w1, h1) = renderer.CalculateBorderDimensions(5, 8);
            var (w2, h2) = renderer.CalculateBorderDimensions(20, 15);
            
            Assert.AreEqual(7, w1, "5x8 board should have 7 width border");
            Assert.AreEqual(10, h1, "5x8 board should have 10 height border");
            Assert.AreEqual(22, w2, "20x15 board should have 22 width border");
            Assert.AreEqual(17, h2, "20x15 board should have 17 height border");
        }

        [TestMethod]
        public void DrawBorderDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.DrawBorder(10, 10, 5, 3);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("DrawBorder should not throw exceptions");
            }
        }

        [TestMethod]
        public void DrawBorderHandlesSmallBoards()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.DrawBorder(3, 3, 0, 0);
                Assert.IsTrue(true); // Should handle minimum size boards
            }
            catch
            {
                Assert.Fail("DrawBorder should handle small boards gracefully");
            }
        }

        // Centering logic tests (Phase 2, Step 4)
        [TestMethod]
        public void CalculateCenteredBoardPositionReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(10, 10);
            
            Assert.IsTrue(x >= 0, "Centered board X position should be non-negative");
            Assert.IsTrue(y >= 0, "Centered board Y position should be non-negative");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionCentersCorrectlyInStandardTerminal()
        {
            var renderer = new ConsoleRenderer();
            
            // Test with known terminal size (this will use actual terminal dimensions)
            var (x, y) = renderer.CalculateCenteredBoardPosition(20, 10);
            var (termWidth, termHeight) = renderer.GetTerminalSize();
            
            int expectedX = Math.Max(0, (termWidth - 22) / 2); // 20 + 2 for border
            int expectedY = Math.Max(0, (termHeight - 12) / 2); // 10 + 2 for border
            
            Assert.AreEqual(expectedX, x, "Board should be centered horizontally with border consideration");
            Assert.AreEqual(expectedY, y, "Board should be centered vertically with border consideration");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionWithCustomTerminalSize()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(10, 8, 80, 25);
            
            Assert.AreEqual(34, x, "10x8 board in 80x25 terminal should be at x=34 ((80-12)/2)");
            Assert.AreEqual(7, y, "10x8 board in 80x25 terminal should be at y=7 ((25-10)/2)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesSmallTerminals()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(20, 15, 30, 20);
            
            Assert.AreEqual(4, x, "20x15 board in 30x20 terminal should be at x=4 ((30-22)/2)");
            Assert.AreEqual(1, y, "20x15 board in 30x20 terminal should be at y=1 ((20-17)/2)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesExactFitTerminal()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(10, 8, 12, 10);
            
            Assert.AreEqual(0, x, "Board exactly fitting terminal width should be at x=0");
            Assert.AreEqual(0, y, "Board exactly fitting terminal height should be at y=0");
        }

        [TestMethod]
        public void IsBoardTooLargeForTerminalReturnsTrueForOversizedBoard()
        {
            var renderer = new ConsoleRenderer();
            
            bool tooLarge = renderer.IsBoardTooLargeForTerminal(30, 25, 30, 25);
            Assert.IsTrue(tooLarge, "Board requiring 32x27 space should be too large for 30x25 terminal");
        }

        [TestMethod]
        public void IsBoardTooLargeForTerminalReturnsFalseForFittingBoard()
        {
            var renderer = new ConsoleRenderer();
            
            bool tooLarge = renderer.IsBoardTooLargeForTerminal(10, 8, 80, 25);
            Assert.IsFalse(tooLarge, "Board requiring 12x10 space should fit in 80x25 terminal");
        }

        [TestMethod]
        public void SetCursorToCenteredBoardPositionDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetCursorToCenteredBoardPosition(15, 12);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("SetCursorToCenteredBoardPosition should not throw exceptions");
            }
        }

        [TestMethod]
        public void GetOptimalBoardSizeForTerminalReturnsReasonableSize()
        {
            var renderer = new ConsoleRenderer();
            
            var (width, height) = renderer.GetOptimalBoardSizeForTerminal();
            
            Assert.IsTrue(width > 5, "Optimal board width should be reasonable (> 5)");
            Assert.IsTrue(height > 5, "Optimal board height should be reasonable (> 5)");
            Assert.IsTrue(width <= 50, "Optimal board width should not be excessive (<= 50)");
            Assert.IsTrue(height <= 30, "Optimal board height should not be excessive (<= 30)");
        }

        [TestMethod]
        public void GetOptimalBoardSizeForCustomTerminalReturnsAppropriateSize()
        {
            var renderer = new ConsoleRenderer();
            
            var (width, height) = renderer.GetOptimalBoardSizeForTerminal(80, 25);
            
            // Should leave room for borders and some margin
            Assert.IsTrue(width < 78, "Optimal width should leave room for borders in 80-wide terminal");
            Assert.IsTrue(height < 23, "Optimal height should leave room for borders in 25-high terminal");
            Assert.IsTrue(width >= 10, "Optimal width should be at least 10 for playability");
            Assert.IsTrue(height >= 8, "Optimal height should be at least 8 for playability");
        }
    }
}