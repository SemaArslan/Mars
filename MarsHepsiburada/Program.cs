using System;
using System.Text.RegularExpressions;

namespace MarsHepsiburada
{
    enum AppStates {
        PLATAUE_CONFIGURATION,
        ROVER_CONFIGURATION,
        ROVER_COMMAND_EXECUTION
    }

    class Program
    {
        private static AppStates currentState = AppStates.PLATAUE_CONFIGURATION;

        public static void Main(string[] args)
        {
            Console.WriteLine("- Plateau Configuration ");
            Console.WriteLine("- Rover Configuration  ");
            Console.WriteLine("- Rover Command Execution ");
            Console.WriteLine("'Exit' for stoping application");

            Plateau plateau = new Plateau(0,0);
            Rover rover = new Rover();

            while (true)
            {
                string inputCommand = Console.ReadLine();
                string[] commandParts = inputCommand.Split(' ');

                try
                {
                    if (String.IsNullOrEmpty(inputCommand))
                    {
                        throw new Exception("Unknown command expection");
                    }
                    
                    if (inputCommand == "Exit")
                    {
                        return;
                    }

                    if (currentState == AppStates.PLATAUE_CONFIGURATION)
                    {
                        plateau = createPlateau(commandParts);
                        currentState = AppStates.ROVER_CONFIGURATION;
                    }
                    else if (currentState == AppStates.ROVER_CONFIGURATION)
                    {
                        rover = createRover(commandParts, plateau);
                        currentState = AppStates.ROVER_COMMAND_EXECUTION;
                    }
                    else if (currentState == AppStates.ROVER_COMMAND_EXECUTION)
                    {
                        rover.Execute(commandParts);
                        Console.WriteLine("Rover current position is {0}", rover.ToString());
                        currentState = AppStates.ROVER_CONFIGURATION;
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                    continue;
                }
                
            }
            
        }

        private static Plateau createPlateau(string[] commandParts)
        {
            if (commandParts.Length != 2)
            {
                throw new Exception("Invalid plataeu configuration command expection. Expected format 5 5 etc.");
            }

            int sizeX, sizeY = 0;

            bool isNumericSizeX = Int32.TryParse(commandParts[0], out sizeX);
            bool isNumericSizeY = Int32.TryParse(commandParts[1], out sizeY);

            if (!isNumericSizeX || !isNumericSizeY)
            {
                throw new Exception("Invalid plataeu configuration command expection. All arguments must be number");
            }

            if (sizeX < 0 || sizeY < 0)
            {
                throw new Exception("Invalid plataeu configuration command expection. All arguments must be positive number");
            }

            return new Plateau(sizeX, sizeY);
        }

        private static Rover createRover(string[] commandParts, Plateau plateau)
        {
            if (commandParts.Length != 3)
            {
                throw new Exception("Invalid rover configuration command expection. Expected format 1 2 N etc.");
            }

            int coordinateX, coordinateY = 0;

            bool isNumericX = Int32.TryParse(commandParts[0], out coordinateX);
            bool isNumericY = Int32.TryParse(commandParts[1], out coordinateY);
            string direction = commandParts[2];

            if (!isNumericX || !isNumericY || !Regex.IsMatch(commandParts[2], @"\b[NESW]\w*\b"))
            {
                throw new Exception("Invalid rover configuration command expection. Expected format 1 2 N etc.");
            }

            return new Rover(plateau, new Coordinate(coordinateX, coordinateY), direction[0]);
        }
    }
    
}
