using System;

namespace TreeSimulation.Core.Settings
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : Attribute
    {
        public RangeAttribute(double lower, double upper)
        {
            Lower = lower;
            Step = 1;
            Upper = upper;
        }

        public RangeAttribute(double lower, double step, double upper)
        {
            Lower = lower;
            Step = step;
            Upper = upper;
        }

        public double Lower { get; }
        public double Step { get; }
        public double Upper { get; }
    }
}