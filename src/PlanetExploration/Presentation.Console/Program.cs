using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Presentation.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Display.SetConsoleDefaults();

            var surface = Surface.Create()
                .WithObstacles(weight: 1)
                .SetDimension(15, 15)
                .SetRover(Rover.Create()
                    .SetDirection(Direction.North)
                    .SetEnergy(100)
                    .Build())
                .Build();

            Display.PrintPanel(surface, null);

            while (true)
            {
                Display.PrintCommandRequest();

                var commands = System.Console.ReadLine();
                var message = surface.GetRover().ExecuteCommands(commands);

                Display.Clear();

                Display.PrintPanel(surface, message);
            }
        }
    }
}
