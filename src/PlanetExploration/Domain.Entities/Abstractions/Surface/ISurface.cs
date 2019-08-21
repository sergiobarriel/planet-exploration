using System.Collections.Generic;
using Domain.Entities.Abstractions.Quadrant;
using Domain.Entities.Abstractions.Rover;

namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurface: ISurfaceInstance, ISurfaceDimension, ISurfaceObstacle, ISurfaceRover
    {
        int GetWidth();
        int GetHeight();
        IRover GetRover();
        IQuadrant GetQuadrant(int x, int y);
        IEnumerable<IQuadrant> GetQuadrants();
    }
}
