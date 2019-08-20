namespace Domain.Entities.Abstractions.Quadrant
{
    public interface IQuadrantPosition
    {
        IQuadrantTerrain SetTerrain(Enums.Terrain terrain);

    }
}