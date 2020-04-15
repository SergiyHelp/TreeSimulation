using System;
using System.Collections.Generic;
using System.Linq;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core
{
    public class CellCollection
    {
        private readonly Dictionary<Cell, Position> _cells;
        private readonly World _world;

        public CellCollection(World place) : base()
        {
            _world = place;
            Map = new bool[Width, Height];
            _cells = new Dictionary<Cell, Position>();
        }

        public int Width
        {
            get;
        }
        public int Height
        {
            get => _world.Height;
        }
        public bool[,] Map 
        { 
            get;
        }
        public List<Cell> Objects
        {
            get => _cells.Keys.ToList();
        }


        public bool Remove(Cell cell)
        {
            if (_cells.ContainsKey(cell))
            {
                var pos = _cells[cell];
                _cells.Remove(cell);
                Map[pos.X, pos.Y] = false;
                return true;
            }
            return false;
        }
        public Position GetPosition(Cell cell)
        {
            if (_cells.ContainsKey(cell))
                return _cells[cell];
            else
                return new Position(0, 0);
        }
       
        public bool Replace(Cell from, Cell to)
        {
            if (_cells.ContainsKey(from))
            {
                var pos = _cells[from];
                _cells.Remove(from);
                _cells.Add(to, pos);
                return true;
            }
            return false;
        }
        public bool HasCellAt(Position position)
        {
            
            return Map[position.X, position.Y];
        }
        public void Add(Cell cell, Position position)
        {
            position = position.Normalized(_world);
            _cells.Add(cell, position);
            Map[position.X, position.Y] = true;
        }
    }
}