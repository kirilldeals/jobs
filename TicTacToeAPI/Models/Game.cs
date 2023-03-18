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
        [JsonIgnore]
        public char[,] Field { get; }
        public Player CurrentPlayer { get; private set; }
        public GameState State { get; private set; }

        [JsonPropertyName("Field")]
        public string[] JsonField {
            get
            {
                var jsonArr = new string[3];
                for (int i = 0; i < Field.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < Field.GetLength(1); j++)
                    {
                        row += Field[i, j];
                    }
                    jsonArr[i] = row;
                }
                return jsonArr;
            }
        }

        public Game()
        {
            Id = Guid.NewGuid();
            Field = new char[3,3];
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    Field[i,j] = '.';
                }
            }
            CurrentPlayer = Player.X;
            State = GameState.InProgress;
        }

        public void Move(int row, int col)
        {
            if (IsGameFinished() || !IsCellEmpty(row, col))
                return;
            Field[row,col] = CurrentPlayer == Player.X ? 'X' : 'O';
            UpdateGameState(row, col);
        }

        public bool IsGameFinished() => State != GameState.InProgress;
        public bool IsCellEmpty(int row, int col) => Field[row,col] == '.';
        public bool IsCellEmpty(int row, int col, out string player)
        {
            bool isCellEmpty = Field[row,col] == '.';
            player = isCellEmpty ? "None" : Field[row,col].ToString();
            return isCellEmpty;
        }


        private void UpdateGameState(int row, int col)
        {
            if (DidPlayerWin(row, col))
            {
                return;
            }
            else if (!Field.Cast<char>().Any(c => c == '.'))
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
            // Row
            if (Enumerable.Range(0, Field.GetLength(1)).Select(x => Field[row, x]).Distinct().Count() == 1)
            {
                return FinishGame();
            }
            // Column
            if (Enumerable.Range(0, Field.GetLength(0)).Select(x => Field[x, col]).Distinct().Count() == 1)
            {
                return FinishGame();
            }
            // Main diagonal
            if (row == col)
            {
                HashSet<char> chars = new HashSet<char>();
                for (int i = 0; i < Field.GetLength(0); i++)
                {
                    chars.Add(Field[i,i]);
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
                for (int i = 0; i < Field.GetLength(0); i++)
                {
                    chars.Add(Field[i,2 - i]);
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
