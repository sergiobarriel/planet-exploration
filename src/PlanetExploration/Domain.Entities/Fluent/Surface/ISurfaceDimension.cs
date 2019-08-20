using Domain.Entities.Fluent.Rover;

namespace Domain.Entities.Fluent.Surface
{
    public interface ISurfaceDimension
    {
        ISurfaceRover SetRover(IRover rover);
    }
}
