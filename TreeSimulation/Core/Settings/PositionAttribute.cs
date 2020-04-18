using System;

namespace TreeSimulation.Core.Settings
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class PositionAttribute : Attribute
    {
        public PositionAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}