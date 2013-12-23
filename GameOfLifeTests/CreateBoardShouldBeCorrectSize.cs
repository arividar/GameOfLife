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
            Assert.IsTrue(board.IsAllDead());
        }

        [TestMethod]
        public void NextGenerationOfEmptyBoardIsAllDead()
        {
            var board = new GameOfLifeBoard(5);
            var nextGenBoard = board.NextGenerationBoard();
            Assert.IsTrue(nextGenBoard.IsAllDead());
        }
    }
}
