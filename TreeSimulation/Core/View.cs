using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace TreeSimulation.Core
{
    public readonly struct View
    {
        public View(Position position, Color color)
        {
            X = position.X;
            Y = position.Y;
            Color = color;
        }

        public int X { get; }
        public int Y { get; }
        public Color Color { get; }
    }
}
