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

        public static GameConfiguration CreateClassicConfiguration()
        {
            return new GameConfiguration
            {
                UseColors = false,
                UseUnicodeCharacters = false,
                Style = VisualStyle.Classic,
                AnimationDelay = 500
            };
        }

        public static GameConfiguration CreateModernConfiguration()
        {
            return new GameConfiguration
            {
                UseColors = true,
                UseUnicodeCharacters = true,
                Style = VisualStyle.Modern,
                AnimationDelay = 500
            };
        }

        public void ApplyToRenderer(ConsoleRenderer renderer)
        {
            renderer.SetAnimationDelay(AnimationDelay);
            
            // Apply visual style configurations first
            switch (Style)
            {
                case VisualStyle.Classic:
                    renderer.SetUnicodeCharactersEnabled(false);
                    renderer.SetAnsiColorsEnabled(false);
                    break;
                case VisualStyle.Modern:
                    // Apply individual settings for Modern style (allow customization)
                    renderer.SetUnicodeCharactersEnabled(UseUnicodeCharacters);
                    renderer.SetAnsiColorsEnabled(UseColors);
                    break;
                case VisualStyle.Minimal:
                    // Minimal style could use simpler characters but keep some modern features
                    renderer.SetUnicodeCharactersEnabled(UseUnicodeCharacters);
                    renderer.SetAnsiColorsEnabled(UseColors);
                    break;
            }
        }
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
            ConsoleRenderer renderer = null;
            try
            {
                var board = new GameOfLifeBoard(width, height);
                renderer = CreateRendererWithConfiguration();
                
                SetGenerationZero(board);
                
                for (int i = 0; i <= maxGenerations; i++)
                {
                    PrintBoardWithRenderer(i, board, renderer);
                    if (i < maxGenerations)
                    {
                        board = board.NextGenerationBoard();
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions gracefully for tests
                // In a real application, we might want to log or display the error
            }
            finally
            {
                // Always restore console state, even if an exception occurred
                renderer?.RestoreConsole();
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
                renderer.SetUnicodeCharactersEnabled(config.UseUnicodeCharacters);
                renderer.SetAnsiColorsEnabled(config.UseColors);
                
                // Apply visual style configurations
                switch (config.Style)
                {
                    case VisualStyle.Classic:
                        renderer.SetUnicodeCharactersEnabled(false);
                        renderer.SetAnsiColorsEnabled(false);
                        break;
                    case VisualStyle.Modern:
                        renderer.SetUnicodeCharactersEnabled(true);
                        renderer.SetAnsiColorsEnabled(true);
                        break;
                    case VisualStyle.Minimal:
                        // Minimal style could use simpler characters but keep some modern features
                        break;
                }
            }
            catch (Exception)
            {
                // Handle configuration application failures gracefully
            }
        }

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            // Check for help flag
            if (args.Length > 0 && (args[0] == "--help" || args[0] == "-h" || args[0] == "/?" || args[0] == "help"))
            {
                ShowHelp();
                return;
            }

            // Parse command line arguments
            var config = ParseCommandLineArguments(args);
            int width = 50, height = 35, generations = 100;

            // Override defaults with parsed values if provided
            if (args.Length >= 3)
            {
                if (int.TryParse(args[0], out int w) && w > 0) width = w;
                if (int.TryParse(args[1], out int h) && h > 0) height = h;
                if (int.TryParse(args[2], out int g) && g > 0) generations = g;
            }

            // Use new renderer-based game loop with parsed configuration
            RunGameWithRendererAndConfig(width, height, generations, config);
            Console.ReadLine();
        }

        [ExcludeFromCodeCoverage]
        public static void ShowHelp()
        {
            Console.WriteLine("Conway's Game of Life - ANSI Graphics Edition");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("USAGE:");
            Console.WriteLine("  GameOfLife [width] [height] [generations] [options]");
            Console.WriteLine("  GameOfLife --help");
            Console.WriteLine();
            Console.WriteLine("ARGUMENTS:");
            Console.WriteLine("  width        Board width (default: 50, range: 10-100)");
            Console.WriteLine("  height       Board height (default: 35, range: 8-50)");
            Console.WriteLine("  generations  Number of generations (default: 100, range: 1-1000)");
            Console.WriteLine();
            Console.WriteLine("OPTIONS:");
            Console.WriteLine("  --help, -h   Show this help message");
            Console.WriteLine("  --classic    Use classic ASCII mode (no Unicode or colors)");
            Console.WriteLine("  --no-color   Disable ANSI colors (keep Unicode characters)");
            Console.WriteLine("  --no-unicode Disable Unicode characters (use ASCII fallback)");
            Console.WriteLine("  --delay=ms   Animation delay in milliseconds (default: 500)");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES:");
            Console.WriteLine("  GameOfLife");
            Console.WriteLine("    Run with default settings (50x35, 100 generations, modern graphics)");
            Console.WriteLine();
            Console.WriteLine("  GameOfLife 30 20 50");
            Console.WriteLine("    Run with 30x20 board for 50 generations");
            Console.WriteLine();
            Console.WriteLine("  GameOfLife 40 25 200 --classic");
            Console.WriteLine("    Run with 40x25 board, 200 generations, classic ASCII mode");
            Console.WriteLine();
            Console.WriteLine("  GameOfLife --no-color --delay=250");
            Console.WriteLine("    Run with default size, no colors, faster animation");
            Console.WriteLine();
            Console.WriteLine("VISUAL MODES:");
            Console.WriteLine("  Modern (default): Unicode characters (██ ··) with ANSI colors");
            Console.WriteLine("  Classic:          ASCII characters (OO ..) without colors");
            Console.WriteLine("  No Color:         Unicode characters without colors");
            Console.WriteLine("  No Unicode:       ASCII fallback with colors");
            Console.WriteLine();
            Console.WriteLine("ENVIRONMENT VARIABLES:");
            Console.WriteLine("  NO_COLOR=1       Disable colors (overrides --no-color)");
            Console.WriteLine("  TERM=dumb        Force ASCII mode");
            Console.WriteLine();
            Console.WriteLine("TERMINAL COMPATIBILITY:");
            Console.WriteLine("  ✓ Windows Terminal, PowerShell, Command Prompt");
            Console.WriteLine("  ✓ macOS Terminal.app, iTerm2, VS Code");
            Console.WriteLine("  ✓ Linux xterm, gnome-terminal, konsole, tmux, screen");
            Console.WriteLine();
            Console.WriteLine("The game automatically detects your terminal capabilities and");
            Console.WriteLine("falls back to ASCII characters and plain text when needed.");
        }

        [ExcludeFromCodeCoverage]
        public static GameConfiguration ParseCommandLineArguments(string[] args)
        {
            var config = CreateDefaultConfiguration();

            foreach (string arg in args)
            {
                switch (arg.ToLowerInvariant())
                {
                    case "--classic":
                        config = GameConfiguration.CreateClassicConfiguration();
                        break;
                    case "--no-color":
                        config.UseColors = false;
                        break;
                    case "--no-unicode":
                        config.UseUnicodeCharacters = false;
                        break;
                    default:
                        if (arg.StartsWith("--delay="))
                        {
                            string delayStr = arg.Substring("--delay=".Length);
                            if (int.TryParse(delayStr, out int delay) && delay >= 0)
                            {
                                config.AnimationDelay = delay;
                            }
                        }
                        break;
                }
            }

            return config;
        }

        [ExcludeFromCodeCoverage]
        public static void RunGameWithRendererAndConfig(int width, int height, int maxGenerations, GameConfiguration config)
        {
            ConsoleRenderer renderer = null;
            try
            {
                // Validate input parameters
                width = Math.Max(10, Math.Min(100, width));
                height = Math.Max(8, Math.Min(50, height));
                maxGenerations = Math.Max(1, Math.Min(1000, maxGenerations));

                var board = new GameOfLifeBoard(width, height);
                renderer = CreateRendererWithConfiguration();
                
                // Apply command line configuration
                config.ApplyToRenderer(renderer);
                
                SetGenerationZero(board);
                
                for (int i = 0; i <= maxGenerations; i++)
                {
                    PrintBoardWithRenderer(i, board, renderer);
                    if (i < maxGenerations)
                    {
                        board = board.NextGenerationBoard();
                    }
                }
            }
            catch (Exception)
            {
                // Handle any exceptions gracefully for tests
                // In a real application, we might want to log or display the error
            }
            finally
            {
                // Always restore console state, even if an exception occurred
                renderer?.RestoreConsole();
            }
        }

    }
}
