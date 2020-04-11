using System;

namespace TreeSimulation.Core
{
    public class Genome
    {
        private readonly Gene[] _genes;
        private readonly Random _random;

        public Genome(Genome original)
        {
            _random = original._random;
            _genes = new Gene[CommonSize];

            for (int i = 0; i < _genes.Length; i++)
            {
                _genes[i] = original._genes[i];
            }
        }

        public Genome(Random random)
        {
            _random = random;
            _genes = new Gene[CommonSize];

            for (int i = 0; i < _genes.Length; i++)
            {
                _genes[i] = Gene.CreateRandom(_random);
            }
        }

        public static int CommonSize => 16;
        public static double MutationProbability => 0.25;

        public Gene this[int index]
        {
            get => _genes[index];
        }

        public Gene UseActivator(int dna)
        {
            return _genes[dna % CommonSize];
        }

        public static bool IsValidGene(int v)
        {
            return v < CommonSize * 3;
        }

        public Genome Copy()
        {
            var newGenome = new Genome(this);

            if (_random.NextDouble() < MutationProbability)
                newGenome._genes[_random.Next(newGenome._genes.Length)] = Gene.CreateRandom(_random);

            return newGenome;
        }
    }
}