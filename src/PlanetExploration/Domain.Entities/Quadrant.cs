using Domain.Entities.Abstractions;
using Domain.Entities.Fluent.Quadrant;
using Domain.Entities.Terrain;

namespace Domain.Entities
{
    public class Quadrant : IQuadrant
    {
        public Point Point { get; set; }
        private ITerrain Terrain { get; set; }

        private Quadrant() {}

        public ITerrain GetObject() => Terrain;
        public Point GetPoint() => Point;

        public static IQuadrantInstance Create() => new Quadrant();

        public IQuadrantPosition SetPosition(int x, int y)
        {
            Point = new Point(x, y);

            return this;
        }

        public IQuadrantTerrain SetTerrain(Enums.Terrain terrain = Enums.Terrain.Sand)
        {
            switch (terrain)
            {
                case Enums.Terrain.Sand:
                    Terrain = new Sand();
                    break;
                case Enums.Terrain.Rock:
                    Terrain = new Rock();
                    break;
            }

            return this;
        }

        public IQuadrant Build() => this;

    }
}
