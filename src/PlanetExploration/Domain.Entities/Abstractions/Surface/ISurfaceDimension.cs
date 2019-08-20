using Domain.Entities.Abstractions.Rover;

namespace Domain.Entities.Abstractions.Surface
{
    public interface ISurfaceDimension
    {
        ISurfaceRover SetRover(IRover rover);
    }
}
