namespace TreeSimulation.Core.Cells
{
    public class Wood : Cell
    {
        public Wood(Tree owner, Gene active) : base(owner, active)
        {
        }

        public override View View => new View(Position, Owner.WoodColor);
    }
}