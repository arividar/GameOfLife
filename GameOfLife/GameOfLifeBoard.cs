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
            get { return _board.GetUpperBound(0)+1; }
        }

        public int LiveCount
        {
            get
            {
                int liveCount = 0;
                for (int i = 0; i < Size; i++)
                    for (int h = 0; h < Size; h++)
                        if (_board[i, h] == CellStatus.Alive)
                            liveCount++;
                return liveCount;
            }
        }

        public void SetCellAlive(int x, int y)
        {
            _board[x, y] = CellStatus.Alive;
        }

        public CellStatus GetCell(int x, int y)
        {
            return _board[x, y];
        }

        public GameOfLifeBoard NextGenerationBoard()
        {
            var nextGenBoard = new GameOfLifeBoard(Size);
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
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
            if (x < 0 || y < 0 || x >= Size || y >= Size)
                return false;
            return (_board[x, y] == CellStatus.Alive);
        }
    }
}
