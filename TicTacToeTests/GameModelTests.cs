using TicTacToeAPI.Models;
using NUnit.Framework;

namespace TicTacToeTests
{
    [TestFixture]
    public class GameModelTests
    {
        private void PopulateField(Game game, string[] field)
        {
            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    game.Field[i, j] = field[i][j];
                }
            }
        }

        [Test]
        public void RowWin()
        {
            var game = new Game();
            var field = new[]
            {
                "XX.",
                "OO.",
                "..."
            };
            PopulateField(game, field);

            game.Move(2, 0);
            game.Move(1, 2);

            Assert.That(game.GameState, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void ColumnWin()
        {
            var game = new Game();
            var field = new[]
            {
                "XO.",
                "XO.",
                "..."
            };
            PopulateField(game, field);

            game.Move(0, 2);
            game.Move(2, 1);

            Assert.That(game.GameState, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void MainDiagonalWin()
        {
            var game = new Game();
            var field = new[]
            {
                "XO.",
                "OX.",
                "..."
            };
            PopulateField(game, field);

            game.Move(2, 2);

            Assert.That(game.GameState, Is.EqualTo(GameState.XWin));
        }

        [Test]
        public void AnitDiagonalWin()
        {
            var game = new Game();
            var field = new[]
            {
                "XXO",
                "XOX",
                "..."
            };
            PopulateField(game, field);

            game.Move(2, 2);
            game.Move(2, 0);

            Assert.That(game.GameState, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void FullFieldTie()
        {
            var game = new Game();
            var field = new[]
            {
                "XOX",
                "XOO",
                "OX."
            };
            PopulateField(game, field);

            game.Move(2, 2);

            Assert.That(game.GameState, Is.EqualTo(GameState.Tie));
        }

        [Test]
        public void FullFieldWin()
        {
            var game = new Game();
            var field = new[]
            {
                "XOX",
                "XXO",
                "OO."
            };
            PopulateField(game, field);

            game.Move(2, 2);

            Assert.That(game.GameState, Is.EqualTo(GameState.XWin));
        }

        [Test]
        public void MakeMoveInFilledCell()
        {
            var game = new Game();
            var field = new[]
            {
                "XXO",
                "OO.",
                "X.."
            };
            PopulateField(game, field);

            game.Move(1, 0);

            Assert.That(game.GameState, Is.EqualTo(GameState.InProgress));
            Assert.That(game.Field[1,0], Is.EqualTo('O'));
        }

        [Test]
        public void MakeMoveInFinishedGame()
        {
            var game = new Game();
            var field = new[]
            {
                "XX.",
                "OO.",
                "..."
            };
            PopulateField(game, field);

            game.Move(0, 2);
            game.Move(1, 2);

            Assert.That(game.GameState, Is.EqualTo(GameState.XWin));
            Assert.That(game.Field[1,2], Is.EqualTo('.'));
        }
    }
}