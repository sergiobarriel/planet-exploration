using Domain.Entities.Enums;

namespace Domain.Entities.Abstractions.Rover
{
    public interface IRoverPosition
    {
        IRoverDirection SetDirection(Direction direction);
    }
}
