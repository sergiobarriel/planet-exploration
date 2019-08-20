namespace Domain.Entities.Fluent.Rover
{
    public interface IRoverAction
    {
        void Advance();
        void Back();
        void TurnLeft();
        void TurnRight();
        void Drill();
        void ExecuteCommands(string commands);
    }
}
