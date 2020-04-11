using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSimulation.Core.Orders
{
    public abstract class Order
    {
        public abstract int Priority { get; }
        public abstract void Execute(World world);
    }
}
