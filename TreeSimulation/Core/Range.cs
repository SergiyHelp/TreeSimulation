using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSimulation.Core
{
    public readonly struct Range
    {
        public Range(double l, double u)
        {
            L = Math.Min(l, u);
            U = Math.Max(l, u);
        }

        public double L { get; }
        public double U { get; }
        public double Length => U - L;

        public static Range Default => new Range(0, 100);

        public override bool Equals(object obj)
        {
            return obj is Range range &&
                   L == range.L &&
                   U == range.U;
        }

        public override int GetHashCode()
        {
            int hashCode = -1001547245;
            hashCode = hashCode * -1521134295 + L.GetHashCode();
            hashCode = hashCode * -1521134295 + U.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"Range({L}, {U})";
        }
    }

    public readonly struct IntRange
    {
        public IntRange(int l, int u)
        {
            L = Math.Min(l, u);
            U = Math.Max(l, u);
        }

        public int L { get; }
        public int U { get; }
        public int Length => U - L;


        public override bool Equals(object obj)
        {
            return obj is Range range &&
                   L == range.L &&
                   U == range.U;
        }

        public override int GetHashCode()
        {
            int hashCode = -1001547245;
            hashCode = hashCode * -1521134295 + L.GetHashCode();
            hashCode = hashCode * -1521134295 + U.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"Range({L}, {U})";
        }
    }

}
