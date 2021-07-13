using System;
using System.Text.RegularExpressions;

namespace MarsHepsiburada
{
    public class Rover
    {
        private char direction;
        private Coordinate coordinate;
        private Plateau plateau;

        public char Direction { get => direction; set => direction = value; }
        public Plateau Plateau { get => plateau; set => plateau = value; }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }

        public Rover() {
            this.Plateau = new Plateau(0,0);
            this.Direction = 'N';
            this.Coordinate = new Coordinate(0,0);
        }

        public Rover(Plateau plateau, Coordinate coordinate, char direction)
        {
            this.Plateau = plateau;
            this.Direction = direction;
            this.Coordinate = coordinate;

            CheckRoverSettings();
            CheckPlateauBoundry(coordinate);
        }

        public void Execute(string[] commandSet)
        {
            if (commandSet.Length != 1)
            {
                throw new Exception("Invalid rover command expection. Expected format LMLMLMLMM etc.");
            }

            if (!Regex.IsMatch(commandSet[0], @"\b[LRM]\w*\b"))
            {
                throw new Exception("Invalid rover command expection. Expected command codes L R or M");
            }

            foreach (char item in commandSet[0])
            {
                if (item == 'L' || item == 'R')
                    Rotate(item);
                else if (item == 'M')
                    Move();
                else
                    throw new Exception("HOOO");
            }

        }

        public void Move()
        {   
            Coordinate newCoordinate = this.Coordinate;

            switch (this.Direction)
            {
                case 'N': { newCoordinate.Y += 1; } break;
                case 'S': { newCoordinate.Y -= 1; } break;
                case 'E': { newCoordinate.X += 1; } break;
                case 'W': { newCoordinate.X -= 1; } break;
                default: break;
            }

            CheckPlateauBoundry(newCoordinate);
            this.Coordinate = newCoordinate;

        }

        public void Rotate(char to)
        {

            if (!(to == 'R' || to == 'L')) return;

            const string DIRECTIONS = "NESW";

            int index = DIRECTIONS.IndexOf(this.Direction);

            if (index < 0) return;

            if (to == 'R')
                index += 1;
            else
                index -= 1;

            if (index > 3)
                index = 0;

            if (index < 0)
                index = 3;

            this.Direction = DIRECTIONS[index];
        }

        public void CheckPlateauBoundry(Coordinate coordinate)
        {
            CheckRoverSettings();

            if (coordinate == null)
            {
                throw new Exception("Coordinate can't be empty");
            }

            if (coordinate.X > this.Plateau.Coordinate.X || coordinate.Y > this.Plateau.Coordinate.Y || coordinate.X < 0 || coordinate.Y < 0)
            {
                throw new Exception("The rover go out of bounds from plateau");
            }
            
        }

        private void CheckRoverSettings() {

            if (coordinate == null)
            {
                throw new Exception("The rover's coordinate can't be empty");
            }

            if (this.Plateau == null)
            {
                throw new Exception("The rover's plateau can't be empty");
            }

            if ("NESW".IndexOf(this.Direction) < 0)
            {
                throw new Exception("The rover's direction can only be N,E,S,W");
            }

        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Coordinate.X.ToString(), Coordinate.Y.ToString(), Direction.ToString());
        }
    }
}
