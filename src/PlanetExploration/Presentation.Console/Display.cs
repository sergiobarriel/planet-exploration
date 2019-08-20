using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Terrain;

namespace Presentation.Console
{
    public static class Display
    {
        public static void PrintSurface(Surface surface)
        {
            System.Console.Clear();

            for (var height = surface.GetHeight() - 1; height >= 1; height--)
            {
                for (var width = 1; width <= surface.GetWidth() - 1; width++)
                {
                    var quadrant = surface.GetQuadrant(width, height);

                    if (surface.GetRover().GetPosition().GetX() == width
                        && surface.GetRover().GetPosition().GetY() == height)
                    {
                        switch (surface.GetRover().GetDirection())
                        {
                            case Direction.North:
                                System.Console.Write("N ");
                                break;
                            case Direction.South:
                                System.Console.Write("S ");
                                break;
                            case Direction.East:
                                System.Console.Write("E ");
                                break;
                            case Direction.West:
                                System.Console.Write("W ");
                                break;
                        }
                    }
                    else if (quadrant.GetObject() != null && quadrant.GetObject() is Sand)
                    {
                        System.Console.Write("X ");
                    }

                    else if (quadrant.GetObject() != null && quadrant.GetObject() is Rock)
                    {
                        System.Console.Write("R ");
                    }
                }
                System.Console.Write("\r\n");
            }
        }

        public static void PrintRover(Rover rover)
        {
            System.Console.Write("\r\n");
            System.Console.WriteLine($"-------------------");
            System.Console.WriteLine($"Energy: {rover.GetEnergy()}");
            System.Console.WriteLine($"Direction: {rover.GetDirection()}");
            System.Console.WriteLine($"X position: {rover.GetPosition().GetX()}");
            System.Console.WriteLine($"Y position: {rover.GetPosition().GetY()}");
            System.Console.WriteLine($"-------------------");
        }
    }
}
