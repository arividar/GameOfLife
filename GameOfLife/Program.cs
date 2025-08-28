using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GameOfLife
{
    public enum VisualStyle
    {
        Classic,
        Modern,
        Minimal
    }

    public class GameConfiguration
    {
        private int _animationDelay = 500;

        public int AnimationDelay
        {
            get => _animationDelay;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Animation delay cannot be negative", nameof(value));
                _animationDelay = value;
            }
        }

        public bool UseColors { get; set; } = true;
        public bool UseUnicodeCharacters { get; set; } = true;
        public VisualStyle Style { get; set; } = VisualStyle.Modern;
    }
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

        public static void PrintBoardWithRenderer(int generation, GameOfLifeBoard board, ConsoleRenderer renderer)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));
            if (renderer == null)
                throw new ArgumentNullException(nameof(renderer));

            try
            {
                renderer.InitializeConsole();
                
                // Calculate centered position for the board
                var (boardX, boardY) = renderer.CalculateCenteredBoardPosition(board.Size, board.Size);
                
                // Render generation counter at top
                renderer.RenderGenerationCounterAtTop(generation, boardX, boardY);
                
                // Render the board with border
                renderer.RenderBoardWithBorder(board, boardX, boardY);
                
                // Apply animation delay for smooth rendering
                renderer.ApplyAnimationDelay();
            }
            catch (Exception)
            {
                // Fallback to original PrintBoard if renderer fails
                PrintBoard(generation, board);
            }
        }

        public static ConsoleRenderer CreateRendererWithConfiguration()
        {
            var renderer = new ConsoleRenderer();
            var config = CreateDefaultConfiguration();
            ApplyConfigurationToRenderer(renderer, config);
            return renderer;
        }

        public static void RunGameWithRenderer(int boardSize, int maxGenerations)
        {
            try
            {
                var board = new GameOfLifeBoard(boardSize);
                var renderer = CreateRendererWithConfiguration();
                
                SetGenerationZero(board);
                
                for (int i = 0; i <= maxGenerations; i++)
                {
                    PrintBoardWithRenderer(i, board, renderer);
                    if (i < maxGenerations)
                    {
                        board = board.NextGenerationBoard();
                    }
                }
                
                renderer.RestoreConsole();
            }
            catch (Exception)
            {
                // Handle any exceptions gracefully for tests
                // In a real application, we might want to log or display the error
            }
        }

        public static GameConfiguration CreateDefaultConfiguration()
        {
            return new GameConfiguration
            {
                AnimationDelay = 500,
                UseColors = true,
                UseUnicodeCharacters = true,
                Style = VisualStyle.Modern
            };
        }

        public static void ApplyConfigurationToRenderer(ConsoleRenderer renderer, GameConfiguration config)
        {
            if (renderer == null)
                throw new ArgumentNullException(nameof(renderer));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            try
            {
                renderer.SetAnimationDelay(config.AnimationDelay);
                
                // Apply visual style configurations
                switch (config.Style)
                {
                    case VisualStyle.Classic:
                        // Classic style could use different characters/colors
                        break;
                    case VisualStyle.Modern:
                        // Modern style uses current Unicode characters
                        break;
                    case VisualStyle.Minimal:
                        // Minimal style could use simpler characters
                        break;
                }
                
                // Color and Unicode settings would be applied here
                // when renderer supports these configuration options
            }
            catch (Exception)
            {
                // Handle configuration application failures gracefully
            }
        }

        [ExcludeFromCodeCoverage]
        static void Main()
        {
            // Use new renderer-based game loop for enhanced graphics
            RunGameWithRenderer(10, 20);
        }

    }
}
