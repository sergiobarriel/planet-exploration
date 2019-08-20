namespace Domain.Entities.Abstractions.Quadrant
{
    public interface IQuadrantInstance
    {
        IQuadrantPosition SetPosition(int x, int y);
    }
}