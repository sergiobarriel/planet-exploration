using System;
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

            SetConsoleDefaults();

            PrintInfo(surface.GetRover());

            for (var height = surface.GetHeight(); height >= 1; height--)
            {
                for (var width = 1; width <= surface.GetWidth(); width++)
                {
                    var quadrant = surface.GetQuadrant(width, height);

                    if (surface.GetRover().GetPosition().GetX() == width
                        && surface.GetRover().GetPosition().GetY() == height)
                    {
                        switch (surface.GetRover().GetDirection())
                        {
                            case Direction.North:
                                PrintRover("N");
                                break;
                            case Direction.South:
                                PrintRover("S");
                                break;
                            case Direction.East:
                                PrintRover("E");
                                break;
                            case Direction.West:
                                PrintRover("W");
                                break;
                        }
                    }
                    else if (quadrant.GetObject() != null && quadrant.GetObject() is Sand) PrintSand();
                    else if (quadrant.GetObject() != null && quadrant.GetObject() is Rock) PrintRock();
                    
                }

                Jump();
            }
        }

        public static void Jump(int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                System.Console.Write("\r\n");
        }

        private static void PrintInfo(Rover rover)
        {
            Separator();
            Info($"Energy: {rover.GetEnergy()}");
            Info($"Direction: {rover.GetDirection()}");
            Info($"X position: {rover.GetPosition().GetX()}");
            Info($"Y position: {rover.GetPosition().GetY()}");
            Separator();
            Jump();
        }


        private static void SameLine(string value, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            SetConsoleColor(foreColor, backgroundColor);

            System.Console.Write($" {value} ");

            SetConsoleDefaults();
        }

        private static void NewLine(string value, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            SetConsoleColor(foreColor, backgroundColor);

            System.Console.WriteLine($" {value} ");

            SetConsoleDefaults();
        }

        private static void SetConsoleColor(ConsoleColor foreColor, ConsoleColor backgroundColor)
        {
            System.Console.ForegroundColor = foreColor;
            System.Console.BackgroundColor = backgroundColor;
        }

        private static void SetConsoleDefaults()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;
        }


        private static void PrintRover(string direction) => SameLine($"{direction}", ConsoleColor.Black, ConsoleColor.White);
        private static void PrintSand() => SameLine($"S");
        private static void PrintRock() => SameLine($"R", ConsoleColor.White, ConsoleColor.DarkGray);

        private static void Separator() => NewLine($"-----------------");
        private static void Info(string info) => NewLine(info);
    }
}
