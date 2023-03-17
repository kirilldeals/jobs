using TicTacToeAPI.Models;
using NUnit.Framework;

namespace TicTacToeTests
{
    [TestFixture]
    public class GameModelTests
    {
        [Test]
        public void RowWin()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'X', '.' };
            game.Field[1] = new char[] { 'O', 'O', '.' };
            game.Move(2, 0);
            game.Move(1, 2);

            Assert.That(game.State, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void ColumnWin()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'O', '.' };
            game.Field[1] = new char[] { 'X', 'O', '.' };
            game.Move(0, 2);
            game.Move(2, 1);

            Assert.That(game.State, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void MainDiagonalWin()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'O', '.' };
            game.Field[1] = new char[] { 'O', 'X', '.' };
            game.Move(2, 2);

            Assert.That(game.State, Is.EqualTo(GameState.XWin));
        }

        [Test]
        public void AnitDiagonalWin()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'X', 'O' };
            game.Field[1] = new char[] { 'X', 'O', 'X' };
            game.Move(2, 2);
            game.Move(2, 0);

            Assert.That(game.State, Is.EqualTo(GameState.OWin));
        }

        [Test]
        public void FullFieldTie() 
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'O', 'X' };
            game.Field[1] = new char[] { 'X', 'O', 'O' };
            game.Field[2] = new char[] { 'O', 'X', '.' };
            game.Move(2, 2);

            Assert.That(game.State, Is.EqualTo(GameState.Tie));
        }

        [Test]
        public void FullFieldWin()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'O', 'X' };
            game.Field[1] = new char[] { 'X', 'X', 'O' };
            game.Field[2] = new char[] { 'O', 'O', '.' };
            game.Move(2, 2);

            Assert.That(game.State, Is.EqualTo(GameState.XWin));
        }

        [Test]
        public void MakeMoveInFilledCell()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'X', 'O' };
            game.Field[1] = new char[] { 'O', 'O', '.' };
            game.Field[2] = new char[] { 'X', '.', '.' };
            game.Move(1, 0);

            Assert.That(game.State, Is.EqualTo(GameState.InProgress));
            Assert.That(game.Field[1][0], Is.EqualTo('O'));
        }

        [Test]
        public void MakeMoveInFinishedGame()
        {
            var game = new Game();
            game.Field[0] = new char[] { 'X', 'X', '.' };
            game.Field[1] = new char[] { 'O', 'O', '.' };
            game.Move(0, 2);
            game.Move(1, 2);

            Assert.That(game.State, Is.EqualTo(GameState.XWin));
            Assert.That(game.Field[1][2], Is.EqualTo('.'));
        }
    }
}