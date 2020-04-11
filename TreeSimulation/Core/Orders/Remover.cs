using System.Collections.Generic;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core.Orders
{
    public class Remover : Order
    {
        private readonly Cell _cell;

        public Remover(Cell cell)
        {
            _cell = cell;
        }

        public override int Priority => 5;

        public override void Execute(World world)
        {
            world.Cells.Remove(_cell);
        }
    }
}
