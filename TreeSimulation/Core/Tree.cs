using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace TreeSimulation.Core
{
    public class Tree
    {       
        public Tree(World environment)
        {
            var rand = environment.Random;
            Genome = new Genome(rand);
            Environment = environment;
            WoodColor = Color.FromArgb(255, (byte)rand.Next(100), (byte)rand.Next(120, 255), (byte)rand.Next(100));
            DeathDay = Environment.Day + rand.Next(80, 120);
            Energy = Environment.Settings.InitialEnergy;
            
        }
        public Tree(Tree parent) : this(parent.Environment)
        {
            Energy = parent.Energy / parent.SporesProduced;
            Genome = new Genome(parent.Genome);
        }

        public int SporesProduced { get; set; }
        public World Environment { get; private set; }
        public Color WoodColor { get; private set; }
        public Genome Genome { get; private set; }
        public int DeathDay { get; private set; }
        public double Energy { get; set; }

        public override string ToString()
        {
            return $"Tree ({Energy})";
        }
    }
}
