using Domain.Entities.Enums;

namespace Domain.Entities.Fluent.Rover
{
    public interface IRoverPosition
    {
        IRoverDirection SetDirection(Direction direction);
    }
}
