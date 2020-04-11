namespace TreeSimulation.Core.Orders
{
    public class Seeder : Order
    {
        private readonly Seed _seed;

        public Seeder(Seed seed)
        {
            _seed = seed;
        }

        public override int Priority => 7;

        public override void Execute(World world)
        {
            world.Seeds.Add(_seed);
        }
    }
}
