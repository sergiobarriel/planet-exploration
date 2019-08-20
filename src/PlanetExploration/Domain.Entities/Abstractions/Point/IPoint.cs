using Domain.Entities.Enums;

namespace Domain.Entities.Abstractions.Point
{
    public interface IPoint : IPointInstance
    {
        int GetX();
        int GetY();
        void Increase(Axis axis, int max);
        void Decrease(Axis axis);
    }
}
