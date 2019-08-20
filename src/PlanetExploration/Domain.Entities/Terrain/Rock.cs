using Domain.Entities.Abstractions;

namespace Domain.Entities.Terrain
{
    public class Rock : ITerrain
    {
        public bool IsObstacle { get; set; }

        public Rock()
        {
            IsObstacle = true;
        }
    }
}
