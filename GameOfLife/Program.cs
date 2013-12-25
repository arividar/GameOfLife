using System;

namespace GameOfLife
{
    class Program
    {
        private static void SetInitialPosition(GameOfLifeBoard initialBoard)
        {
            initialBoard.SetAlive(2, 3);
            initialBoard.SetAlive(3, 4);
            initialBoard.SetAlive(3, 5);
            initialBoard.SetAlive(6, 3);
            initialBoard.SetAlive(6, 4);
            initialBoard.SetAlive(6, 3);
            initialBoard.SetAlive(7, 2);
            initialBoard.SetAlive(8, 5);
            initialBoard.SetAlive(9, 4);
        }

        private static void PrintBoard(int generation, GameOfLifeBoard board)
        {
            Console.WriteLine("Generation: "+generation.ToString());
            for (int x = 0; x < board.Size - 1; x++)
            {
                for (int y = 0; y < board.Size - 1; y++)
                { 
                    Console.Write(board.GetCell(x, y) == CellStatus.Dead ? " . " : " X ");
                }
                Console.WriteLine();
            }

        }

        static void Main()
        {
            var initialBoard = new GameOfLifeBoard(10);
            SetInitialPosition(initialBoard);
            PrintBoard(1, initialBoard);
            PrintBoard(2, initialBoard.NextGenerationBoard());
            Console.ReadLine();
        }

    }
}
