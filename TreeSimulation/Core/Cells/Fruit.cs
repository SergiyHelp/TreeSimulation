using Windows.UI;

namespace TreeSimulation.Core.Cells
{
    public class Fruit : Cell
    {
        public Fruit(Tree parent, Gene active) : base(parent, active)
        {
        }

        public override View View => new View(Position, Colors.Violet);

        protected override void Death(Order order)
        {
            base.Death(order);
            if (CommonEnergy > 0)
                order.AddSeeding(Position, Owner);
        }
    }
}