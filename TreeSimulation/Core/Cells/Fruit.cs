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

        protected override void Death(ICollection<Order> orders)
        {
            base.Death(orders);
            if (CommonEnergy > 0)
                orders.Add(new Seeder(new Seed(Position, Owner)));
        }
    }
}
