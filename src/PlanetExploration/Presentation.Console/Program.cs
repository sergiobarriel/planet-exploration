using Domain.Entities;
using Domain.Entities.Enums;

namespace Presentation.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var surface = Surface.Create()
                .SetDimension(15, 15)
                .SetRover(Rover.Create()
                    .SetLimits(15, 15)
                    .SetPosition(5, 5)
                    .SetDirection(Direction.North)
                    .SetEnergy(100)
                    .Build())
                .Build();

            Display.PrintSurface(surface);

            while (true)
            {
                Display.Jump();

                System.Console.Write("Enter commands: ");
                var commands = System.Console.ReadLine();

                surface.GetRover().ExecuteCommands(commands);

                Display.PrintSurface(surface);
            }
        }
    }
}
