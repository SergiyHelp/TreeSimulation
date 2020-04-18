using System;

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