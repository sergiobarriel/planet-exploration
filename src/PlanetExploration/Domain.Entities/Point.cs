using Domain.Entities.Abstractions.Point;
using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class Point : IPoint
    {
        private int X { get; set; }
        private int Y { get; set; }

        private Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static IPointInstance Create(int x, int y) => new Point(x, y);
        public IPoint Build() => this;

        public int GetX() => X;
        public int GetY() => Y;

        public void Increase(Axis axis, int max)
        {
            switch (axis)
            {
                case Axis.X:
                    if (X < max) X++;
                    break;

                case Axis.Y:
                    if (Y < max) Y++;
                    break;
            }
        }
        public void Decrease(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    if (X > 1) X--;
                    break;

                case Axis.Y:
                    if (Y > 1) Y--;
                    break;
            }
        }

    }
}
