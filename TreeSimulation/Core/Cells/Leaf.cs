using Microsoft.Graphics.Canvas.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override View GetView()
        {
            return new View(Position, Colors.Red);
        }
    }
}
