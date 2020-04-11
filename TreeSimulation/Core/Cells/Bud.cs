using Windows.UI;

namespace TreeSimulation.Core.Cells
{
    public class Bud : Cell
    {
        public Bud(Tree owner, Gene active) : base(owner, active)
        {
        }

        public double Energy
        {
            get;
            private set;
        }

        public override void EnergyStage()
        {
            base.EnergyStage();
            var light = LightEnergy;
            Energy += light;
            CommonEnergy += light * Settings.BudProfit;
        }

        public override View GetView()
        {
            return new View(Position, Colors.Blue);
        }

        protected override void NormalLife(Order order)
        {
            if (Energy > Settings.BudMass)
            {
                int x = Position.X;
                int y = Position.Y;

                order.AddReplacing(this, typeof(Wood), Owner, ActiveGene);

                order.AddCreating(new Position(x, y + 1), Owner, ActiveGene[0]);
                order.AddCreating(new Position(x, y - 1), Owner, ActiveGene[1]);
                order.AddCreating(new Position(x + 1, y), Owner, ActiveGene[2]);
                order.AddCreating(new Position(x - 1, y), Owner, ActiveGene[3]);
            }
        }
    }
}