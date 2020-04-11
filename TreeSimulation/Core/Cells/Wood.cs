using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace TreeSimulation.Core.Cells
{
    public class Wood : Cell
    {
        public Wood(Tree owner, Gene active) : base(owner, active)
        {
        }
        
        public override View GetView()
        {
            return new View(Position, Owner.WoodColor);
        }
    }
}
