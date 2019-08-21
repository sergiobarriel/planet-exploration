using Domain.Entities.Abstractions.Point;

namespace Domain.Entities.Abstractions.Quadrant
{
    public interface IQuadrant : IQuadrantInstance, IQuadrantPosition, IQuadrantTerrain
    {
        ITerrain GetObject();
        IPoint GetPoint();
        void DrillQuadrant();
    }
}
