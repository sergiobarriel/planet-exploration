namespace Domain.Entities.Fluent.Rover
{
    public interface IRoverInstance
    {
        IRoverLimit SetLimits(int width, int heigth);
    }
}
