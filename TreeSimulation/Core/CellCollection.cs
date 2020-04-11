using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSimulation.Core.Cells;

namespace TreeSimulation.Core
{
    public class CellCollection 
    {
        private readonly Dictionary<Cell, Position> _cells;
        private readonly bool[,] _map;
        public CellCollection(int width, int height) : base()
        {
            Width = width;
            Height = height;
            _map = new bool[width, height];
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
        public List<Cell> Objects
        {
            get => _cells.Keys.ToList();
        }
        public bool[,] Map
        {
            get => _map;
        }
        
        public void Add(Cell cell, Position position)
        {
            position = Normalize(position);
            _cells.Add(cell, position);
            _map[position.X, position.Y] = true;
        }
        public bool Remove(Cell cell)
        {
            if (_cells.ContainsKey(cell))
            {
                var pos = _cells[cell];
                _cells.Remove(cell);
                _map[pos.X, pos.Y] = false;
                return true;
            }
            return false;
        }
        public bool Replace(Cell from, Cell to)
        {
            if(_cells.ContainsKey(from))
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
            return IsInside(position) && !_map[position.X, position.Y];
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
    }
}
