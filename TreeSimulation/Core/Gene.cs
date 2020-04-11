using System;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core
{
    public class Gene
    {
        private readonly int[] _values;

        public Gene(params int[] vals)
        {
            _values = vals;
        }

        public int this[int i]
        {
            get => _values[i];
        }

        public static Type GetCellType(int i)
        {
            switch (i / Genome.CommonSize)
            {
                case 0:
                    return typeof(Bud);

                case 1:
                    return typeof(Fruit);

                case 2:
                    return typeof(Leaf);

                default:
                    throw new Exception("Using of invalid gene!");
            }
        }

        public static Gene CreateRandom(Random random)
        {
            return new Gene(random.Next(Genome.CommonSize * 5),
                            random.Next(Genome.CommonSize * 5),
                            random.Next(Genome.CommonSize * 5),
                            random.Next(Genome.CommonSize * 5));
        }
    }
}