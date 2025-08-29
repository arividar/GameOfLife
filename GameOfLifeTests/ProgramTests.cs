using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System;
using System.IO;

namespace GameOfLifeTests
{
    [TestClass]
    public class ProgramTests
    {
        // Phase 4 Step 8: Program.cs Integration Tests

        [TestMethod]
        public void PrintBoardWithRendererDoesNotThrowException()
        {
            var board = new GameOfLifeBoard(5);
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(1, board, renderer);
                Assert.IsTrue(true); // Should not throw exceptions
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should not throw exceptions");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesNullBoard()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(0, null, renderer);
                Assert.Fail("PrintBoardWithRenderer should throw exception for null board");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); // Expected exception for null board
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should throw ArgumentNullException for null board");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesNullRenderer()
        {
            var board = new GameOfLifeBoard(3);
            
            try 
            {
                Program.PrintBoardWithRenderer(0, board, null);
                Assert.Fail("PrintBoardWithRenderer should throw exception for null renderer");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); // Expected exception for null renderer
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should throw ArgumentNullException for null renderer");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesZeroGeneration()
        {
            var board = new GameOfLifeBoard(3);
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(0, board, renderer);
                Assert.IsTrue(true); // Should handle zero generation
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should handle zero generation");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesLargeGeneration()
        {
            var board = new GameOfLifeBoard(3);
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(999999, board, renderer);
                Assert.IsTrue(true); // Should handle large generation numbers
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should handle large generation numbers");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesSmallBoard()
        {
            var board = new GameOfLifeBoard(1);
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(5, board, renderer);
                Assert.IsTrue(true); // Should handle 1x1 board
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should handle small boards");
            }
        }

        [TestMethod]
        public void PrintBoardWithRendererHandlesLargeBoard()
        {
            var board = new GameOfLifeBoard(50);
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.PrintBoardWithRenderer(3, board, renderer);
                Assert.IsTrue(true); // Should handle large boards
            }
            catch
            {
                Assert.Fail("PrintBoardWithRenderer should handle large boards");
            }
        }

        [TestMethod]
        public void CreateRendererWithConfigurationReturnsValidRenderer()
        {
            try
            {
                var renderer = Program.CreateRendererWithConfiguration();
                Assert.IsNotNull(renderer, "CreateRendererWithConfiguration should return non-null renderer");
                Assert.IsInstanceOfType(renderer, typeof(ConsoleRenderer), "Should return ConsoleRenderer instance");
            }
            catch
            {
                Assert.Fail("CreateRendererWithConfiguration should not throw exceptions");
            }
        }

        [TestMethod]
        public void RunGameWithRendererDoesNotThrowException()
        {
            try
            {
                Program.RunGameWithRenderer(5, 5, 3); // Small board, few generations
                Assert.IsTrue(true); // Should complete without exceptions
            }
            catch
            {
                Assert.Fail("RunGameWithRenderer should not throw exceptions");
            }
        }

        [TestMethod]
        public void RunGameWithRendererHandlesZeroGenerations()
        {
            try
            {
                Program.RunGameWithRenderer(5, 5, 0); // Zero generations should just show initial state
                Assert.IsTrue(true); // Should handle zero generations
            }
            catch
            {
                Assert.Fail("RunGameWithRenderer should handle zero generations");
            }
        }

        [TestMethod]
        public void RunGameWithRendererHandlesSmallBoard()
        {
            try
            {
                Program.RunGameWithRenderer(1, 1, 2); // Minimum board size
                Assert.IsTrue(true); // Should handle small boards
            }
            catch
            {
                Assert.Fail("RunGameWithRenderer should handle small boards");
            }
        }

        // Phase 4 Step 9: Configuration Options Tests

        [TestMethod]
        public void GameConfigurationCanBeInstantiated()
        {
            var config = new GameConfiguration();
            Assert.IsNotNull(config, "GameConfiguration should be instantiable");
        }

        [TestMethod]
        public void GameConfigurationHasDefaultValues()
        {
            var config = new GameConfiguration();
            
            Assert.IsTrue(config.AnimationDelay > 0, "Default animation delay should be positive");
            Assert.IsTrue(config.UseColors, "Default should use colors");
            Assert.IsTrue(config.UseUnicodeCharacters, "Default should use Unicode characters");
            Assert.AreEqual(VisualStyle.Modern, config.Style, "Default style should be Modern");
        }

        [TestMethod]
        public void GameConfigurationCanSetAnimationDelay()
        {
            var config = new GameConfiguration();
            config.AnimationDelay = 750;
            
            Assert.AreEqual(750, config.AnimationDelay, "Should set animation delay");
        }

        [TestMethod]
        public void GameConfigurationCanToggleColors()
        {
            var config = new GameConfiguration();
            config.UseColors = false;
            
            Assert.IsFalse(config.UseColors, "Should disable colors");
            
            config.UseColors = true;
            Assert.IsTrue(config.UseColors, "Should enable colors");
        }

        [TestMethod]
        public void GameConfigurationCanToggleUnicodeCharacters()
        {
            var config = new GameConfiguration();
            config.UseUnicodeCharacters = false;
            
            Assert.IsFalse(config.UseUnicodeCharacters, "Should disable Unicode characters");
            
            config.UseUnicodeCharacters = true;
            Assert.IsTrue(config.UseUnicodeCharacters, "Should enable Unicode characters");
        }

        [TestMethod]
        public void GameConfigurationCanSetVisualStyle()
        {
            var config = new GameConfiguration();
            
            config.Style = VisualStyle.Classic;
            Assert.AreEqual(VisualStyle.Classic, config.Style, "Should set Classic style");
            
            config.Style = VisualStyle.Minimal;
            Assert.AreEqual(VisualStyle.Minimal, config.Style, "Should set Minimal style");
            
            config.Style = VisualStyle.Modern;
            Assert.AreEqual(VisualStyle.Modern, config.Style, "Should set Modern style");
        }

        [TestMethod]
        public void GameConfigurationValidatesAnimationDelay()
        {
            var config = new GameConfiguration();
            
            try 
            {
                config.AnimationDelay = -100;
                Assert.Fail("Should reject negative animation delays");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true); // Expected exception for negative delay
            }
            catch
            {
                Assert.Fail("Should throw ArgumentException for negative delay");
            }
        }

        [TestMethod]
        public void ApplyConfigurationToRendererUpdatesRenderer()
        {
            var config = new GameConfiguration();
            var renderer = new ConsoleRenderer();
            
            config.AnimationDelay = 300;
            config.UseColors = false;
            config.Style = VisualStyle.Classic;
            
            try
            {
                Program.ApplyConfigurationToRenderer(renderer, config);
                Assert.IsTrue(true); // Should apply configuration without exceptions
            }
            catch
            {
                Assert.Fail("ApplyConfigurationToRenderer should not throw exceptions");
            }
        }

        [TestMethod]
        public void ApplyConfigurationToRendererHandlesNullRenderer()
        {
            var config = new GameConfiguration();
            
            try 
            {
                Program.ApplyConfigurationToRenderer(null, config);
                Assert.Fail("ApplyConfigurationToRenderer should throw exception for null renderer");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); // Expected exception for null renderer
            }
            catch
            {
                Assert.Fail("Should throw ArgumentNullException for null renderer");
            }
        }

        [TestMethod]
        public void ApplyConfigurationToRendererHandlesNullConfiguration()
        {
            var renderer = new ConsoleRenderer();
            
            try 
            {
                Program.ApplyConfigurationToRenderer(renderer, null);
                Assert.Fail("ApplyConfigurationToRenderer should throw exception for null configuration");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true); // Expected exception for null configuration
            }
            catch
            {
                Assert.Fail("Should throw ArgumentNullException for null configuration");
            }
        }

        [TestMethod]
        public void CreateDefaultConfigurationReturnsValidConfiguration()
        {
            var config = Program.CreateDefaultConfiguration();
            
            Assert.IsNotNull(config, "CreateDefaultConfiguration should return non-null configuration");
            Assert.IsInstanceOfType(config, typeof(GameConfiguration), "Should return GameConfiguration instance");
            Assert.IsTrue(config.AnimationDelay > 0, "Default configuration should have positive animation delay");
        }
    }
}
