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

    }
}
