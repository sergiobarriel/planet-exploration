namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurfaceInstance
    {
        ISurfaceObstacle WithObstacles();
        ISurfaceObstacle WithoutObstacles();
    }
}
