using System.Collections.Generic;
using System.Linq;

namespace TreeSimulation.Core
{
    public class SeedCollection
    {
        private List<Seed> _seeds;
        private readonly World _world;

        public SeedCollection(World world)
        {
            _world = world;
            _seeds = new List<Seed>();

            int count = (int)world.Settings.Population;
            for (int i = 1; i < count + 1; i++)
            {
                int x = world.Width / (count + 1) * i;
                int y = world.Height - 5;

                _seeds.Add(new Seed(new Position(x, y), new Tree(world)));
            }
        }

        public IEnumerable<View> Views
        {
            get => _seeds.Select(x => x.View);
        }

        public void Add(Seed seed)
        {
            _seeds.Add(seed);
        }

        public void Filter()
        {
            _seeds = _seeds.Where(c => _world.IsFreeAt(c.Position)).ToList();
        }

        public void PlantLanded()
        {
            var landedSeeds = _seeds.Where(x => _world.Landscape.IsPointOnGround(x.Position)).ToList();

            foreach (var item in landedSeeds)
                _world.Cells.Add(item.Plant(), item.Position);

            _seeds = _seeds.Except(landedSeeds).ToList();
        }

        public void Fall()
        {
            foreach (var item in _seeds)
                item.Fall();
        }
    }
}