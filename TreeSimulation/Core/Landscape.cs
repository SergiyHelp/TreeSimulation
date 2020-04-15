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

        public Landscape(int width, int seed, Range range)
        {
            Width = width;
            _points = new int[width];
            Random rn = new Random(seed);

            double shift1 = rn.NextDouble() * 10;
            double shift2 = rn.NextDouble() * 5;
            double shift3 = rn.NextDouble() * 4;
            double shift4 = rn.NextDouble() * 24;
            double shift5 = rn.NextDouble() * 24;

            double factor1 = rn.NextDouble() * 256;
            double factor2 = rn.NextDouble() * 64;
            double factor3 = rn.NextDouble() * 16;
            double factor4 = rn.NextDouble() * 4;
            double factor5 = rn.NextDouble() * 1;

            double func(double x) => 
                Sin(x * PI * 2 *  2 + shift1) * factor1 + 
                Sin(x * PI * 2 *  5 + shift2) * factor2 + 
                Sin(x * PI * 2 * 13 + shift3) * factor3 +
                Sin(x * PI * 2 * 37 + shift4) * factor4 +
                Sin(x * PI * 2 * 61 + shift5) * factor5;

            double[] points = new double[width];

            for (int i = 0; i < width; i++)
                points[i] = func(i / (double)(width + 1));

            double min = points.Min();
            double max = points.Max();
            double diff = max - min;

            _points = points.Select(x => (int)((x - min) / diff * range.Length + range.L)).ToArray();

        }

        public int Width { get; }
        public int this[int x]
        {
            get => _points[x];
        }

        public bool HasLandAt(Position position)
        {
            return _points[position.X] <= position.Y;
        }

    }
}
