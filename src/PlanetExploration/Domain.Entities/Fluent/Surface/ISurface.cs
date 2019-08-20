using Domain.Entities.Fluent.Rover;

namespace Domain.Entities.Fluent.Surface
{
    public interface ISurface: ISurfaceInstance, ISurfaceDimension, ISurfaceRover
    {
        int GetWidth();
        int GetHeight();
        IRover GetRover();
        Entities.Quadrant GetQuadrant(int x, int y);
    }
}
