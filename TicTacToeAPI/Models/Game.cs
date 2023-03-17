using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace TicTacToeAPI.Models
{
    public class Game
    {
        public Guid Id { get; }
        public char[][] Field { get; }
        public Player CurrentPlayer { get; private set; }
        public GameState State { get; private set; }

        public Game()
        {
            Id = Guid.NewGuid();
            Field = new char[3][];
            for (int i = 0; i < Field.Length; i++)
            {
                Field[i] = new char[3];
                for (int j = 0; j < Field[i].Length; j++)
                {
                    Field[i][j] = '.';
                }
            }
            CurrentPlayer = Player.X;
            State = GameState.InProgress;
        }

        public void Move(int row, int col)
        {
            if (IsGameFinished() || !IsCellEmpty(row, col))
                return;
            Field[row][col] = CurrentPlayer == Player.X ? 'X' : 'O';
            UpdateGameState(row, col);
        }

        public bool IsGameFinished() => State != GameState.InProgress;
        public bool IsCellEmpty(int row, int col) => Field[row][col] == '.';
        public bool IsCellEmpty(int row, int col, out string player)
        {
            bool isCellEmpty = Field[row][col] == '.';
            player = isCellEmpty ? "None" : Field[row][col].ToString();
            return isCellEmpty;
        }


        private void UpdateGameState(int row, int col)
        {
            if (DidPlayerWin(row, col))
            {
                return;
            }
            else if (!Field.SelectMany(x => x).Any(c => c == '.'))
            {
                State = GameState.Tie;
                CurrentPlayer = Player.None;
            }
            else
            {
                CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
            }
        }

        private bool DidPlayerWin(int row, int col)
        {
            if (Field[row].Distinct().Count() == 1)
            {
                return FinishGame();
            }
            if (Field.Select(r => r[col]).Distinct().Count() == 1)
            {
                return FinishGame();
            }
            // Main diagonal
            if (row == col)
            {
                HashSet<char> chars = new HashSet<char>();
                for (int i = 0; i < Field.Length; i++)
                {
                    chars.Add(Field[i][i]);
                }
                if (chars.Count == 1)
                {
                    return FinishGame();
                }
            }
            // Anti diagonal
            if (row + col == 2)
            {
                HashSet<char> chars = new HashSet<char>();
                for (int i = 0; i < Field.Length; i++)
                {
                    chars.Add(Field[i][2-i]);
                }
                if (chars.Count == 1)
                {
                    return FinishGame();
                }
            }
            return false;
        }

        private bool FinishGame()
        {
            State = CurrentPlayer == Player.X ? GameState.XWin : GameState.OWin;
            CurrentPlayer = Player.None;
            return true;
        }
    }
}
