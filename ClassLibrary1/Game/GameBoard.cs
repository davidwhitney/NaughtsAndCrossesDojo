using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExesAndOhhs.Game
{
    public class GameBoard
    {
        private string[,] _state;

        public List<PlayerChoice> History { get; private set; }
        public bool GameEnded { get; set; }
        public char Winner { get; set; }

        public GameBoard()
        {
            _state = new string[3,3];
            History = new List<PlayerChoice>();
        }

        public GameBoard(string state) : this()
        {
            BoardFromState(state);
        }

        public bool TakeTurn(char naughtOrCross, int x, int y)
        {
            if (!CanMove(naughtOrCross, x, y))
            {
                return false;
            }

            _state[y, x] = naughtOrCross.ToString();

            History.Add(new PlayerChoice {NaughtOrCross = naughtOrCross, X = x, Y = y});

            var isThereAwinner = FindWinner();
            GameEnded = isThereAwinner != " ";
            Winner = isThereAwinner[0];

            CheckForGameEnd();

            return true;
        }

        private void CheckForGameEnd()
        {
            var spacesLeft = true;
            foreach (var item in AllPlaces.Where(item => item == "-" || item == null))
            {
                spacesLeft = false;
            }

            if (!spacesLeft)
            {
                GameEnded = true;
            }
        }

        public bool CanMove(char naughtOrCross, int x, int y)
        {
            if ((_state[y, x] == "-")
                && (History.LastOrDefault() == null || History.LastOrDefault().NaughtOrCross != naughtOrCross))
            {
                return true;
            }

            if (_state[y, x] == null 
                && (History.LastOrDefault() == null || History.LastOrDefault().NaughtOrCross != naughtOrCross))
            {
                return true;
            }

            return false;
        }

        private string FindWinner()
        {
            foreach (var row in Rows)
            {
                if (row.All(c => c == "x") || row.All(c => c == "o"))
                {
                    return row.First();
                }
            }

            foreach (var row in Columns)
            {
                if (row.All(c => c == "x") || row.All(c => c == "o"))
                {
                    return row.First();
                }
            }

            if ((_state[0, 0] == "x"
                 && _state[1, 1] == "x"
                 && _state[2, 2] == "x")
                ||
                (_state[0, 0] == "o"
                 && _state[1, 1] == "o"
                 && _state[2, 2] == "o"))
            {
                return _state[0, 0];
            }

            if ((_state[0, 2] == "x"
                 && _state[1, 1] == "x"
                 && _state[2, 0] == "x")
                ||
                (_state[0, 2] == "o"
                 && _state[1, 1] == "o"
                 && _state[2, 0] == "o"))
            {
                return _state[0, 0];
            }

            return " ";
        }

        private IEnumerable<List<string>> Rows
        {
            get
            {
                for (var yy = 0; yy < _state.GetLength(0); yy++)
                {
                    var row = new List<string>();
                    for (var xx = 0; xx < _state.GetLength(1); xx++)
                    {
                        var val = _state[yy, xx] ?? "-";
                        row.Add(val);
                    }

                    yield return row;
                }
            }
        }

        private IEnumerable<List<string>> Columns
        {
            get
            {
                for (var xx = 0; xx < _state.GetLength(1); xx++)
                {
                    var row = new List<string>();
                    for (var yy = 0; yy < _state.GetLength(0); yy++)
                    {
                        var val = _state[yy, xx] ?? "-";
                        row.Add(val);
                    }

                    yield return row;
                }
            }
        }

        private IEnumerable<string> AllPlaces
        {
            get { return Rows.SelectMany(row => row); }
        } 

        private void BoardFromState(string state)
        {
            _state = BoardBuilder.BuildBoardFrom(state);
        }

        public override string ToString()
        {
            return new Renderer(_state).Render();
        }
    }
}