using System;
using System.Collections.Generic;
using System.Linq;
using TreeSimulation.Core.Cells;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core
{
    public class World
    {
        private int[,] _lightField;
    
        public World(int seed, int width, int height, int startPopulation, WorldSettings settings)
        {
            Settings = settings;
            Random = new Random(seed);
            _lightField = new int[width, height];
            Cells = new CellCollection(width, height);
            Seeds = CreateStartSeeds(startPopulation);
            UpdateView();
        }

        public IEnumerable<View> View
        {
            get;
            private set;
        }
        public WorldSettings Settings
        {
            get;
        }
        public CellCollection Cells
        {
            get;
        }
        public List<Seed> Seeds
        {
            get;
            set;
        }
        public Random Random
        {
            get;
        }
        public int Height
        {
            get => Cells.Height;
        }
        public int Width
        {
            get => Cells.Width;
        }
        public int Day
        {
            get;
            private set;
        }


        public void MakeStep()
        {
            UpdateLightField();
            foreach (var item in Cells.Objects)
                item.EnergyStage();

            Order order = new Order();
            foreach (var item in Cells.Objects)
                item.GenerationStage(order);            

            Seeds = Seeds.Where(c => Cells.IsFreeAt(c.Position)).ToList();

            var landedSeeds = Seeds.Where(x => IsOnGround(x)).ToList();
            landedSeeds.ForEach(x => Cells.Add(x.Plant(), x.Position));
            Seeds = Seeds.Except(landedSeeds).ToList();
            Seeds.ForEach(x => x.Fall());

            Seeds = Seeds.Where(c => Cells.IsFreeAt(c.Position)).ToList();

            
            UpdateView();
            Day++;
        }

        private List<Seed> CreateStartSeeds(int count)
        {
            var res = new List<Seed>();
            for (int i = 1; i < count + 1; i++)
                res.Add(new Seed(new Position(Width / (count + 1) * i, 0), new Tree(this)));

            return res;
        }
        public double GetEnergy(Position pos)
        {
            double relativeHeight = pos.Y / (double)Height;
            double energy = relativeHeight * (Settings.MaxEnergy - Settings.MinEnergy) + Settings.MinEnergy;

            energy *= Math.Pow(Settings.CellsTransparentsy, _lightField[pos.X, pos.Y]);

            return energy;
        }
        private bool IsOnGround(Seed seed)
        {
            return seed.Position.Y == 0;
        }
        private void UpdateLightField()
        {
            _lightField = new int[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                int shadow = 0;
                for (int y = Height - 1; y >= 0; y--)
                {
                    _lightField[x, y] = shadow;

                    if (Cells.Map[x, y])
                        shadow++;
                }
            }
        }
        private void UpdateView()
        {
            View = Cells.Objects.Cast<IVisible>().Concat(Seeds.Cast<IVisible>()).Select(x => x.GetView()).ToList();
        }
      
    }
}
