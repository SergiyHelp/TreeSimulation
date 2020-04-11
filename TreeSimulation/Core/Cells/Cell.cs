using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Diagnostics;
using TreeSimulation.Core.Orders;

namespace TreeSimulation.Core.Cells
{
    public abstract class Cell : IVisible
    {
        public static long BudTicks;
        public static long LeafTicks;
        public static long FruitTicks;
        public static long WoodTicks;

        public Cell(Tree owner, Gene active)
        {
            Owner = owner;
            ActiveGene = active;
        }
       
        public Tree Owner
        {
            get;
        }
        public Gene ActiveGene
        {
            get;
        }
        public Position Position
        {
            get => Owner.Environment.Cells.GetPosition(this);
        }
        protected double LightEnergy
        {
            get => Owner.Environment.GetEnergy(Position);
        }
        protected double CommonEnergy
        {
            get => Owner.Energy;
            set => Owner.Energy = value;
        }
        protected WorldSettings Settings
        {
            get => Owner.Environment.Settings;
        }

        public static Cell Create(Type type, Tree tree, Gene active)
        {
            return Activator.CreateInstance(type, tree, active) as Cell;
        }
        
        public virtual void EnergyStage()
        {
            CommonEnergy -= Settings.EnergyCosts;
        }
        public void GenerationStage(Order order)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                if (CommonEnergy <= 0 || Owner.DeathDay <= Owner.Environment.Day)
                    Death(order);
                else
                    NormalLife(order);
            }
            finally
            {
                watch.Stop();
                if (this is Bud)
                    BudTicks += watch.ElapsedTicks;
                else if (this is Leaf)
                    LeafTicks += watch.ElapsedTicks;
                else if (this is Fruit)
                    FruitTicks += watch.ElapsedTicks;
                else if (this is Wood)
                    WoodTicks += watch.ElapsedTicks;
            }

        }
        protected virtual void NormalLife(Order order)
        {            
        }
        protected virtual void Death(Order order)
        {
            order.AddRemoving(this);
        }

        public abstract View GetView();

        public override string ToString()
        {
            return $"{GetType().Name} {Position}";
        }
    }
}
