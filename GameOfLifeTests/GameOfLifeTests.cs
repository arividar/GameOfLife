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

            board.SetAlive(3, 5);
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
        public void NextGenerationOfABoardWithASingleLiveCellIsAllDead()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetAlive(2, 2);
            Assert.AreEqual(1, board.LiveCount);
            Assert.AreEqual(0, board.NextGenerationBoard().LiveCount);            
        }

        [TestMethod]
        public void CellWithTwoNeighboursLives()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetAlive(2, 2);
            board.SetAlive(2, 3);
            board.SetAlive(2, 4);
            Assert.IsTrue(board.NextGenerationBoard().IsCellAlive(2, 3));            
        }

        [TestMethod]
        public void LiveCellWithLessThanTwoNeighbourDies()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetAlive(0, 0);
            board.SetAlive(2, 2);
            board.SetAlive(2, 3);
            board.SetAlive(2, 4);
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(2, 4) || nextBoard.IsCellAlive(2, 2) || nextBoard.IsCellAlive(0, 0));
        }

        [TestMethod]
        public void LiveCellWithMoreThanThreeLiveNeighboursDies()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetAlive(1, 3);
            board.SetAlive(2, 2);
            board.SetAlive(2, 3);
            board.SetAlive(2, 4);
            board.SetAlive(3, 3);
            board.SetAlive(3, 4);
            var nextBoard = board.NextGenerationBoard();
            Assert.IsFalse(nextBoard.IsCellAlive(2, 3) || nextBoard.IsCellAlive(2, 4));
        }

        [TestMethod]
        public void DeadCellWithExactlyThreeLiveNeighboursBecomesAlive()
        {
            var board = new GameOfLifeBoard(5);
            Assert.AreEqual(0, board.LiveCount);
            board.SetAlive(2, 2);
            board.SetAlive(2, 3);
            board.SetAlive(2, 4);
            Assert.IsFalse(board.IsCellAlive(3, 3));
            Assert.AreEqual(3, board.LiveNeighbourCount(3,3));
            Assert.IsTrue(board.NextGenerationBoard().IsCellAlive(3,3));
        }

    }
}
