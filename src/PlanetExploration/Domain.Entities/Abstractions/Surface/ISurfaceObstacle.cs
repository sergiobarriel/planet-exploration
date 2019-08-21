namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurfaceObstacle
    {
        ISurfaceDimension SetDimension(int x = 10, int y = 10);
    }
}