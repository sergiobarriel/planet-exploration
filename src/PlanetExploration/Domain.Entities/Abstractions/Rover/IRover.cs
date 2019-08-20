using Domain.Entities.Enums;

namespace Domain.Entities.Abstractions.Rover
{
    public interface IRover : IRoverDirection, IRoverEnergy, IRoverInstance, IRoverLimit, IRoverPosition
    {
        void Advance();
        void Back();
        void TurnLeft();
        void TurnRight();
        void Drill();
        void ExecuteCommands(string commands);

        Point GetPosition();
        Direction GetDirection();
        Energy GetEnergy();
    }
}
