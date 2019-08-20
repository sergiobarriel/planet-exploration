using Domain.Entities.Enums;

namespace Domain.Entities.Fluent.Quadrant
{
    public interface IQuadrantPosition
    {
        IQuadrantTerrain SetTerrain(Enums.Terrain terrain);

    }
}