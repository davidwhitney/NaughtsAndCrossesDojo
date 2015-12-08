using System.Collections.Generic;
using System.Text;

namespace ExesAndOhhs.Game
{
    public class Renderer
    {
        private readonly string[,] _state;

        public Renderer(string[,] state)
        {
            _state = state;
        }

        public string Render()
        {
            var list = new List<string>();
            for (var yy = 0; yy < _state.GetLength(0); yy++)
            {
                var row = "";
                for (var xx = 0; xx < _state.GetLength(1); xx++)
                {
                    var val = _state[yy, xx] ?? "-";
                    row += val;
                }
                list.Add(row);
            }

            list.Reverse();

            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.AppendLine(item);
            }

            return sb.ToString().Trim();
        }
    }
}