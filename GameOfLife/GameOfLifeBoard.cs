using System;

namespace GameOfLife
{
    public enum CellStatus { Alive, Dead }

    public class GameOfLifeBoard
    {
        private readonly CellStatus[,] _board;
        private readonly int _width;
        private readonly int _height;

        public int Width => _width;
        public int Height => _height;
        
        // Keep Size property for backward compatibility with square boards
        public int Size
        {
            get
            {
                if (_width != _height)
                    throw new InvalidOperationException("Size property is only valid for square boards. Use Width and Height for rectangular boards.");
                return _width;
            }
        }

        public int LiveCount
        {
            get
            {
                int liveCount = 0;
                for (int x = 0; x < _width; x++)
                    for (int y = 0; y < _height; y++)
                        if (_board[x, y] == CellStatus.Alive)
                            liveCount++;
                return liveCount;
            }
        }

        // Square board constructor for backward compatibility
        public GameOfLifeBoard(int size) : this(size, size)
        {
        }
        
        // Rectangular board constructor
        public GameOfLifeBoard(int width, int height)
        {
            _width = width;
            _height = height;
            _board = new CellStatus[width, height];
            
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                    _board[x, y] = CellStatus.Dead;
        }

        public void SetCellAlive(int x, int y)
        {
            SetCell(x, y, CellStatus.Alive);
        }        

        public void KillCell(int x, int y)
        {
            SetCell(x, y, CellStatus.Dead);
        }

        public CellStatus GetCell(int x, int y)
        {
            return _board[x, y];
        }

        public GameOfLifeBoard NextGenerationBoard()
        {
            var nextGenBoard = new GameOfLifeBoard(_width, _height);
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                {
                    nextGenBoard.SetCell(x, y, NextGenerationCell(x, y));
                }
            return nextGenBoard;
        }

        private void SetCell(int x, int y, CellStatus vitality)
        {
            _board[x, y] = vitality;
        }

        private CellStatus NextGenerationCell(int x, int y)
        {
            int liveNeighbours = LiveNeighbourCount(x, y);
            if ((liveNeighbours < 2) || (liveNeighbours > 3))
                return CellStatus.Dead;
            if (liveNeighbours == 3)
                return CellStatus.Alive;
            return _board[x, y];
        }

        public int LiveNeighbourCount(int x, int y)
        {
            return OneIfAlive(x-1, y-1) +
                   OneIfAlive(x,   y-1) +
                   OneIfAlive(x+1, y-1) +
                   OneIfAlive(x+1, y  ) +
                   OneIfAlive(x+1, y+1) +
                   OneIfAlive(x,   y+1) +
                   OneIfAlive(x-1, y+1) +
                   OneIfAlive(x-1, y  );
        }

        private int OneIfAlive(int x, int y)
        {
            return IsCellAlive(x, y) ? 1 : 0;
        }

        public bool IsCellAlive(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
                return false;
            return (_board[x, y] == CellStatus.Alive);
        }
    }
}
