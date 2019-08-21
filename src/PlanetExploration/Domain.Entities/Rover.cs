using System;
using System.Collections.Generic;
using Domain.Entities.Abstractions.Energy;
using Domain.Entities.Abstractions.Point;
using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Abstractions.Surface;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class Rover : IRover
    {
        private IList<char> Commands { get; set; }
        private IPoint Position { get; set; }
        private Direction Direction { get; set; }
        private IEnergy Energy { get; set; }
        private ISurface Surface { get; set; }

        private int MaxWidthSurface { get; set; }
        private int MaxHeightSurface { get; set; }

        public IPoint GetPosition() => Position;
        public Direction GetDirection() => Direction;
        public IEnergy GetEnergy() => Energy;

        private Rover()
        {
            Commands = new List<char>();
        }

        public static IRoverInstance Create() => new Rover();

        public IRoverLimit SetLimits(int width, int height)
        {
            MaxWidthSurface = width;
            MaxHeightSurface = height;

            return this;
        }

        public IRoverPosition SetPosition(int x = 1, int y = 1)
        {
            if (x < 1) x = 1;
            if (x > MaxWidthSurface) x = MaxWidthSurface;

            if (y < 1) y = 1;
            if (y > MaxHeightSurface) y = MaxHeightSurface;

            Position = Point.Create(x, y).Build();

            return this;
        }

        public IRoverDirection SetDirection(Direction direction)
        {
            Direction = direction;
            return this;
        }

        public IRoverEnergy SetEnergy(decimal energy)
        {
            Energy = Entities.Energy.Create(energy).Build();

            return this;
        }

        public IRover Build() => this;

        /// <summary>
        /// Set parent surface on rover
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        public IRover SetSurface(ISurface surface)
        {
            if (surface is null) throw new Exception($"Rover must contain Surface");

            if (Surface is null) Surface = surface;

            return this;
        }

        public void ExecuteCommands(string commands)
        {
            foreach (var command in commands.ToUpper().ToCharArray())
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(char command)
        {
            if (!Energy.HasEnergy()) return;

            if (command.Equals('A')) Advance();
            if (command.Equals('B')) Back();
            if (command.Equals('R')) TurnRight();
            if (command.Equals('L')) TurnLeft();
            if (command.Equals('D')) Drill();

            Commands.Add(command);
        }

        /// <summary>
        /// Check that next position of rover has a obstacle
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        private bool NextPositionIsObstacle(Axis axis, int increment)
        {
            var x = axis == Axis.X ? Position.GetX() + increment : Position.GetX();
            var y = axis == Axis.Y ? Position.GetY() + increment : Position.GetY();

            if (x >= 1 && y >= 1 && x <= MaxWidthSurface && y <= MaxHeightSurface)
            {
                var quadrant = Surface.GetQuadrant(x, y);

                if (quadrant != null)
                {
                    return quadrant.GetObject().IsObstacle;
                }
            }

            return true;
        }

        public void Advance()
        {
            if (!Energy.HasEnergy()) return;

            switch (Direction)
            {
                case Direction.North:

                    if (!NextPositionIsObstacle(Axis.Y, 1))
                    {
                        Position.Increase(Axis.Y, MaxHeightSurface);
                        Energy.Discharge(1m);
                    }
                    break;

                case Direction.South:

                    if (!NextPositionIsObstacle(Axis.Y, -1))
                    {
                        Position.Decrease(Axis.Y);
                        Energy.Discharge(1m);
                    }
                    break;

                case Direction.East:

                    if (!NextPositionIsObstacle(Axis.X, 1))
                    {
                        Position.Increase(Axis.X, MaxWidthSurface);
                        Energy.Discharge(1m);
                    }
                    break;

                case Direction.West:

                    if (!NextPositionIsObstacle(Axis.X, -1))
                    {
                        Position.Decrease(Axis.X);
                        Energy.Discharge(1m);
                    }
                    break;
            }

            Energy.Discharge(0.1m);
        }

        public void Back()
        {
            if (!Energy.HasEnergy()) return;

            switch (Direction)
            {
                case Direction.North:
                    Position.Decrease(Axis.Y);
                    break;
                case Direction.South:
                    Position.Increase(Axis.Y, MaxHeightSurface);
                    break;
                case Direction.East:
                    Position.Decrease(Axis.X);
                    break;
                case Direction.West:
                    Position.Increase(Axis.X, MaxWidthSurface);
                    break;
            }

            Energy.Discharge(1);
        }

        public void TurnLeft()
        {
            if (!Energy.HasEnergy()) return;

            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.West;
                    break;
                case Direction.South:
                    Direction = Direction.East;
                    break;
                case Direction.East:
                    Direction = Direction.North;
                    break;
                case Direction.West:
                    Direction = Direction.South;
                    break;
            }

            Energy.Discharge(0.5m);
        }

        public void TurnRight()
        {
            if (!Energy.HasEnergy()) return;

            switch (Direction)
            {
                case Direction.North:
                    Direction = Direction.East;
                    break;
                case Direction.South:
                    Direction = Direction.West;
                    break;
                case Direction.East:
                    Direction = Direction.South;
                    break;
                case Direction.West:
                    Direction = Direction.North;
                    break;
            }

            Energy.Discharge(0.5m);
        }

        public void Drill()
        {
            if (Energy.HasEnergy())
            {
                // TODO... 

                Energy.Discharge(2m);
            }
        }


    }
}
