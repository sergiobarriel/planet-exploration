using Domain.Entities.Enums;

namespace Domain.Entities.Abstractions.Rover
{
    public interface IRoverInstance
    {
        IRoverDirection SetDirection(Direction direction);
    }
}
