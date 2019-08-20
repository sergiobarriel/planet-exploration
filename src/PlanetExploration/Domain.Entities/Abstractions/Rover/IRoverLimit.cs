namespace Domain.Entities.Abstractions.Rover
{
    public interface IRoverLimit
    {
        IRoverPosition SetPosition(int x, int y);
    }
}
