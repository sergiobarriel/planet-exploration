namespace Domain.Entities.Abstractions.Quadrant
{
    public interface IQuadrant : IQuadrantInstance, IQuadrantPosition, IQuadrantTerrain
    {
        ITerrain GetObject();
        Point GetPoint();
    }
}
