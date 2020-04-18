using Windows.UI;

namespace TreeSimulation.Core.Cells
{
    public class Bud : Cell
    {
        private int _term;
        private bool _ripe;

        public Bud(Tree owner, Gene active) : base(owner, active)
        {
            _term = ActiveGene[4] % Genome.CommonSize;
        }

        public double Energy
        {
            get;
            private set;
        }

        public override void EnergyStage()
        {
            base.EnergyStage();
            var light = LightEnergy * Settings.BudProfit;
            Energy += light;
            if (_term == 0)
            {
                _term = ActiveGene[4] % Genome.CommonSize;
                if (Energy < Settings.BudMass)
                {
                    CommonEnergy += Energy;
                    Energy = 0;
                }
                else
                {
                    _ripe = true;
                }
            }
            else
            {
                _term--;
            }
        }

        public override View View => new View(Position, Colors.Blue);

        protected override void NormalLife(Order order)
        {
            if (_ripe)
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