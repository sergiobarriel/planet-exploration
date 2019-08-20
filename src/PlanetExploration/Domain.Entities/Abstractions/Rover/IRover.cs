using Domain.Entities.Abstractions.Energy;
using Domain.Entities.Abstractions.Point;
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

        IPoint GetPosition();
        Direction GetDirection();
        IEnergy GetEnergy();
    }
}
