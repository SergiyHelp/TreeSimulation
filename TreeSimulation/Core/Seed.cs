using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core
{
    public class Seed : IVisible
    {
        private readonly Tree _parent;

        public Seed(Position position, Tree parent)
        {
            Position = position;
            parent.SporesProduced++;
            _parent = parent;
        }

        public Position Position { get; set; }
      
        public void Fall()
        {
            Position = new Position(Position.X, Position.Y - 1);
        }

        public View GetView()
        {
            return new View(Position, Colors.Black);
        }

        public Bud Plant()
        {
            var tree = new Tree(_parent);
            return new Bud(tree, tree.Genome[0]);
        }       
    }
}
