using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;

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
    }
}