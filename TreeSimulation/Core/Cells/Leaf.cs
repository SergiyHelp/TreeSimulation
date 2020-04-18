using Windows.UI;

namespace TreeSimulation.Core.Cells
{
    public class Leaf : Cell
    {
        public Leaf(Tree parent, Gene active) : base(parent, active)
        {
        }

        public override void EnergyStage()
        {
            base.EnergyStage();
            CommonEnergy += LightEnergy;
        }

        public override View View => new View(Position, Colors.Red);
    }
}