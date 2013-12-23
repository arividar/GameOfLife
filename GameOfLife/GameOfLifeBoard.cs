using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public enum CellStatus { Alive, Dead }

    public class GameOfLifeBoard
    {
        private readonly CellStatus[,] _board;

        public GameOfLifeBoard(int size)
        {
            _board = new CellStatus[size, size];
            for (int i = 0; i < size; i++)
                for (int h = 0; h < size; h++)
                    _board[i, h] = CellStatus.Dead;
        }

        public int Size
        {
            get { return _board.Length; }
        }

        public void SetCell(int x, int y)
        {
            _board[x, y] = CellStatus.Alive;
        }

        public CellStatus GetCell(int x, int y)
        {
            return _board[x, y];
        }
    }
}
