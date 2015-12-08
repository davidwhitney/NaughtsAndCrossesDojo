using System;
using System.Linq;

namespace ExesAndOhhs.Game
{
    public class BoardBuilder
    {
        public static string[,] BuildBoardFrom(string value)
        {
            var state = new string[3, 3];
            var stateLines = value.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            stateLines = stateLines.Reverse().ToArray();

            for (var y = 0; y < stateLines.Length; y++)
            {
                var line = stateLines[y].Trim();
                for (var x = 0; x < line.Length; x++)
                {
                    var ch = line[x];
                    state[y, x] = ch.ToString();
                }
            }
            return state;
        }
    }
}