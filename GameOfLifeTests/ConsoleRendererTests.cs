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
    }
}