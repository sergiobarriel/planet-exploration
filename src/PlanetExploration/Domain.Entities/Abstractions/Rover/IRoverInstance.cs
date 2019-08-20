namespace Domain.Entities.Abstractions.Rover
{
    public interface IRoverInstance
    {
        IRoverLimit SetLimits(int width, int height);
    }
}
