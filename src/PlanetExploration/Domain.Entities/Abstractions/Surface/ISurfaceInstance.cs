namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurfaceInstance
    {
        ISurfaceDimension SetDimension(int x = 10, int y = 10);
    }
}
