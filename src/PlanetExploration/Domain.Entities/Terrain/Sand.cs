using Domain.Entities.Abstractions;

namespace Domain.Entities.Terrain
{
    public class Sand : ITerrain
    {
        public bool IsObstacle { get; set; }

        public Sand()
        {
            IsObstacle = false;
        }
    }
}
