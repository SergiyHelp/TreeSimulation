using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core.Cells
{
    public class Bud : Cell
    {
        public Bud(Tree owner, Gene active) : base(owner, active)
        {
        }

        public double Energy 
        { 
            get; 
            private set; 
        }
       
        public override void EnergyStage()
        {
            base.EnergyStage();
            var light = LightEnergy;
            Energy += light;
            CommonEnergy += light * Settings.BudProfit;
        }

        public override View GetView()
        {
            return new View(Position, Colors.Blue);
        }

        protected override void NormalLife(ICollection<Order> orders)
        {            
            if (Energy > Settings.BudMass)
            {
                int x = Position.X;
                int y = Position.Y;

                orders.Add(new Replacer(this, new Wood(Owner, ActiveGene)));

                TryCreate(orders, ActiveGene[0], new Position(x, y + 1));
                TryCreate(orders, ActiveGene[1], new Position(x, y - 1));
                TryCreate(orders, ActiveGene[2], new Position(x + 1, y));
                TryCreate(orders, ActiveGene[3], new Position(x - 1, y));
            }
        }

        private void TryCreate(ICollection<Order> orders, int genePart, Position position)
        {
            if (Genome.IsValidGene(genePart) && Owner.Environment.Cells.IsFreeAt(position))
                orders.Add( new Creator(position, Create(Gene.GetCellType(genePart), Owner, Owner.Genome[genePart % Genome.CommonSize])));
        }
    }
}
