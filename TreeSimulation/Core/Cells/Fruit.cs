using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core.Cells
{
    public class Fruit : Cell
    {
        public Fruit(Tree parent, Gene active) : base(parent, active)
        {
        }

        public override View GetView()
        {
            return new View(Position, Colors.Violet);
        }

        protected override void Death(Order order)
        {
            base.Death(order);
            if (CommonEnergy > 0)
                order.AddSeeding(Position, Owner);
        }
    }
}
