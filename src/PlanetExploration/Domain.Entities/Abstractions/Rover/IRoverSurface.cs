using Domain.Entities.Abstractions.Surface;

namespace Domain.Entities.Abstractions.Rover
{
    public interface IRoverSurface
    {
        IRover SetSurface(ISurface surface);
    }
}
