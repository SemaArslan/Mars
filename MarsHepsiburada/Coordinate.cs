using System;
using System.Collections.Generic;
using System.Text;

namespace MarsHepsiburada
{
    public class Coordinate
    {
        private int x;
        private int y;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

    }
}
