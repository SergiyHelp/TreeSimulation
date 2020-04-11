using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core.Orders
{
    public class Replacer : Order
    {
        private readonly Cell _old;
        private readonly Cell _new;

        public Replacer(Cell old, Cell @new)
        {
            _old = old;
            _new = @new;
        }

        public override int Priority => 1;

        public override void Execute(World world)
        {
            world.Cells.Replace(_old, _new);
        }
    }
}
