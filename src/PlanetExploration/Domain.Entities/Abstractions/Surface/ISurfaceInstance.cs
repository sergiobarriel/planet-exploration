namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurfaceInstance
    {
        ISurfaceObstacle WithObstacles(int weight);
        ISurfaceObstacle WithoutObstacles();
    }
}
