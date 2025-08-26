using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;

namespace GameOfLifeTests
{
    [TestClass]
    public class GameOfLifeTests
    {
        [TestMethod]
        public void CreateBoardShouldBeCorrectSize()
        {
            var board = new GameOfLifeBoard(10);
            Assert.AreEqual(10, board.Size);
        }

        [TestMethod]
        public void SetCellBringsACellToLife()
        {
            var board = new GameOfLifeBoard(10);
            var c = board.GetCell(3, 5);
            Assert.AreEqual(CellStatus.Dead, c);

            board.SetCellAlive(3, 5);
            c = board.GetCell(3, 5);
            Assert.AreEqual(CellStatus.Alive, c);
        }

        [TestMethod]
        public void NewBoardIsAllDead()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
        }

        [TestMethod]
        public void NextGenerationOfEmptyBoardIsAllDead()
        {
            var board = new GameOfLifeBoard(5);
            var nextGenBoard = board.NextGenerationBoard();
            Assert.AreEqual(0, nextGenBoard.LiveCount);
        }

        [TestMethod]
        public void LiveCountIsTwoForTwoLiveCells()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            Assert.AreEqual(2, board.LiveCount);
        }

        [TestMethod]
        public void NextGenerationOfABoardWithASingleLiveCellIsAllDead()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(2, 2);
            Assert.AreEqual(1, board.LiveCount);
            Assert.AreEqual(0, board.NextGenerationBoard().LiveCount);            
        }

        [TestMethod]
        public void CellWithTwoNeighboursLives()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            board.SetCellAlive(2, 4);
            Assert.IsTrue(board.NextGenerationBoard().IsCellAlive(2, 3));            
        }

        [TestMethod]
        public void LiveCellWithLessThanTwoNeighbourDies()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(0, 0);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            board.SetCellAlive(2, 4);
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(board.LiveNeighbourCount(2, 4) < 2 && board.LiveNeighbourCount(2, 2) < 2 && board.LiveNeighbourCount(0, 0) < 2);
            Assert.IsFalse(nextBoard.IsCellAlive(2, 4) || nextBoard.IsCellAlive(2, 2) || nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void DeadCellWithTwoNeighboursStaysDead()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(3, 2);
            board.SetCellAlive(3, 3);
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(2, 3) || nextBoard.IsCellAlive(4, 2));
        }

        [TestMethod]
        public void LiveCellWithMoreThanThreeLiveNeighboursDies()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(1, 3);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            board.SetCellAlive(2, 4);
            board.SetCellAlive(3, 3);
            board.SetCellAlive(3, 4);
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(2, 3) || nextBoard.IsCellAlive(2, 4));
        }

        [TestMethod]
        public void LiveCellWithTwoOrThreeLiveNeighboursLives()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(1, 3);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            board.SetCellAlive(2, 4);
            board.SetCellAlive(3, 2);
            Assert.IsTrue(board.IsCellAlive(1, 3));
            Assert.AreEqual(3, board.LiveNeighbourCount(1, 3));
            Assert.IsTrue(board.IsCellAlive(3, 2));
            Assert.AreEqual(2, board.LiveNeighbourCount(3, 2));
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextBoard.IsCellAlive(1, 3) && nextBoard.IsCellAlive(3, 2));
        }


        [TestMethod]
        public void DeadCellWithExactlyThreeLiveNeighboursBecomesAlive()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            board.SetCellAlive(2, 4);
            Assert.IsFalse(board.IsCellAlive(3, 3));
            Assert.AreEqual(3, board.LiveNeighbourCount(3, 3));
            Assert.IsTrue(board.NextGenerationBoard().IsCellAlive(3, 3));
        }

        [TestMethod]
        public void KillCellMakesLiveCellDead()
        {
            var board = new GameOfLifeBoard(5);
            board.SetCellAlive(2, 2);
            Assert.AreEqual(CellStatus.Alive, board.GetCell(2, 2));
            
            board.KillCell(2, 2);
            Assert.AreEqual(CellStatus.Dead, board.GetCell(2, 2));
        }

        [TestMethod]
        public void KillCellOnDeadCellHasNoEffect()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(CellStatus.Dead, board.GetCell(2, 2));
            
            board.KillCell(2, 2);
            Assert.AreEqual(CellStatus.Dead, board.GetCell(2, 2));
        }

        [TestMethod]
        public void KillCellReducesLiveCount()
        {
            var board = new GameOfLifeBoard(5);
            board.SetCellAlive(2, 2);
            board.SetCellAlive(2, 3);
            Assert.AreEqual(2, board.LiveCount);
            
            board.KillCell(2, 2);
            Assert.AreEqual(1, board.LiveCount);
        }

        [TestMethod]
        public void IsCellAliveReturnsFalseForOutOfBoundsCoordinates()
        {
            var board = new GameOfLifeBoard(5);
            
            Assert.IsFalse(board.IsCellAlive(-1, 0));
            Assert.IsFalse(board.IsCellAlive(0, -1));
            Assert.IsFalse(board.IsCellAlive(-1, -1));
            Assert.IsFalse(board.IsCellAlive(5, 0));
            Assert.IsFalse(board.IsCellAlive(0, 5));
            Assert.IsFalse(board.IsCellAlive(5, 5));
            Assert.IsFalse(board.IsCellAlive(10, 10));
        }

        [TestMethod]
        public void CellsAtBoardEdgesCanBeSetAndRetrieved()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 0);
            board.SetCellAlive(0, 4);
            board.SetCellAlive(4, 0);
            board.SetCellAlive(4, 4);
            
            Assert.IsTrue(board.IsCellAlive(0, 0));
            Assert.IsTrue(board.IsCellAlive(0, 4));
            Assert.IsTrue(board.IsCellAlive(4, 0));
            Assert.IsTrue(board.IsCellAlive(4, 4));
            Assert.AreEqual(4, board.LiveCount);
        }

        [TestMethod]
        public void EdgeCellsCanBeKilled()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 0);
            board.SetCellAlive(0, 4);
            board.SetCellAlive(4, 0);
            board.SetCellAlive(4, 4);
            Assert.AreEqual(4, board.LiveCount);
            
            board.KillCell(0, 0);
            board.KillCell(4, 4);
            
            Assert.IsFalse(board.IsCellAlive(0, 0));
            Assert.IsFalse(board.IsCellAlive(4, 4));
            Assert.AreEqual(2, board.LiveCount);
        }

        [TestMethod]
        public void CornerCellHasCorrectNeighborCount()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 1);
            board.SetCellAlive(1, 0);
            board.SetCellAlive(1, 1);
            
            Assert.AreEqual(3, board.LiveNeighbourCount(0, 0));
        }

        [TestMethod]
        public void EdgeCellHasCorrectNeighborCount()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 1);
            board.SetCellAlive(1, 1);
            board.SetCellAlive(2, 1);
            board.SetCellAlive(0, 2);
            board.SetCellAlive(2, 2);
            
            Assert.AreEqual(5, board.LiveNeighbourCount(1, 2));
        }

        [TestMethod]
        public void CornerCellWithThreeNeighboursBecomesAlive()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 1);
            board.SetCellAlive(1, 0);
            board.SetCellAlive(1, 1);
            
            Assert.IsFalse(board.IsCellAlive(0, 0));
            Assert.AreEqual(3, board.LiveNeighbourCount(0, 0));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void EdgeCellWithTwoNeighboursStaysAlive()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 2);
            board.SetCellAlive(1, 1);
            board.SetCellAlive(1, 3);
            
            Assert.IsTrue(board.IsCellAlive(0, 2));
            Assert.AreEqual(2, board.LiveNeighbourCount(0, 2));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextBoard.IsCellAlive(0, 2));
        }

        [TestMethod]
        public void BoardWithSizeOneWorksCorrectly()
        {
            var board = new GameOfLifeBoard(1);
            Assert.AreEqual(1, board.Size);
            Assert.AreEqual(0, board.LiveCount);
            
            board.SetCellAlive(0, 0);
            Assert.AreEqual(1, board.LiveCount);
            Assert.IsTrue(board.IsCellAlive(0, 0));
            
            Assert.AreEqual(0, board.LiveNeighbourCount(0, 0));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void BoardWithSizeZeroCreatesEmptyBoard()
        {
            var board = new GameOfLifeBoard(0);
            Assert.AreEqual(0, board.Size);
            Assert.AreEqual(0, board.LiveCount);
        }

        [TestMethod]
        public void LargeBoardWorksCorrectly()
        {
            var board = new GameOfLifeBoard(50);
            Assert.AreEqual(50, board.Size);
            Assert.AreEqual(0, board.LiveCount);
            
            board.SetCellAlive(25, 25);
            board.SetCellAlive(25, 26);
            board.SetCellAlive(25, 27);
            
            Assert.AreEqual(3, board.LiveCount);
            Assert.AreEqual(2, board.LiveNeighbourCount(25, 26));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextBoard.IsCellAlive(25, 26));
            Assert.IsTrue(nextBoard.IsCellAlive(24, 26));
            Assert.IsTrue(nextBoard.IsCellAlive(26, 26));
        }

        [TestMethod]
        public void CornerCellWithLessThanTwoNeighboursDies()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 0);
            board.SetCellAlive(0, 1);
            
            Assert.IsTrue(board.IsCellAlive(0, 0));
            Assert.AreEqual(1, board.LiveNeighbourCount(0, 0));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void EdgeCellWithMoreThanThreeNeighboursDies()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(1, 0);
            board.SetCellAlive(0, 0);
            board.SetCellAlive(2, 0);
            board.SetCellAlive(0, 1);
            board.SetCellAlive(1, 1);
            board.SetCellAlive(2, 1);
            
            Assert.IsTrue(board.IsCellAlive(1, 0));
            Assert.AreEqual(5, board.LiveNeighbourCount(1, 0));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(1, 0));
        }

        [TestMethod]
        public void DeadCellAtBoundaryWithExactlyThreeNeighboursBecomesAlive()
        {
            var board = new GameOfLifeBoard(5);
            
            board.SetCellAlive(0, 1);
            board.SetCellAlive(1, 0);
            board.SetCellAlive(1, 1);
            
            Assert.IsFalse(board.IsCellAlive(0, 0));
            Assert.AreEqual(3, board.LiveNeighbourCount(0, 0));
            
            var nextBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void NextGenerationOfEmptyBoundaryStaysEmpty()
        {
            var board = new GameOfLifeBoard(3);
            
            Assert.AreEqual(0, board.LiveCount);
            
            var nextBoard = board.NextGenerationBoard();
            Assert.AreEqual(0, nextBoard.LiveCount);
            
            Assert.IsFalse(nextBoard.IsCellAlive(0, 0));
            Assert.IsFalse(nextBoard.IsCellAlive(0, 2));
            Assert.IsFalse(nextBoard.IsCellAlive(2, 0));
            Assert.IsFalse(nextBoard.IsCellAlive(2, 2));
        }

        // Program class tests
        [TestMethod]
        public void SetGenerationZeroCreatesPredefinedPattern()
        {
            var board = new GameOfLifeBoard(10);
            Assert.AreEqual(0, board.LiveCount);
            
            Program.SetGenerationZero(board);
            
            // Verify the pattern was set correctly
            Assert.AreEqual(23, board.LiveCount);
            
            // Test specific cells that should be alive based on the pattern
            Assert.IsTrue(board.IsCellAlive(1, 0));
            Assert.IsTrue(board.IsCellAlive(1, 1));
            Assert.IsTrue(board.IsCellAlive(1, 2));
            Assert.IsTrue(board.IsCellAlive(1, 3));
            Assert.IsTrue(board.IsCellAlive(2, 0));
            Assert.IsTrue(board.IsCellAlive(2, 1));
            Assert.IsTrue(board.IsCellAlive(2, 2));
            Assert.IsTrue(board.IsCellAlive(2, 3));
            Assert.IsTrue(board.IsCellAlive(3, 4));
            Assert.IsTrue(board.IsCellAlive(3, 5));
            Assert.IsTrue(board.IsCellAlive(6, 2));
            Assert.IsTrue(board.IsCellAlive(6, 3));
            Assert.IsTrue(board.IsCellAlive(6, 4));
            Assert.IsTrue(board.IsCellAlive(7, 2));
            Assert.IsTrue(board.IsCellAlive(7, 3));
            Assert.IsTrue(board.IsCellAlive(8, 4));
            Assert.IsTrue(board.IsCellAlive(8, 5));
            Assert.IsTrue(board.IsCellAlive(9, 4));
            Assert.IsTrue(board.IsCellAlive(9, 5));
            Assert.IsTrue(board.IsCellAlive(9, 6));
            Assert.IsTrue(board.IsCellAlive(9, 7));
            Assert.IsTrue(board.IsCellAlive(9, 8));
            Assert.IsTrue(board.IsCellAlive(9, 9));
        }

        [TestMethod]
        public void SetGenerationZeroWorksWithLargeBoardSizes()
        {
            var largeBoard = new GameOfLifeBoard(15);
            Assert.AreEqual(0, largeBoard.LiveCount);
            
            Program.SetGenerationZero(largeBoard);
            
            // Should set all 23 cells since board is large enough
            Assert.AreEqual(23, largeBoard.LiveCount);
            
            // Verify some key positions
            Assert.IsTrue(largeBoard.IsCellAlive(1, 0));
            Assert.IsTrue(largeBoard.IsCellAlive(9, 9));
        }

        [TestMethod]
        public void PrintBoardDoesNotThrowException()
        {
            var board = new GameOfLifeBoard(3);
            board.SetCellAlive(1, 1);
            
            // This test verifies PrintBoard can be called without throwing exceptions
            // We can't easily test console output, but we can ensure it executes
            try 
            {
                Program.PrintBoard(0, board);
                Program.PrintBoard(5, board);
                Program.PrintBoard(-1, board);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("PrintBoard should not throw exceptions");
            }
        }

        [TestMethod]
        public void PrintBoardHandlesEmptyBoard()
        {
            var board = new GameOfLifeBoard(2);
            
            try 
            {
                Program.PrintBoard(0, board);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("PrintBoard should handle empty boards without throwing exceptions");
            }
        }

        [TestMethod]
        public void PrintBoardHandlesLargeBoard()
        {
            var board = new GameOfLifeBoard(10);
            Program.SetGenerationZero(board);
            
            try 
            {
                Program.PrintBoard(1, board);
                Assert.IsTrue(true); // If we reach here, no exception was thrown
            }
            catch
            {
                Assert.Fail("PrintBoard should handle large boards without throwing exceptions");
            }
        }

        // ConsoleRenderer tests
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
