using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class Rover : IRover
    {
        private Point Position { get; set; }
        private Direction Direction { get; set; }
        private Energy Energy { get; set; }

        private int MaxWidthSurface { get; set; }
        private int MaxHeightSurface { get; set; }

        public Point GetPosition() => Position;
        public Direction GetDirection() => Direction;
        public Energy GetEnergy() => Energy;

        private Rover() { }

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

            Position = new Point(x, y);

            return this;
        }

        public IRoverDirection SetDirection(Direction direction)
        {
            Direction = direction;
            return this;
        }

        public IRoverEnergy SetEnergy(decimal energy)
        {
            Energy = new Energy(energy);
            return this;
        }

        public IRover Build() => this;

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
        }

        public void Advance()
        {
            if (!Energy.HasEnergy()) return;

            switch (Direction)
            {
                case Direction.North:
                    Position.Increase(Axis.Y, MaxHeightSurface);
                    break;
                case Direction.South:
                    Position.Decrease(Axis.Y);
                    break;
                case Direction.East:
                    Position.Increase(Axis.X, MaxWidthSurface);
                    break;
                case Direction.West:
                    Position.Decrease(Axis.X);
                    break;
            }

            Energy.Discharge(1);
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
                Energy.Discharge(2m);
            }
        }

    }
}
