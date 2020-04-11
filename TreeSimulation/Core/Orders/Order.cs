using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSimulation.Core;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core.Orders
{
    public class Order
    {
        event Action<World> Create;
        event Action<World> Replace;
        event Action<World> Remove;
        event Action<World> Seed;



        public void AddCreating(Position position, Tree owner, int dna)
        {
            Create += (w) =>
            {
                if (Genome.IsValidGene(dna) && w.Cells.IsFreeAt(position))
                    w.Cells.Add(Cells.Cell.Create(Gene.GetCellType(dna), owner, owner.Genome.UseActivator(dna)), position);
            };
        }
        public void AddReplacing(Cells.Cell old, Type type, Tree owner, Gene active )
        {
            Replace += (w) => w.Cells.Replace(old, Cells.Cell.Create(type, owner, active));
        }
        public void AddRemoving(Cells.Cell cell)
        {
            Replace += (w) => w.Cells.Remove(cell);
        }
        public void AddSeeding(Position position, Tree parent)
        {
            Create += (w) => w.Seeds.Add(new Seed(position, parent));
        }

        public void CreateAll(World world)
        {
            Create(world);
        }
        public void ReplaceAll(World world)
        {
            Replace(world);
        }
        public void RemoveAll(World world)
        {
            Remove(world);
        }
        public void SeedAll(World world)
        {
            Seed(world);
        }

    }
}
