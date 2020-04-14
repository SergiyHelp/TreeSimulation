using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSimulation.Core.Settings
{
    public class RangeAttribute : Attribute
    {
        public RangeAttribute(double lower, double upper)
        {
            Lower = lower;
            Upper = upper;
        }

        public double Lower { get; }
        public double Upper { get; }
    }
}
