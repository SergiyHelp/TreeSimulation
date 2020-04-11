using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core.Orders
{
    public class Creator : Order
    {
        private readonly Position _position;
        private readonly Cell _cell;

        public Creator(Position position, Cell cell)
        {
            _position = position;
            _cell = cell;
        }

        public override int Priority => 3;

        public override void Execute(World world)
        {
            world.Cells.Add(_cell, _position);
        }
    }
}
