namespace Domain.Entities.Abstractions
{
    public interface ITerrain
    {
        bool IsObstacle { get; set; }
        bool HasWater { get; set; }
        bool HasPlutonium { get; set; }
    }
}
