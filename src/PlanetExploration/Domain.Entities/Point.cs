using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

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
