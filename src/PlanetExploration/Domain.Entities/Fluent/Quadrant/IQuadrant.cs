using Domain.Entities.Abstractions;

namespace Domain.Entities.Fluent.Quadrant
{
    public interface IQuadrant : IQuadrantInstance, IQuadrantPosition, IQuadrantTerrain
    {
        ITerrain GetObject();
        Point GetPoint();
    }
}
