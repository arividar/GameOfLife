using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GameOfLife
{
    public class Program
    {
        public static void SetGenerationZero(GameOfLifeBoard theBoard)
        {
            theBoard.SetCellAlive(1, 0);
            theBoard.SetCellAlive(1, 1);
            theBoard.SetCellAlive(1, 2);
            theBoard.SetCellAlive(1, 3);
            theBoard.SetCellAlive(2, 0);
            theBoard.SetCellAlive(2, 1);
            theBoard.SetCellAlive(2, 2);
            theBoard.SetCellAlive(2, 3);
            theBoard.SetCellAlive(3, 4);
            theBoard.SetCellAlive(3, 5);
            theBoard.SetCellAlive(6, 2);
            theBoard.SetCellAlive(6, 3);
            theBoard.SetCellAlive(6, 4);
            theBoard.SetCellAlive(7, 2);
            theBoard.SetCellAlive(7, 3);
            theBoard.SetCellAlive(8, 4);
            theBoard.SetCellAlive(8, 5);
            theBoard.SetCellAlive(9, 4);
            theBoard.SetCellAlive(9, 5);
            theBoard.SetCellAlive(9, 6);
            theBoard.SetCellAlive(9, 7);
            theBoard.SetCellAlive(9, 8);
            theBoard.SetCellAlive(9, 9);
        }

        public static void PrintBoard(int generation, GameOfLifeBoard board)
        {
            Console.WriteLine("Generation: "+generation.ToString(CultureInfo.InvariantCulture));
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                { 
                    Console.Write(board.GetCell(x, y) == CellStatus.Dead ? " . " : " O ");
                }
                Console.WriteLine();
            }

        }

        [ExcludeFromCodeCoverage]
        static void Main()
        {
            var board = new GameOfLifeBoard(10);
            SetGenerationZero(board);
            for (int i = 0; i <= 20; i++)
            {
                PrintBoard(i, board);
                board = board.NextGenerationBoard();
            }
            Console.ReadLine();
        }

    }
}
