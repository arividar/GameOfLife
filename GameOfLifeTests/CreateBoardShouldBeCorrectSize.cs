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

            board.SetCell(3, 5);
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
            board.SetCell(2, 2);
            Assert.AreEqual(1, board.LiveCount);
            Assert.AreEqual(0, board.NextGenerationBoard().LiveCount);            
        }

    }
}
