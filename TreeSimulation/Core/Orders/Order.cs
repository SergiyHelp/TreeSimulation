using System;

namespace TreeSimulation.Core.Orders
{
    public class Order
    {
        private event Action<World> OnCreate;

        private event Action<World> OnReplace;

        private event Action<World> OnRemove;

        private event Action<World> OnSeed;

        public void AddCreating(Position position, Tree owner, int dna)
        {
            OnCreate += (w) =>
            {
                if (Genome.IsValidGene(dna) && w.Cells.IsFreeAt(position))
                    w.Cells.Add(Cells.Cell.Create(Gene.GetCellType(dna), owner, owner.Genome.UseActivator(dna)), position);
            };
        }

        public void AddReplacing(Cells.Cell old, Type type, Tree owner, Gene active)
        {
            OnReplace += (w) => w.Cells.Replace(old, Cells.Cell.Create(type, owner, active));
        }

        public void AddRemoving(Cells.Cell cell)
        {
            OnReplace += (w) => w.Cells.Remove(cell);
        }

        public void AddSeeding(Position position, Tree parent)
        {
            OnCreate += (w) => w.Seeds.Add(new Seed(position, parent));
        }

        public void Execute(World world)
        {
            OnReplace?.Invoke(world);
            OnRemove?.Invoke(world);
            OnCreate?.Invoke(world);
            OnSeed?.Invoke(world);
        }
    }
}