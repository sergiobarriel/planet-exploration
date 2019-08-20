using System;
using Domain.Entities.Abstractions;
using Domain.Entities.Abstractions.Point;
using Domain.Entities.Abstractions.Quadrant;
using Domain.Entities.Terrain;

namespace Domain.Entities
{
    public class Quadrant : IQuadrant
    {
        public IPoint Point { get; set; }
        private ITerrain Terrain { get; set; }

        private Quadrant() { }

        public ITerrain GetObject() => Terrain;
        public IPoint GetPoint() => Point;

        public static IQuadrantInstance Create() => new Quadrant();

        public IQuadrantPosition SetPosition(int x, int y)
        {
            if (x <= 0) throw new Exception($"{nameof(x)} must be greater than zero.");
            if (y <= 0) throw new Exception($"{nameof(y)} must be greater than zero.");

            Point = Entities.Point.Create(x, y).Build();

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
