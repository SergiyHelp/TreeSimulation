using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

using static System.Math;

namespace TreeSimulation.Core
{
    public class Landscape
    {
        private readonly int[] _points;

        public Landscape(int width, int seed, int lowerBound, int upperBound)
        {
            Width = width;
            _points = new int[width];
            Random rn = new Random(seed);

            double shift1 = rn.NextDouble() * 10;
            double shift2 = rn.NextDouble() * 5;
            double shift3 = rn.NextDouble() * 4;
            
            double factor1 = rn.NextDouble() * 16;
            double factor2 = rn.NextDouble() * 8;
            double factor3 = rn.NextDouble() * 3;

            double func(double x) => Sin(x * PI * 2 + shift1) * factor1 + Sin(x * PI * 5 + shift2) * factor2 + Sin(x * PI * 23 + shift3) * factor3;

            double[] points = new double[width];

            for (int i = 0; i < width; i++)
                points[i] = func(i / (double)width);

            double min = points.Min();
            double max = points.Max();
            double range = max - min;

            _points = points.Select(x => (int)((x - min) * range)).ToArray();

            _points = points.Select(x => 10).ToArray();
        }

        public int Width { get; }
        public int this[int x]
        {
            get => _points[x];
        }

        public bool IsFreeAt(Position position)
        {
            return _points[position.X] <= position.Y;
        }

    }
}
