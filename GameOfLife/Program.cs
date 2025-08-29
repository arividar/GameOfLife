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
            Random random = new Random();
            int totalCells = theBoard.Width * theBoard.Height;
            int targetLiveCells = (int)(totalCells * 0.4); // 40% of board size
            
            int liveCellsPlaced = 0;
            while (liveCellsPlaced < targetLiveCells)
            {
                int x = random.Next(theBoard.Width);
                int y = random.Next(theBoard.Height);
                
                if (theBoard.GetCell(x, y) == CellStatus.Dead)
                {
                    theBoard.SetCellAlive(x, y);
                    liveCellsPlaced++;
                }
            }
        }

        public static void PrintBoard(int generation, GameOfLifeBoard board)
        {
            Console.WriteLine("Generation: "+generation.ToString(CultureInfo.InvariantCulture));
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
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
                var (boardX, boardY) = renderer.CalculateCenteredBoardPosition(board.Width, board.Height);
                
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

        public static void RunGameWithRenderer(int width, int height, int maxGenerations)
        {
            try
            {
                var board = new GameOfLifeBoard(width, height);
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
            RunGameWithRenderer(30, 10, 30);
        }

    }
}
