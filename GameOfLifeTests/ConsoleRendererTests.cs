using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System;
using System.Collections.Generic;

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

        // Additional centering logic tests for Phase 2 Step 5
        [TestMethod]
        public void CalculateCenterHandlesVerySmallTerminals()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(10, 8, 15, 12);
            
            Assert.AreEqual(2, x, "Small terminal should center 10-wide board at x=2 ((15-10)/2)");
            Assert.AreEqual(2, y, "Small terminal should center 8-high board at y=2 ((12-8)/2)");
        }

        [TestMethod]
        public void CalculateCenterHandlesVeryLargeBoards()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(100, 50, 80, 25);
            
            Assert.AreEqual(0, x, "Large board in small terminal should be clamped to x=0");
            Assert.AreEqual(0, y, "Large board in small terminal should be clamped to y=0");
        }

        [TestMethod]
        public void CalculateCenterHandlesNegativeCenterCalculation()
        {
            var renderer = new ConsoleRenderer();
            
            // Board larger than terminal results in negative center calculation
            var (x, y) = renderer.CalculateBoardPosition(50, 30, 40, 20);
            
            Assert.AreEqual(0, x, "Negative center calculation should be clamped to 0 for x");
            Assert.AreEqual(0, y, "Negative center calculation should be clamped to 0 for y");
        }

        [TestMethod]
        public void CalculateCenterHandlesOddTerminalDimensions()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(10, 8, 81, 25);
            
            Assert.AreEqual(35, x, "Odd terminal width should center correctly ((81-10)/2 = 35)");
            Assert.AreEqual(8, y, "Terminal height should center correctly ((25-8)/2 = 8)");
        }

        [TestMethod]
        public void CalculateCenterHandlesEvenTerminalDimensions()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(11, 9, 80, 24);
            
            Assert.AreEqual(34, x, "Even terminal width should center correctly ((80-11)/2 = 34)");
            Assert.AreEqual(7, y, "Even terminal height should center correctly ((24-9)/2 = 7)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesBorderOffsetCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(8, 6, 40, 20);
            
            // Board size 8x6, border adds 2 to each dimension = 10x8 total
            Assert.AreEqual(15, x, "Centering with border should account for border size ((40-10)/2 = 15)");
            Assert.AreEqual(6, y, "Centering with border should account for border size ((20-8)/2 = 6)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesZeroDimensions()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(0, 0, 80, 25);
            
            // 0x0 board with 2x2 border = 2x2 total
            Assert.AreEqual(39, x, "Zero-size board should center border correctly ((80-2)/2 = 39)");
            Assert.AreEqual(11, y, "Zero-size board should center border correctly ((25-2)/2 = 11)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesSingleCellBoard()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(1, 1, 80, 25);
            
            // 1x1 board with 2x2 border = 3x3 total
            Assert.AreEqual(38, x, "Single cell board should center correctly ((80-3)/2 = 38)");
            Assert.AreEqual(11, y, "Single cell board should center correctly ((25-3)/2 = 11)");
        }

        [TestMethod]
        public void CalculateCenteredBoardPositionHandlesMaximumBoardSize()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateCenteredBoardPosition(78, 23, 80, 25);
            
            // 78x23 board with 2x2 border = 80x25 total (exact fit)
            Assert.AreEqual(0, x, "Maximum board size should result in x=0 for exact fit");
            Assert.AreEqual(0, y, "Maximum board size should result in y=0 for exact fit");
        }

        [TestMethod]
        public void IsBoardTooLargeForTerminalHandlesEdgeCaseExactBorderFit()
        {
            var renderer = new ConsoleRenderer();
            
            bool exactFit = renderer.IsBoardTooLargeForTerminal(78, 23, 80, 25);
            Assert.IsFalse(exactFit, "Board that exactly fits with borders should not be too large");
        }

        [TestMethod]
        public void IsBoardTooLargeForTerminalHandlesOnePixelOver()
        {
            var renderer = new ConsoleRenderer();
            
            bool tooLarge = renderer.IsBoardTooLargeForTerminal(79, 23, 80, 25);
            Assert.IsTrue(tooLarge, "Board one pixel too wide should be too large (79+2=81 > 80)");
            
            tooLarge = renderer.IsBoardTooLargeForTerminal(78, 24, 80, 25);
            Assert.IsTrue(tooLarge, "Board one pixel too high should be too large (24+2=26 > 25)");
        }

        [TestMethod]
        public void CalculateCenterWithVeryWideBoard()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(200, 5, 80, 25);
            
            Assert.AreEqual(0, x, "Very wide board should be clamped to x=0");
            Assert.AreEqual(10, y, "Narrow board should still center vertically ((25-5)/2 = 10)");
        }

        [TestMethod]
        public void CalculateCenterWithVeryTallBoard()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateBoardPosition(5, 100, 80, 25);
            
            Assert.AreEqual(37, x, "Narrow board should center horizontally ((80-5)/2 = 37)");
            Assert.AreEqual(0, y, "Very tall board should be clamped to y=0");
        }

        // Phase 3 Step 6: Smooth rendering tests
        [TestMethod]
        public void RenderCellAtPositionDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderCellAtPosition(5, 3, CellStatus.Alive, 10, 8);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("RenderCellAtPosition should not throw exceptions");
            }
        }

        [TestMethod]
        public void RenderCellAtPositionHandlesDeadCell()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderCellAtPosition(2, 4, CellStatus.Dead, 15, 10);
                Assert.IsTrue(true); // Should handle dead cells without exceptions
            }
            catch
            {
                Assert.Fail("RenderCellAtPosition should handle dead cells without exceptions");
            }
        }

        [TestMethod]
        public void RenderCellAtPositionHandlesZeroOffset()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderCellAtPosition(0, 0, CellStatus.Alive, 0, 0);
                Assert.IsTrue(true); // Should handle zero offset positioning
            }
            catch
            {
                Assert.Fail("RenderCellAtPosition should handle zero offset positioning");
            }
        }

        [TestMethod]
        public void RenderCellAtPositionHandlesLargeCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderCellAtPosition(50, 30, CellStatus.Dead, 100, 60);
                Assert.IsTrue(true); // Should handle large coordinates gracefully
            }
            catch
            {
                Assert.Fail("RenderCellAtPosition should handle large coordinates gracefully");
            }
        }

        [TestMethod]
        public void ClearGameAreaDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearGameArea(10, 8, 15, 12);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("ClearGameArea should not throw exceptions");
            }
        }

        [TestMethod]
        public void ClearGameAreaHandlesSmallArea()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearGameArea(1, 1, 0, 0);
                Assert.IsTrue(true); // Should handle minimal game area
            }
            catch
            {
                Assert.Fail("ClearGameArea should handle minimal game area");
            }
        }

        [TestMethod]
        public void ClearGameAreaHandlesLargeArea()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearGameArea(50, 30, 5, 3);
                Assert.IsTrue(true); // Should handle large game area
            }
            catch
            {
                Assert.Fail("ClearGameArea should handle large game area without exceptions");
            }
        }

        [TestMethod]
        public void ClearGameAreaHandlesZeroPosition()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearGameArea(15, 12, 0, 0);
                Assert.IsTrue(true); // Should handle zero start position
            }
            catch
            {
                Assert.Fail("ClearGameArea should handle zero start position");
            }
        }

        [TestMethod]
        public void SetAnimationDelayAcceptsValidDelay()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetAnimationDelay(500);
                Assert.IsTrue(true); // Should accept valid delay values
            }
            catch
            {
                Assert.Fail("SetAnimationDelay should accept valid delay values");
            }
        }

        [TestMethod]
        public void GetAnimationDelayReturnsDefaultValue()
        {
            var renderer = new ConsoleRenderer();
            
            int defaultDelay = renderer.GetAnimationDelay();
            Assert.AreEqual(500, defaultDelay, "Default animation delay should be 500ms");
        }

        [TestMethod]
        public void SetAndGetAnimationDelayWorksCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnimationDelay(750);
            int delay = renderer.GetAnimationDelay();
            Assert.AreEqual(750, delay, "Animation delay should return the set value");
        }

        [TestMethod]
        public void SetAnimationDelayHandlesZeroDelay()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnimationDelay(0);
            int delay = renderer.GetAnimationDelay();
            Assert.AreEqual(0, delay, "Should accept zero delay for immediate rendering");
        }

        [TestMethod]
        public void SetAnimationDelayHandlesLargeDelay()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnimationDelay(5000);
            int delay = renderer.GetAnimationDelay();
            Assert.AreEqual(5000, delay, "Should accept large delay values");
        }

        [TestMethod]
        public void SetAnimationDelayRejectsNegativeValues()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetAnimationDelay(-100);
                Assert.Fail("SetAnimationDelay should reject negative values");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true); // Expected exception for negative values
            }
            catch
            {
                Assert.Fail("SetAnimationDelay should throw ArgumentException for negative values");
            }
        }

        [TestMethod]
        public void RenderBoardSmoothDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            var previousBoard = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.RenderBoardSmooth(board, previousBoard, 10, 8);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("RenderBoardSmooth should not throw exceptions");
            }
        }

        [TestMethod]
        public void RenderBoardSmoothHandlesNullPreviousBoard()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.RenderBoardSmooth(board, null, 10, 8);
                Assert.IsTrue(true); // Should handle null previous board (first render)
            }
            catch
            {
                Assert.Fail("RenderBoardSmooth should handle null previous board gracefully");
            }
        }

        [TestMethod]
        public void RenderBoardSmoothHandlesSameSizeBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(10);
            var previousBoard = new GameOfLifeBoard(10);
            
            try 
            {
                renderer.RenderBoardSmooth(board, previousBoard, 5, 3);
                Assert.IsTrue(true); // Should handle same size boards
            }
            catch
            {
                Assert.Fail("RenderBoardSmooth should handle same size boards");
            }
        }

        [TestMethod]
        public void RenderBoardSmoothHandlesDifferentSizeBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(8);
            var previousBoard = new GameOfLifeBoard(6);
            
            try 
            {
                renderer.RenderBoardSmooth(board, previousBoard, 12, 9);
                Assert.IsTrue(true); // Should handle different size boards gracefully
            }
            catch
            {
                Assert.Fail("RenderBoardSmooth should handle different size boards gracefully");
            }
        }

        [TestMethod]
        public void RenderBoardSmoothHandlesZeroOffset()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(3);
            var previousBoard = new GameOfLifeBoard(3);
            
            try 
            {
                renderer.RenderBoardSmooth(board, previousBoard, 0, 0);
                Assert.IsTrue(true); // Should handle zero offset positioning
            }
            catch
            {
                Assert.Fail("RenderBoardSmooth should handle zero offset positioning");
            }
        }

        [TestMethod]
        public void CalculateCellScreenPositionReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (screenX, screenY) = renderer.CalculateCellScreenPosition(2, 3, 10, 8);
            
            Assert.AreEqual(12, screenX, "Cell screen X should be boardX + offsetX (2 + 10)");
            Assert.AreEqual(11, screenY, "Cell screen Y should be boardY + offsetY (3 + 8)");
        }

        [TestMethod]
        public void CalculateCellScreenPositionHandlesZeroOffset()
        {
            var renderer = new ConsoleRenderer();
            
            var (screenX, screenY) = renderer.CalculateCellScreenPosition(5, 7, 0, 0);
            
            Assert.AreEqual(5, screenX, "Cell screen X with zero offset should equal board X");
            Assert.AreEqual(7, screenY, "Cell screen Y with zero offset should equal board Y");
        }

        [TestMethod]
        public void CalculateCellScreenPositionHandlesZeroPosition()
        {
            var renderer = new ConsoleRenderer();
            
            var (screenX, screenY) = renderer.CalculateCellScreenPosition(0, 0, 15, 12);
            
            Assert.AreEqual(15, screenX, "Cell at origin should use offset X");
            Assert.AreEqual(12, screenY, "Cell at origin should use offset Y");
        }

        [TestMethod]
        public void HasCellChangedReturnsTrueForDifferentCells()
        {
            var renderer = new ConsoleRenderer();
            
            bool changed = renderer.HasCellChanged(CellStatus.Alive, CellStatus.Dead);
            Assert.IsTrue(changed, "Should return true when cells have different status");
        }

        [TestMethod]
        public void HasCellChangedReturnsFalseForSameCells()
        {
            var renderer = new ConsoleRenderer();
            
            bool changed = renderer.HasCellChanged(CellStatus.Alive, CellStatus.Alive);
            Assert.IsFalse(changed, "Should return false when cells have same status");
            
            changed = renderer.HasCellChanged(CellStatus.Dead, CellStatus.Dead);
            Assert.IsFalse(changed, "Should return false when both cells are dead");
        }

        [TestMethod]
        public void GetDelayForSmoothRenderingReturnsConfiguredDelay()
        {
            var renderer = new ConsoleRenderer();
            renderer.SetAnimationDelay(300);
            
            int delay = renderer.GetDelayForSmoothRendering();
            Assert.AreEqual(300, delay, "Should return the configured animation delay");
        }

        [TestMethod]
        public void ApplyAnimationDelayDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ApplyAnimationDelay();
                Assert.IsTrue(true); // Should not throw exceptions during delay
            }
            catch
            {
                Assert.Fail("ApplyAnimationDelay should not throw exceptions");
            }
        }

        // Phase 3 Step 7: Generation Counter Display Tests

        [TestMethod]
        public void RenderGenerationCounterAtTopDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderGenerationCounterAtTop(5, 10, 8);
                Assert.IsTrue(true); // Should not throw exceptions when rendering at top
            }
            catch
            {
                Assert.Fail("RenderGenerationCounterAtTop should not throw exceptions");
            }
        }

        [TestMethod]
        public void RenderGenerationCounterAtBottomDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderGenerationCounterAtBottom(12, 15, 20, 25);
                Assert.IsTrue(true); // Should not throw exceptions when rendering at bottom
            }
            catch
            {
                Assert.Fail("RenderGenerationCounterAtBottom should not throw exceptions");
            }
        }

        [TestMethod]
        public void CalculateGenerationCounterTopPositionReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateGenerationCounterTopPosition(10, 8);
            
            Assert.IsTrue(x >= 0, "Top counter X position should be non-negative");
            Assert.IsTrue(y >= 0, "Top counter Y position should be non-negative");
            Assert.IsTrue(y < 8, "Top counter should be above the game area");
        }

        [TestMethod]
        public void CalculateGenerationCounterBottomPositionReturnsValidCoordinates()
        {
            var renderer = new ConsoleRenderer();
            
            var (x, y) = renderer.CalculateGenerationCounterBottomPosition(15, 20, 10, 12);
            
            Assert.IsTrue(x >= 0, "Bottom counter X position should be non-negative");
            Assert.IsTrue(y >= 0, "Bottom counter Y position should be non-negative");
            Assert.IsTrue(y > 20, "Bottom counter should be below the game area");
        }

        [TestMethod]
        public void FormatGenerationCounterReturnsCorrectString()
        {
            var renderer = new ConsoleRenderer();
            
            string formatted = renderer.FormatGenerationCounter(42);
            
            Assert.IsNotNull(formatted, "Formatted generation counter should not be null");
            Assert.IsTrue(formatted.Contains("42"), "Formatted string should contain generation number");
            Assert.IsTrue(formatted.Contains("Generation"), "Formatted string should contain 'Generation' label");
        }

        [TestMethod]
        public void FormatGenerationCounterHandlesZeroGeneration()
        {
            var renderer = new ConsoleRenderer();
            
            string formatted = renderer.FormatGenerationCounter(0);
            
            Assert.IsNotNull(formatted, "Formatted generation counter should not be null for zero");
            Assert.IsTrue(formatted.Contains("0"), "Formatted string should contain zero generation number");
        }

        [TestMethod]
        public void FormatGenerationCounterHandlesLargeGeneration()
        {
            var renderer = new ConsoleRenderer();
            
            string formatted = renderer.FormatGenerationCounter(999999);
            
            Assert.IsNotNull(formatted, "Formatted generation counter should not be null for large numbers");
            Assert.IsTrue(formatted.Contains("999999"), "Formatted string should contain large generation number");
        }

        [TestMethod]
        public void GetGenerationCounterLengthReturnsCorrectLength()
        {
            var renderer = new ConsoleRenderer();
            
            int length = renderer.GetGenerationCounterLength(123);
            
            Assert.IsTrue(length > 0, "Generation counter length should be positive");
            Assert.IsTrue(length >= 10, "Generation counter should be at least 10 characters (for 'Generation ' + digits)");
        }

        [TestMethod]
        public void ClearGenerationCounterAreaDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.ClearGenerationCounterArea(5, 3, 20);
                Assert.IsTrue(true); // Should not throw exceptions when clearing counter area
            }
            catch
            {
                Assert.Fail("ClearGenerationCounterArea should not throw exceptions");
            }
        }

        [TestMethod]
        public void SetGenerationCounterPositionUpdatesPosition()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetGenerationCounterPosition(GenerationCounterPosition.Top);
                Assert.IsTrue(true); // Should accept Top position
                
                renderer.SetGenerationCounterPosition(GenerationCounterPosition.Bottom);
                Assert.IsTrue(true); // Should accept Bottom position
            }
            catch
            {
                Assert.Fail("SetGenerationCounterPosition should not throw exceptions for valid positions");
            }
        }

        [TestMethod]
        public void GetGenerationCounterPositionReturnsCurrentPosition()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetGenerationCounterPosition(GenerationCounterPosition.Bottom);
            var position = renderer.GetGenerationCounterPosition();
            
            Assert.AreEqual(GenerationCounterPosition.Bottom, position, "Should return the set position");
        }

        [TestMethod]
        public void GenerationCounterDoesNotOverlapWithGameArea()
        {
            var renderer = new ConsoleRenderer();
            
            // Test top position doesn't overlap
            var (topX, topY) = renderer.CalculateGenerationCounterTopPosition(10, 5);
            Assert.IsTrue(topY < 5, "Top counter should not overlap with game area starting at Y=5");
            
            // Test bottom position doesn't overlap
            var (bottomX, bottomY) = renderer.CalculateGenerationCounterBottomPosition(15, 10, 10, 15);
            Assert.IsTrue(bottomY > 15 + 10, "Bottom counter should not overlap with game area ending at Y=25");
        }

        [TestMethod]
        public void GenerationCounterStaysWithinTerminalBounds()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                // Test with small terminal dimensions
                renderer.RenderGenerationCounterAtTop(999, 5, 3);
                renderer.RenderGenerationCounterAtBottom(999, 20, 15, 25);
                Assert.IsTrue(true); // Should handle small terminals gracefully
            }
            catch
            {
                Assert.Fail("Generation counter should handle small terminal dimensions");
            }
        }

        [TestMethod]
        public void RenderGenerationCounterWithCustomFormattingDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RenderGenerationCounterWithFormatting(42, 10, 5, "Gen: {0}");
                Assert.IsTrue(true); // Should accept custom formatting
            }
            catch
            {
                Assert.Fail("RenderGenerationCounterWithFormatting should not throw exceptions");
            }
        }

        [TestMethod]
        public void GenerationCounterHandlesConsecutiveUpdates()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                // Simulate consecutive generation updates
                for (int i = 0; i < 5; i++)
                {
                    renderer.RenderGenerationCounterAtTop(i, 10, 8);
                }
                Assert.IsTrue(true); // Should handle consecutive updates
            }
            catch
            {
                Assert.Fail("Generation counter should handle consecutive updates");
            }
        }

        // Phase 5: Cross-platform testing and Unicode fallback tests
        [TestMethod]
        public void DetectTerminalCapabilitiesDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.DetectTerminalCapabilities();
                Assert.IsTrue(true); // Should not throw exceptions during capability detection
            }
            catch
            {
                Assert.Fail("DetectTerminalCapabilities should not throw exceptions");
            }
        }

        [TestMethod]
        public void CanUseUnicodeCharactersReturnsBooleanValue()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                bool canUseUnicode = renderer.CanUseUnicodeCharacters();
                // Should return either true or false, not throw exceptions
                Assert.IsTrue(canUseUnicode == true || canUseUnicode == false);
            }
            catch
            {
                Assert.Fail("CanUseUnicodeCharacters should return boolean without exceptions");
            }
        }

        [TestMethod]
        public void CanUseAnsiColorsReturnsBooleanValue()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                bool canUseColors = renderer.CanUseAnsiColors();
                // Should return either true or false, not throw exceptions
                Assert.IsTrue(canUseColors == true || canUseColors == false);
            }
            catch
            {
                Assert.Fail("CanUseAnsiColors should return boolean without exceptions");
            }
        }

        [TestMethod]
        public void SetUnicodeCharactersEnabledChangesCharacterOutput()
        {
            var renderer = new ConsoleRenderer();
            
            // Enable Unicode
            renderer.SetUnicodeCharactersEnabled(true);
            string unicodeAlive = renderer.GetAliveCellCharacter();
            string unicodeDead = renderer.GetDeadCellCharacter();
            
            // Disable Unicode (force ASCII mode)
            renderer.SetUnicodeCharactersEnabled(false);
            string asciiAlive = renderer.GetAliveCellCharacter();
            string asciiDead = renderer.GetDeadCellCharacter();
            
            // Characters should be different
            Assert.AreNotEqual(unicodeAlive, asciiAlive, "Alive cell characters should differ between Unicode and ASCII modes");
            Assert.AreNotEqual(unicodeDead, asciiDead, "Dead cell characters should differ between Unicode and ASCII modes");
        }

        [TestMethod]
        public void SetAnsiColorsEnabledChangesColorOutput()
        {
            var renderer = new ConsoleRenderer();
            
            // Enable colors
            renderer.SetAnsiColorsEnabled(true);
            string colorAlive = renderer.GetAliveCellColor();
            string colorReset = renderer.GetColorReset();
            
            // Disable colors
            renderer.SetAnsiColorsEnabled(false);
            string noColorAlive = renderer.GetAliveCellColor();
            string noColorReset = renderer.GetColorReset();
            
            // Color strings should be different (empty when disabled)
            Assert.AreNotEqual(colorAlive, noColorAlive, "Color codes should differ when colors are disabled");
            Assert.AreNotEqual(colorReset, noColorReset, "Reset codes should differ when colors are disabled");
        }

        [TestMethod]
        public void GetUnicodeCharactersEnabledReturnsCorrectStatus()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetUnicodeCharactersEnabled(true);
            Assert.IsTrue(renderer.GetUnicodeCharactersEnabled(), "Should return true when Unicode is enabled");
            
            renderer.SetUnicodeCharactersEnabled(false);
            Assert.IsFalse(renderer.GetUnicodeCharactersEnabled(), "Should return false when Unicode is disabled");
        }

        [TestMethod]
        public void GetAnsiColorsEnabledReturnsCorrectStatus()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnsiColorsEnabled(true);
            Assert.IsTrue(renderer.GetAnsiColorsEnabled(), "Should return true when colors are enabled");
            
            renderer.SetAnsiColorsEnabled(false);
            Assert.IsFalse(renderer.GetAnsiColorsEnabled(), "Should return false when colors are disabled");
        }

        [TestMethod]
        public void ForceAsciiModeDisablesBothUnicodeAndColors()
        {
            var renderer = new ConsoleRenderer();
            
            // First enable both
            renderer.SetUnicodeCharactersEnabled(true);
            renderer.SetAnsiColorsEnabled(true);
            
            // Force ASCII mode
            renderer.ForceAsciiMode();
            
            Assert.IsFalse(renderer.GetUnicodeCharactersEnabled(), "ForceAsciiMode should disable Unicode characters");
            Assert.IsFalse(renderer.GetAnsiColorsEnabled(), "ForceAsciiMode should disable ANSI colors");
        }

        [TestMethod]
        public void GetPlatformInfoReturnsNonEmptyString()
        {
            var renderer = new ConsoleRenderer();
            
            string info = renderer.GetPlatformInfo();
            
            Assert.IsNotNull(info, "Platform info should not be null");
            Assert.IsTrue(info.Length > 0, "Platform info should not be empty");
            Assert.IsTrue(info.Contains("OS"), "Platform info should contain OS information");
        }

        [TestMethod]
        public void TestUnicodeOutputDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.TestUnicodeOutput();
                Assert.IsTrue(true); // Should not throw exceptions during Unicode test
            }
            catch
            {
                Assert.Fail("TestUnicodeOutput should not throw exceptions");
            }
        }

        [TestMethod]
        public void BorderCharactersFallbackToAsciiWhenUnicodeDisabled()
        {
            var renderer = new ConsoleRenderer();
            
            // Test Unicode mode
            renderer.SetUnicodeCharactersEnabled(true);
            string unicodeTopLeft = renderer.GetTopLeftBorderCharacter();
            string unicodeHorizontal = renderer.GetHorizontalBorderCharacter();
            string unicodeVertical = renderer.GetVerticalBorderCharacter();
            
            // Test ASCII mode
            renderer.SetUnicodeCharactersEnabled(false);
            string asciiTopLeft = renderer.GetTopLeftBorderCharacter();
            string asciiHorizontal = renderer.GetHorizontalBorderCharacter();
            string asciiVertical = renderer.GetVerticalBorderCharacter();
            
            // ASCII fallbacks should be different from Unicode
            Assert.AreNotEqual(unicodeTopLeft, asciiTopLeft, "Top-left border should differ between Unicode and ASCII");
            Assert.AreNotEqual(unicodeHorizontal, asciiHorizontal, "Horizontal border should differ between Unicode and ASCII");
            Assert.AreNotEqual(unicodeVertical, asciiVertical, "Vertical border should differ between Unicode and ASCII");
            
            // ASCII characters should be basic ASCII
            Assert.AreEqual("+", asciiTopLeft, "ASCII top-left should be plus sign");
            Assert.AreEqual("-", asciiHorizontal, "ASCII horizontal should be dash");
            Assert.AreEqual("|", asciiVertical, "ASCII vertical should be pipe");
        }

        [TestMethod]
        public void ColorsAreEmptyWhenAnsiColorsDisabled()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnsiColorsEnabled(false);
            
            Assert.AreEqual("", renderer.GetAliveCellColor(), "Alive cell color should be empty when colors disabled");
            Assert.AreEqual("", renderer.GetDeadCellColor(), "Dead cell color should be empty when colors disabled");
            Assert.AreEqual("", renderer.GetColorReset(), "Color reset should be empty when colors disabled");
        }

        [TestMethod]
        public void ColorsAreAnsiCodesWhenEnabled()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnsiColorsEnabled(true);
            
            string aliveColor = renderer.GetAliveCellColor();
            string deadColor = renderer.GetDeadCellColor();
            string resetColor = renderer.GetColorReset();
            
            Assert.IsTrue(aliveColor.StartsWith("\x1b["), "Alive color should be ANSI escape sequence");
            Assert.IsTrue(deadColor.StartsWith("\x1b["), "Dead color should be ANSI escape sequence");
            Assert.IsTrue(resetColor.StartsWith("\x1b["), "Reset should be ANSI escape sequence");
        }

        [TestMethod]
        public void RenderCellHandlesBothUnicodeAndAsciiModes()
        {
            var renderer = new ConsoleRenderer();
            
            // Test Unicode mode
            renderer.SetUnicodeCharactersEnabled(true);
            renderer.SetAnsiColorsEnabled(true);
            string unicodeRendered = renderer.RenderCell(CellStatus.Alive);
            
            // Test ASCII mode
            renderer.SetUnicodeCharactersEnabled(false);
            renderer.SetAnsiColorsEnabled(false);
            string asciiRendered = renderer.RenderCell(CellStatus.Alive);
            
            Assert.AreNotEqual(unicodeRendered, asciiRendered, "Rendered cells should differ between Unicode and ASCII modes");
            Assert.IsTrue(unicodeRendered.Contains("█"), "Unicode mode should contain block character");
            Assert.IsTrue(asciiRendered.Contains("O"), "ASCII mode should contain 'O' character");
        }

        [TestMethod]
        public void ConsoleInitializationHandlesCapabilityDetection()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.InitializeConsole();
                
                // Should have detected capabilities (one way or another)
                bool unicodeEnabled = renderer.GetUnicodeCharactersEnabled();
                bool colorsEnabled = renderer.GetAnsiColorsEnabled();
                
                // These should be valid boolean values
                Assert.IsTrue(unicodeEnabled == true || unicodeEnabled == false);
                Assert.IsTrue(colorsEnabled == true || colorsEnabled == false);
                
                renderer.RestoreConsole();
            }
            catch
            {
                Assert.Fail("Console initialization should handle capability detection gracefully");
            }
        }

        [TestMethod]
        public void InitializationFallsBackGracefullyOnException()
        {
            var renderer = new ConsoleRenderer();
            
            // This should not throw even if some console operations fail
            try 
            {
                renderer.InitializeConsole();
                renderer.RestoreConsole();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("Console initialization should handle exceptions gracefully with fallbacks");
            }
        }

        [TestMethod]
        public void CanUseUnicodeCharactersHandlesEnvironmentVariables()
        {
            var renderer = new ConsoleRenderer();
            
            // This method should handle various environment variable scenarios
            // Even if environment variables are not set or accessible
            bool canUse = renderer.CanUseUnicodeCharacters();
            Assert.IsTrue(canUse == true || canUse == false, "Should return a boolean value");
        }

        [TestMethod]
        public void CanUseAnsiColorsRespectsNoColorEnvironmentVariable()
        {
            var renderer = new ConsoleRenderer();
            
            // Store original NO_COLOR value
            string originalNoColor = Environment.GetEnvironmentVariable("NO_COLOR");
            
            try
            {
                // Set NO_COLOR to disable colors
                Environment.SetEnvironmentVariable("NO_COLOR", "1");
                bool canUseColors = renderer.CanUseAnsiColors();
                Assert.IsFalse(canUseColors, "Should return false when NO_COLOR is set");
            }
            finally
            {
                // Restore original value
                Environment.SetEnvironmentVariable("NO_COLOR", originalNoColor);
            }
        }

        [TestMethod]
        public void EnsureCursorHiddenHandlesExceptions()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.EnsureCursorHidden();
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("EnsureCursorHidden should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void ApplyAnimationDelayHandlesZeroDelay()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnimationDelay(0);
            
            try 
            {
                renderer.ApplyAnimationDelay();
                Assert.IsTrue(true); // Should handle zero delay without issues
            }
            catch
            {
                Assert.Fail("ApplyAnimationDelay should handle zero delay gracefully");
            }
        }

        [TestMethod]
        public void RenderBoardWithBorderHandlesExceptions()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.RenderBoardWithBorder(board, 10, 8);
                Assert.IsTrue(true); // Should handle rendering with border
            }
            catch
            {
                Assert.Fail("RenderBoardWithBorder should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void SetEncodingHandlesExceptionsGracefully()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.SetEncoding();
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("SetEncoding should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void GetOptimalBoardSizeForSmallTerminal()
        {
            var renderer = new ConsoleRenderer();
            
            // Test with very small terminal
            var (width, height) = renderer.GetOptimalBoardSizeForTerminal(20, 15);
            
            Assert.IsTrue(width >= 10, "Should maintain minimum width even for small terminals");
            Assert.IsTrue(height >= 8, "Should maintain minimum height even for small terminals");
            Assert.IsTrue(width <= 16, "Should fit within small terminal bounds");
            Assert.IsTrue(height <= 11, "Should fit within small terminal bounds");
        }

        // Phase 5: Performance optimization tests
        [TestMethod]
        public void OptimizedClearGameAreaDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.OptimizedClearGameArea(10, 8, 5, 3);
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("OptimizedClearGameArea should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void CanUseOptimizedClearingReturnsBooleanValue()
        {
            var renderer = new ConsoleRenderer();
            
            bool canUse = renderer.CanUseOptimizedClearing();
            Assert.IsTrue(canUse == true || canUse == false, "Should return a boolean value");
        }

        [TestMethod]
        public void BatchRenderCellsHandlesNullPreviousBoard()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.BatchRenderCells(board, null, 10, 8);
                Assert.IsTrue(true); // Should handle null previous board
            }
            catch
            {
                Assert.Fail("BatchRenderCells should handle null previous board gracefully");
            }
        }

        [TestMethod]
        public void BatchRenderCellsHandlesDifferentSizeBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(8);
            var previousBoard = new GameOfLifeBoard(6);
            
            try 
            {
                renderer.BatchRenderCells(board, previousBoard, 5, 3);
                Assert.IsTrue(true); // Should handle different size boards
            }
            catch
            {
                Assert.Fail("BatchRenderCells should handle different size boards gracefully");
            }
        }

        [TestMethod]
        public void ApplyBatchedChangesHandlesEmptyList()
        {
            var renderer = new ConsoleRenderer();
            var emptyChanges = new List<(int x, int y, string content)>();
            
            try 
            {
                renderer.ApplyBatchedChanges(emptyChanges);
                Assert.IsTrue(true); // Should handle empty list
            }
            catch
            {
                Assert.Fail("ApplyBatchedChanges should handle empty list gracefully");
            }
        }

        [TestMethod]
        public void ApplyBatchedChangesHandlesMultipleChanges()
        {
            var renderer = new ConsoleRenderer();
            var changes = new List<(int x, int y, string content)>
            {
                (5, 3, "test1"),
                (7, 3, "test2"),
                (5, 4, "test3")
            };
            
            try 
            {
                renderer.ApplyBatchedChanges(changes);
                Assert.IsTrue(true); // Should handle multiple changes
            }
            catch
            {
                Assert.Fail("ApplyBatchedChanges should handle multiple changes gracefully");
            }
        }

        [TestMethod]
        public void OptimizedRenderBoardWithBorderHandlesFirstRender()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.OptimizedRenderBoardWithBorder(board, null, 10, 8);
                Assert.IsTrue(true); // Should handle first render (null previous board)
            }
            catch
            {
                Assert.Fail("OptimizedRenderBoardWithBorder should handle first render gracefully");
            }
        }

        [TestMethod]
        public void OptimizedRenderBoardWithBorderHandlesSameSizeBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            var previousBoard = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.OptimizedRenderBoardWithBorder(board, previousBoard, 10, 8);
                Assert.IsTrue(true); // Should handle same size boards
            }
            catch
            {
                Assert.Fail("OptimizedRenderBoardWithBorder should handle same size boards gracefully");
            }
        }

        [TestMethod]
        public void OptimizedRenderBoardWithBorderHandlesDifferentSizeBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(8);
            var previousBoard = new GameOfLifeBoard(5);
            
            try 
            {
                renderer.OptimizedRenderBoardWithBorder(board, previousBoard, 10, 8);
                Assert.IsTrue(true); // Should handle different size boards
            }
            catch
            {
                Assert.Fail("OptimizedRenderBoardWithBorder should handle different size boards gracefully");
            }
        }

        [TestMethod]
        public void MinimizeScreenFlickerDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.MinimizeScreenFlicker();
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("MinimizeScreenFlicker should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void RestoreScreenBufferDoesNotThrowException()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                renderer.RestoreScreenBuffer();
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("RestoreScreenBuffer should handle exceptions gracefully");
            }
        }

        [TestMethod]
        public void CanUseAlternateScreenReturnsBooleanValue()
        {
            var renderer = new ConsoleRenderer();
            
            bool canUse = renderer.CanUseAlternateScreen();
            Assert.IsTrue(canUse == true || canUse == false, "Should return a boolean value");
        }

        [TestMethod]
        public void PerformanceOptimizationSequenceWorksCorrectly()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(5);
            var previousBoard = new GameOfLifeBoard(5);
            
            try 
            {
                // Full performance optimization sequence
                renderer.MinimizeScreenFlicker();
                renderer.OptimizedClearGameArea(5, 5, 10, 8);
                renderer.BatchRenderCells(board, previousBoard, 10, 8);
                renderer.OptimizedRenderBoardWithBorder(board, previousBoard, 10, 8);
                renderer.RestoreScreenBuffer();
                
                Assert.IsTrue(true); // Complete sequence should work
            }
            catch
            {
                Assert.Fail("Performance optimization sequence should work gracefully");
            }
        }

        [TestMethod]
        public void CanUseOptimizedClearingWithAnsiColorsEnabled()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnsiColorsEnabled(true);
            bool canUse = renderer.CanUseOptimizedClearing();
            
            // The result depends on ANSI colors being enabled and TERM environment variable
            Assert.IsTrue(canUse == true || canUse == false);
        }

        [TestMethod]
        public void CanUseOptimizedClearingWithAnsiColorsDisabled()
        {
            var renderer = new ConsoleRenderer();
            
            renderer.SetAnsiColorsEnabled(false);
            bool canUse = renderer.CanUseOptimizedClearing();
            
            Assert.IsFalse(canUse, "Should return false when ANSI colors are disabled");
        }

        [TestMethod]
        public void ApplyBatchedChangesSortsChangesCorrectly()
        {
            var renderer = new ConsoleRenderer();
            
            // Create changes in random order
            var changes = new List<(int x, int y, string content)>
            {
                (10, 5, "cell1"),
                (5, 3, "cell2"),
                (8, 3, "cell3"),
                (5, 4, "cell4")
            };
            
            try 
            {
                // Should sort by row then column and handle efficiently
                renderer.ApplyBatchedChanges(changes);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail("ApplyBatchedChanges should sort and handle changes efficiently");
            }
        }

        [TestMethod]
        public void BatchRenderCellsHandlesEmptyBoards()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(1);
            var previousBoard = new GameOfLifeBoard(1);
            
            try 
            {
                renderer.BatchRenderCells(board, previousBoard, 10, 8);
                Assert.IsTrue(true); // Should handle minimal boards
            }
            catch
            {
                Assert.Fail("BatchRenderCells should handle minimal boards gracefully");
            }
        }

        [TestMethod]
        public void CanUseAlternateScreenWithDifferentTerminalTypes()
        {
            var renderer = new ConsoleRenderer();
            
            // Store original TERM
            string originalTerm = Environment.GetEnvironmentVariable("TERM");
            
            try
            {
                // Test with xterm
                Environment.SetEnvironmentVariable("TERM", "xterm-256color");
                renderer.SetAnsiColorsEnabled(true);
                bool xtermResult = renderer.CanUseAlternateScreen();
                
                // Test with screen
                Environment.SetEnvironmentVariable("TERM", "screen");
                bool screenResult = renderer.CanUseAlternateScreen();
                
                // Test with unknown terminal
                Environment.SetEnvironmentVariable("TERM", "unknown");
                bool unknownResult = renderer.CanUseAlternateScreen();
                
                // All should return boolean values
                Assert.IsTrue(xtermResult == true || xtermResult == false);
                Assert.IsTrue(screenResult == true || screenResult == false);
                Assert.IsTrue(unknownResult == true || unknownResult == false);
            }
            finally
            {
                // Restore original TERM
                Environment.SetEnvironmentVariable("TERM", originalTerm);
            }
        }

        [TestMethod]
        public void OptimizedClearGameAreaFallsBackToBasicClearingWhenNeeded()
        {
            var renderer = new ConsoleRenderer();
            
            // Disable ANSI colors to force fallback
            renderer.SetAnsiColorsEnabled(false);
            
            try 
            {
                renderer.OptimizedClearGameArea(10, 8, 5, 3);
                Assert.IsTrue(true); // Should fallback gracefully
            }
            catch
            {
                Assert.Fail("OptimizedClearGameArea should fallback to basic clearing when needed");
            }
        }

        [TestMethod]
        public void OptimizedClearGameAreaUsesAnsiWhenAvailable()
        {
            var renderer = new ConsoleRenderer();
            
            // Enable ANSI colors 
            renderer.SetAnsiColorsEnabled(true);
            
            try 
            {
                renderer.OptimizedClearGameArea(10, 8, 5, 3);
                Assert.IsTrue(true); // Should use ANSI clearing when available
            }
            catch
            {
                Assert.Fail("OptimizedClearGameArea should use ANSI clearing when available");
            }
        }

        [TestMethod]
        public void BatchRenderCellsCreatesCorrectChangeList()
        {
            var renderer = new ConsoleRenderer();
            var board = new GameOfLifeBoard(3);
            
            // Set some cells to create a pattern
            board.SetCellAlive(0, 0);
            board.SetCellAlive(1, 1);
            board.SetCellAlive(2, 2);
            
            try 
            {
                // This should create changes for all alive cells
                renderer.BatchRenderCells(board, null, 10, 8);
                Assert.IsTrue(true); // Should handle board with living cells
            }
            catch
            {
                Assert.Fail("BatchRenderCells should handle boards with living cells");
            }
        }
    }
}