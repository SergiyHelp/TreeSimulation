using System;
using System.Collections.Generic;
using System.Linq;
using TreeSimulation.Core.Settings;

namespace TreeSimulation.Core
{
    public class World
    {
        private int[,] _lightField;

        public World(int seed, int width, int height, int startPopulation, bool isClosed, Landscape landscape, WorldSettings settings)
        {
            Settings = settings;
            GenerationSeed = seed;
            IsClosed = isClosed;
            Width = width;
            Height = height;
            Random = new Random(seed);
            _lightField = new int[width, height];
            Landscape = landscape;
            Cells = new CellCollection(this);
            Seeds = CreateStartSeeds(startPopulation);
            UpdateView();
        }

        public IEnumerable<View> View
        {
            get;
            private set;
        }

        public bool IsClosed
        {
            get;
        }

        public WorldSettings Settings
        {
            get;
        }

        public CellCollection Cells
        {
            get;
        }
        public Landscape Landscape 
        {
            get;
        }
        public int GenerationSeed
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
            get;
        }

        public int Width
        {
            get;
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
            order.Execute(this);

            Seeds = Seeds.Where(c => IsFreeAt(c.Position)).ToList();

            var landedSeeds = Seeds.Where(x => IsOnGround(x)).ToList();
            landedSeeds.ForEach(x => Cells.Add(x.Plant(), x.Position));
            Seeds = Seeds.Except(landedSeeds).ToList();
            Seeds.ForEach(x => x.Fall());

            Seeds = Seeds.Where(c => IsFreeAt(c.Position)).ToList();

            UpdateView();
            Day++;
        }

        public bool IsFreeAt(Position position)
        {
            position = position.Normalized(this);
            return IsPointInside(position) && !Cells.HasCellAt(position) && !Landscape.HasLandAt(position);
        }
        public bool IsPointInside(Position pos)
        {
            return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
        }
        
        private List<Seed> CreateStartSeeds(int count)
        {
            var res = new List<Seed>();
            for (int i = 1; i < count + 1; i++)
            {
                int x = Width / (count + 1) * i;
                int y = Height - 5;

                res.Add(new Seed(new Position(x, y), new Tree(this)));
            }
            return res;
        }

        public double GetEnergy(Position pos)
        {
            double relativeHeight = pos.Y / (double)Height;
            double energy = relativeHeight * Settings.Light.Length + Settings.Light.L;

            energy *= Math.Pow(Settings.CellsTransparency, _lightField[pos.X, pos.Y]);

            return energy;
        }

        private bool IsOnGround(Seed seed)
        {
            return seed.Position.Y == Landscape[seed.Position.X];
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
            View = Cells.Objects.Select(x => x.GetView()).Concat(Seeds.Select(x => x.GetView())).ToList();
        }
    }
}