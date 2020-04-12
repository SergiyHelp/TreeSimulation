using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace TreeSimulation.Core
{
    public class Landscape
    {
        private readonly int[] _heels;

        public Landscape(int width, int seed, int lowerBound, int upperBound)
        {
            Width = width;
            _heels = new int[width];
            Random rn = new Random(seed);


            var points = new List<double>();
            points.Add(0);
            points.Add(0);

            for (double factor = width; points.Count < width; factor *= 0.5)
                for (int i = points.Count - 1; i > 0; i--)
                    points.Insert(i,  (points[i] + points[i - 1]) / 2 + (rn.NextDouble() - 0.5) * factor);
            
            int dH = upperBound - lowerBound;

            double min = points.Min();
            double max = points.Max();
            double dP = max - min;


            for (int i = 0; i < width; i++)            
                _heels[i] = (int)(((points[i] - min) / dP * dH) + lowerBound);       
        }    

        public int Width { get; }
        public int this[int x]
        {
            get => _heels[x];
        }

        public bool IsFreeAt(Position position)
        {
            return _heels[position.X] <= position.Y;
        }

    }
}
