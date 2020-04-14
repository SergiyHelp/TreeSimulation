using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSimulation
{
    public static class Extensions
    {
        public static double InRange(this Random random, TreeSimulation.Core.Range range)
        {
            return random.NextDouble() * range.Length + range.L;
        }
    }
}
