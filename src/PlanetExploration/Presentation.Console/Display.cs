using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Abstractions.Surface;
using Domain.Entities.Enums;
using Domain.Entities.Terrain;

namespace Presentation.Console
{
    public static class Display
    {
        public static void Clear() => System.Console.Clear();

        #region Helpers

        /// <summary>
        /// Jump n times
        /// </summary>
        /// <param name="amount"></param>
        private static void Jump(int amount = 1)
        {
            for (var i = 0; i < amount; i++)
                System.Console.Write("\r\n");
        }

        /// <summary>
        /// Set default foreground and background color for console
        /// </summary>
        public static void SetConsoleDefaults()
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Set console to specific foreground and background color
        /// </summary>
        /// <param name="foreColor"></param>
        /// <param name="backgroundColor"></param>
        private static void SetConsoleColor(ConsoleColor foreColor, ConsoleColor backgroundColor)
        {
            System.Console.ForegroundColor = foreColor;
            System.Console.BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Print text in same line (foreground and background colors are optionally)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="foreColor"></param>
        /// <param name="backgroundColor"></param>
        private static void PrintInSameLine(string value, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            SetConsoleColor(foreColor, backgroundColor);

            System.Console.Write($" {value} ");

            SetConsoleDefaults();
        }

        /// <summary>
        /// Print text in new line (foreground and background colors are optionally)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="foreColor"></param>
        /// <param name="backgroundColor"></param>
        private static void PrintInNewLine(string value, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            SetConsoleColor(foreColor, backgroundColor);

            System.Console.WriteLine($" {value} ");

            SetConsoleDefaults();
        }

        #endregion

        public static void PrintCommandRequest()
        {
            Jump();
            PrintInNewLine("(A)dvance, (B)ack, (R)ight, (L)eft, (D)rill");
            Jump();
            PrintInSameLine("Enter commands: ");
        }

        public static void PrintPanel(ISurface surface, string message)
        {
            PrintRoverInfo(surface.GetRover(), message);
            PrintSurface(surface);
        }

        private static void PrintSurface(ISurface surface)
        {
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
                    else if (quadrant.GetObject() is Sand) PrintSand();
                    else if (quadrant.GetObject() is Rock) PrintRock();

                }

                Jump();
            }
        }

        private static void PrintRoverInfo(IRover rover, string message)
        {
            PrintInNewLine("----------------------------");

            PrintInNewLine($"Energy: {rover.GetEnergy().GetLoad()}");
            PrintInNewLine($"Direction: {rover.GetDirection()}");
            PrintInNewLine($"Position (x: {rover.GetPosition().GetX()}, y: {rover.GetPosition().GetY()})");
            PrintInSameLine("Messages: ");

            if(message != null)
                PrintInSameLine($"{message} ");

            Jump();

            PrintInNewLine("----------------------------");

            Jump();
        }




        #region Facades

        private static void PrintRover(string direction) => PrintInSameLine($"{direction}", ConsoleColor.Black, ConsoleColor.White);
        private static void PrintSand() => PrintInSameLine("s");
        private static void PrintRock() => PrintInSameLine("R", ConsoleColor.White, ConsoleColor.DarkGray);

        #endregion

    }
}
