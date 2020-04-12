using System.Collections.Generic;
using System.Linq;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core
{
    public class CellCollection
    {
        private readonly Dictionary<Cell, Position> _cells;
        private readonly Landscape _landscape;

        public CellCollection(int width, int height, Landscape landscape) : base()
        {
            Width = width;
            Height = height;
            _landscape = landscape;
            Map = new bool[width, height];
            _cells = new Dictionary<Cell, Position>();
        }

        public int Width
        {
            get;
        }
        public int Height
        {
            get;
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
        public bool IsInside(Position pos)
        {
            pos = Normalize(pos);
            return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
        }
        public Position GetPosition(Cell cell)
        {
            if (_cells.ContainsKey(cell))
                return _cells[cell];
            else
                return new Position(0, 0);
        }
        public Position Normalize(Position pos)
        {
            return new Position(pos.X < 0 ? pos.X + Width : pos.X % Width, pos.Y);
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
        public bool IsFreeAt(Position position)
        {
            position = Normalize(position);
            return IsInside(position) && !Map[position.X, position.Y] && _landscape.IsFreeAt(position);
        }
        public void Add(Cell cell, Position position)
        {
            position = Normalize(position);
            _cells.Add(cell, position);
            Map[position.X, position.Y] = true;
        }
    }
}