using System;
using Xunit;
using MarsHepsiburada;

namespace MarsHepsiburadaUnitTest
{
    public class RoverUnitTest
    {
        [Fact]
        public void Success_Create_Instance()
        {
            Rover rover = new Rover();
            Assert.Equal('N', rover.Direction);
            Assert.Equal(0, rover.Plateau.Coordinate.X);
            Assert.Equal(0, rover.Plateau.Coordinate.Y);
            Assert.Equal(0, rover.Coordinate.X);
            Assert.Equal(0, rover.Coordinate.Y);
        }

        [Fact]
        public void Success_Create_Instance_With_Arguments()
        {
            Plateau plateau = new Plateau(3, 3);
            Coordinate coordinate = new Coordinate(2,1);
            char direction = 'E';

            Rover rover = new Rover(plateau, coordinate, direction);

            Assert.Equal('E', rover.Direction);
            Assert.Equal(3, rover.Plateau.Coordinate.X);
            Assert.Equal(3, rover.Plateau.Coordinate.Y);
            Assert.Equal(2, rover.Coordinate.X);
            Assert.Equal(1, rover.Coordinate.Y);
        }

        [Fact]
        public void Fail_Create_Instance_Without_Coordinate()
        {
            Plateau plateau = new Plateau(3, 3);
            Coordinate coordinate = null;
            char direction = 'E';

            var ex = Assert.Throws<Exception>(() => new Rover(plateau, coordinate, direction));
            Assert.Contains("The rover's coordinate can't be empty", ex.Message);

        }

        [Fact]
        public void Fail_Create_Instance_With_Invalid_Direction()
        {
            Plateau plateau = new Plateau(3, 3);
            Coordinate coordinate = new Coordinate(2, 1);
            char direction = 'X';

            var ex = Assert.Throws<Exception>(() => new Rover(plateau, coordinate, direction));
            Assert.Contains("The rover's direction can only be N,E,S,W", ex.Message);

        }

        [Fact]
        public void Fail_Create_Instance_Without_Platue()
        {
            Plateau plateau = null;
            Coordinate coordinate = new Coordinate(2, 1);
            char direction = 'E';
            
            var ex = Assert.Throws<Exception>(() => new Rover(plateau, coordinate, direction));
            Assert.Contains("The rover's plateau can't be empty", ex.Message);
            
        }
        
        [Fact]
        public void Fail_Boundry_Check_Out_Of_Plateau()
        {
            Plateau plateau = new Plateau(3, 3);
            Coordinate coordinate = new Coordinate(2, 1);
            char direction = 'N';

            Rover rover = new Rover(plateau, coordinate, direction);

            var ex = Assert.Throws<Exception>(() => rover.CheckPlateauBoundry(new Coordinate(4, 1)));
            Assert.Contains("The rover go out of bounds from plateau", ex.Message);
        }

        [Fact]
        public void Fail_Plateau_Boundry_Check_Without_Coordinate()
        {
            Rover rover = new Rover();
            
            var ex = Assert.Throws<Exception>(() => rover.CheckPlateauBoundry(null));
            Assert.Contains("Coordinate can't be empty", ex.Message);
        }

        [Fact]
        public void Fail_Plateau_Boundry_Violation()
        {
            Rover rover = new Rover();

            Coordinate coordinate = new Coordinate(2, 1);

            var ex = Assert.Throws<Exception>(() => rover.CheckPlateauBoundry(coordinate));
            Assert.Contains("The rover go out of bounds from plateau", ex.Message);
        }

        [Fact]
        public void Success_Rotate_Left()
        {
            Rover rover = new Rover();
            rover.Direction = 'N';
            char to = 'L';
            rover.Rotate(to); 

            Assert.Equal('W', rover.Direction);
        }

        [Fact]
        public void Success_Rotate_Right()
        {
            Rover rover = new Rover();
            rover.Direction = 'N';
            char to = 'R';
            rover.Rotate(to);

            Assert.Equal('E', rover.Direction);
        }

        [Fact]
        public void Return_Rotate_Unexpected_Argument()
        {
            Rover rover = new Rover();
            rover.Direction = 'N';
            char to = 'X';
            rover.Rotate(to);

            Assert.Equal('N', rover.Direction);
        }

        [Fact]
        public void Succes_Move()
        {
            Plateau plateau = new Plateau(5, 5);
            Coordinate coordinate = new Coordinate(1, 2);
            char direction = 'N';

            Rover rover = new Rover(plateau, coordinate, direction);
            rover.Move();
            rover.Move();

            Assert.Equal(1, rover.Coordinate.X);
            Assert.Equal(4, rover.Coordinate.Y);
        }

        [Fact]
        public void Success_Execute()
        {
            Plateau plateau = new Plateau(5, 5);
            Coordinate coordinate = new Coordinate(1, 2);
            char direction = 'N';

            Rover rover = new Rover(plateau, coordinate, direction);

            rover.Execute(new string[] { "LMLMLMLMM" });

            Assert.Equal(1, rover.Coordinate.X);
            Assert.Equal(3, rover.Coordinate.Y);
            Assert.Equal('N', rover.Direction);
        }

        [Fact]
        public void Fail_Execute_Invalid_Command_Format()
        {
            Plateau plateau = new Plateau(5, 5);
            Coordinate coordinate = new Coordinate(1, 2);
            char direction = 'N';

            Rover rover = new Rover(plateau, coordinate, direction);
            
            var ex = Assert.Throws<Exception>(() => rover.Execute(new string[] { "LMLMLMLMM", "XX" }));
            Assert.Contains("Invalid rover command expection. Expected format LMLMLMLMM etc.", ex.Message);
        }

        [Fact]
        public void Fail_Execute_Invalid_Command_Charset()
        {
            Plateau plateau = new Plateau(5, 5);
            Coordinate coordinate = new Coordinate(1, 2);
            char direction = 'N';

            Rover rover = new Rover(plateau, coordinate, direction);

            var ex = Assert.Throws<Exception>(() => rover.Execute(new string[] { "XXML"}));
            Assert.Contains("Invalid rover command expection. Expected command codes L R or M", ex.Message);
        }
    }
}
