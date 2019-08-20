using Domain.Entities.Fluent.Quadrant;
using Domain.Entities.Fluent.Rover;

namespace Domain.Entities.Fluent.Surface
{
    public interface ISurface: ISurfaceInstance, ISurfaceDimension, ISurfaceRover
    {
        int GetWidth();
        int GetHeight();
        IRover GetRover();
        IQuadrant GetQuadrant(int x, int y);
    }
}
