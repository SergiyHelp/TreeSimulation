using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using TreeSimulation.Core.Cells;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core
{
    public class World
    {
        private int[,] _lightField;
        private Stopwatch _watch;

        public World(int seed, int width, int height, int startPopulation, WorldSettings settings)
        {
            _watch = new Stopwatch();
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

            Cell.BudTicks = 0L;
            Cell.FruitTicks = 0L;
            Cell.WoodTicks = 0L;
            Cell.LeafTicks = 0L;

            _watch.Reset();
            Debug.WriteLine("Day {0}", Day);

            _watch.Start();
            UpdateLightField();
            GetTime("Field");

            foreach (var item in Cells.Objects)
                item.EnergyStage();
            
            GetTime("Energy");

            List<Order> orders = new List<Order>();
            foreach (var item in Cells.Objects)
                item.GenerationStage(orders);

            orders = orders.OrderBy(x => x.Priority).ToList();


            Debug.WriteLine("{0, 10} {1}", "Bud", Cell.BudTicks);
            Debug.WriteLine("{0, 10} {1}", "Leaf", Cell.LeafTicks);
            Debug.WriteLine("{0, 10} {1}", "Wood", Cell.WoodTicks);
            Debug.WriteLine("{0, 10} {1}", "Fruit", Cell.FruitTicks);


            GetTime("Orders");
            
            foreach (var item in orders)
                item.Execute(this);
            GetTime("Execute");

            Seeds = Seeds.Where(c => Cells.IsFreeAt(c.Position)).ToList();

            var landedSeeds = Seeds.Where(x => IsOnGround(x)).ToList();
            landedSeeds.ForEach(x => Cells.Add(x.Plant(), x.Position));
            Seeds = Seeds.Except(landedSeeds).ToList();
            Seeds.ForEach(x => x.Fall());

            Seeds = Seeds.Where(c => Cells.IsFreeAt(c.Position)).ToList();

            GetTime("Seeds");
            
            _watch.Stop();
            Debug.WriteLine("__________________________________________________________");
            
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

        private void GetTime(string label)
        {
            _watch.Stop();
            Debug.WriteLine("{0, 12} {1}", label, _watch.Elapsed);
            _watch.Reset();
            _watch.Start();
        }
    }
}
