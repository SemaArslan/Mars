using System;
using System.Collections.Generic;
using System.Text;

namespace MarsHepsiburada
{
    public class Plateau
    {
        private Coordinate coordinate;

        public Plateau(int x, int y)
        {
            this.Coordinate = new Coordinate(x, y);
        }

        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
    }
}
