using System;
using System.Collections.Generic;
using System.Linq;
using TreeSimulation.Core.Settings;

namespace TreeSimulation.Core
{
    public class World
    {
        private int[,] _lightField;

        public World(WorldSettings settings)
        {
            Settings = settings;

            GenerationSeed = Int32.TryParse(Settings.Seed, out int res) ? res : (int)DateTime.Now.Ticks;

            IsClosed = settings.IsClosed;
            Width = (int)settings.Width;
            Height = (int)settings.Height;
            Random = new Random(GenerationSeed);
            _lightField = new int[Width, Height];
            Landscape = new Landscape(this);
            Cells = new CellCollection(this);
            Seeds = new SeedCollection(this);
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

        public SeedCollection Seeds
        {
            get;
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

            Seeds.Filter();
            Seeds.PlantLanded();
            Seeds.Fall();
            Seeds.Filter();

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

        public double GetEnergy(Position pos)
        {
            double relativeHeight = pos.Y / (double)Height;
            double energy = relativeHeight * Settings.Light.Length + Settings.Light.L;
            energy += Math.Cos(Day / Settings.YearCycle) * Settings.Amplitude;
            energy = energy > Settings.Light.L ? energy : Settings.Light.L;
            energy *= Math.Pow(Settings.CellsTransparency, _lightField[pos.X, pos.Y]);

            return energy;
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
            View = Cells.Views.Concat(Seeds.Views).ToList();
        }
    }
}