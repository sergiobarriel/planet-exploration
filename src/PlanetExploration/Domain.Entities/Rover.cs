using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Abstractions.Energy;
using Domain.Entities.Abstractions.Point;
using Domain.Entities.Abstractions.Quadrant;
using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Abstractions.Surface;
using Domain.Entities.Enums;
using Domain.Entities.Terrain;

namespace Domain.Entities
{
    public class Rover : IRover
    {
        private IList<char> Commands { get; set; }
        private IPoint Position { get; set; }
        private Direction Direction { get; set; }
        private IEnergy Energy { get; set; }
        private ISurface Surface { get; set; }

        public IPoint GetPosition() => Position;
        public Direction GetDirection() => Direction;
        public IEnergy GetEnergy() => Energy;

        private Rover()
        {
            Commands = new List<char>();
        }

        public static IRoverInstance Create() => new Rover();

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

            if (Surface is null)
            {
                Surface = surface;
                SetRoverInFreeRandomPosition();
            }

            return this;
        }

        /// <summary>
        /// Get all free quadrants and set rover y random position
        /// </summary>
        private void SetRoverInFreeRandomPosition()
        {
            var freeQuadrants = Surface.GetFreeQuadrantsForRover();

            var quadrant = freeQuadrants.ElementAt(Utils.Random.GetRandom(0, freeQuadrants.Count() - 1));

            Position = Point.Create(quadrant.GetPoint().GetX(), quadrant.GetPoint().GetY()).Build();
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

            if (x >= 1 && y >= 1 && x <= Surface.GetWidth() && y <= Surface.GetHeight())
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
            if (!Energy.HasSpecificEnergy(1m)) return;

            switch (Direction)
            {
                case Direction.North:

                    if (!NextPositionIsObstacle(Axis.Y, 1))
                    {
                        Position.Increase(Axis.Y, Surface.GetHeight());
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
                        Position.Increase(Axis.X, Surface.GetWidth());
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
            if (!Energy.HasSpecificEnergy(1)) return;

            switch (Direction)
            {
                case Direction.North:

                    if (!NextPositionIsObstacle(Axis.Y, -1))
                    {
                        Position.Decrease(Axis.Y);
                        Energy.Discharge(1);
                    }
                    break;

                case Direction.South:
                    
                    if (!NextPositionIsObstacle(Axis.Y, 1))
                    {
                        Position.Increase(Axis.Y, Surface.GetHeight());
                        Energy.Discharge(1);
                    }
                    break;

                case Direction.East:
                   
                    if (!NextPositionIsObstacle(Axis.X, -1))
                    {
                        Position.Decrease(Axis.X);
                        Energy.Discharge(1);
                    }
                    break;

                case Direction.West:

                    if (!NextPositionIsObstacle(Axis.X, 1))
                    {
                        Position.Increase(Axis.X, Surface.GetWidth());
                        Energy.Discharge(1);
                    }
                    break;
            }
        }

        public void TurnLeft()
        {
            if (!Energy.HasSpecificEnergy(0.5m)) return;

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
            if (!Energy.HasSpecificEnergy(0.5m)) return;

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
            if (Energy.HasSpecificEnergy(2m))
            {
                var quadrant = GetAdjoiningQuadrant();

                if (!(quadrant is null))
                {
                    var @object = quadrant.GetObject();

                    if (@object is Rock)
                    {
                        if (@object.HasWater)
                        {
                            // TODO: What's happens?                            
                        }

                        if (@object.HasPlutonium) Energy.Charge();

                        quadrant.DrillQuadrant();
                        Energy.Discharge(2m);
                    }
                    else
                    {
                        Energy.Discharge(1m);
                    }

                }
                else
                {
                    Energy.Discharge(1m);
                }

            }
        }


        private IQuadrant GetAdjoiningQuadrant()
        {
            IQuadrant quadrant = null;

            switch (Direction)
            {
                case Direction.North:

                    if(Position.GetY() < Surface.GetHeight())
                        quadrant = Surface.GetQuadrant(Position.GetX(), Position.GetY() + 1);

                    break;
                case Direction.South:

                    if (Position.GetY() > 1)
                        quadrant = Surface.GetQuadrant(Position.GetX(), Position.GetY() - 1);

                    break;
                case Direction.East:

                    if (Position.GetX() < Surface.GetWidth())
                        quadrant = Surface.GetQuadrant(Position.GetX() + 1, Position.GetY());

                    break;
                case Direction.West:

                    if (Position.GetX() > 1)
                        quadrant = Surface.GetQuadrant(Position.GetX() - 1, Position.GetY());

                    break;
            }

            return quadrant;
        }


    }
}
