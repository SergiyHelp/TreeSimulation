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