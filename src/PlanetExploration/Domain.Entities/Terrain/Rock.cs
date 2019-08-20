using Domain.Entities.Abstractions;
using Domain.Entities.Utils;

namespace Domain.Entities.Terrain
{
    public class Rock : ITerrain
    {
        public bool IsObstacle { get; set; }
        public bool HasWater { get; set; }

        public Rock()
        {
            IsObstacle = true;
            HasWater = Random.GetRandom(50);
        }
    }
}
