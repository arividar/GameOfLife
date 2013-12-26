using System;
using System.Globalization;

namespace GameOfLife
{
    class Program
    {
        private static void SetGenerationZero(GameOfLifeBoard theBoard)
        {
            theBoard.SetAlive(1, 0);
            theBoard.SetAlive(1, 1);
            theBoard.SetAlive(1, 2);
            theBoard.SetAlive(1, 3);
            theBoard.SetAlive(2, 0);
            theBoard.SetAlive(2, 1);
            theBoard.SetAlive(2, 2);
            theBoard.SetAlive(2, 3);
            theBoard.SetAlive(3, 4);
            theBoard.SetAlive(3, 5);
            theBoard.SetAlive(6, 2);
            theBoard.SetAlive(6, 3);
            theBoard.SetAlive(6, 4);
            theBoard.SetAlive(7, 2);
            theBoard.SetAlive(7, 3);
            theBoard.SetAlive(8, 4);
            theBoard.SetAlive(8, 5);
            theBoard.SetAlive(9, 4);
            theBoard.SetAlive(9, 5);
            theBoard.SetAlive(9, 6);
            theBoard.SetAlive(9, 7);
            theBoard.SetAlive(9, 8);
            theBoard.SetAlive(9, 9);
        }

        private static void PrintBoard(int generation, GameOfLifeBoard board)
        {
            Console.WriteLine("Generation: "+generation.ToString(CultureInfo.InvariantCulture));
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                { 
                    Console.Write(board.GetCell(x, y) == CellStatus.Dead ? " . " : " X ");
                }
                Console.WriteLine();
            }

        }

        static void Main()
        {
            var board = new GameOfLifeBoard(10);
            SetGenerationZero(board);
            for (int i = 0; i <= 25; i++)
            {
                PrintBoard(i, board);
                board = board.NextGenerationBoard();
            }
            Console.ReadLine();
        }

    }
}
