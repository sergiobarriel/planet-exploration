using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Fluent.Surface;
using Random = Domain.Entities.Utils.Random;

namespace Domain.Entities
{
    public class Surface : ISurface
    {
        private HashSet<Quadrant> Quadrants { get; set; }
        private Rover Rover { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }

        private Surface() => Quadrants = new HashSet<Quadrant>();

        public static ISurfaceInstance Create() => new Surface();

        /// <summary>
        /// Set surface dimension and create all quadrants with objects inside
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public ISurfaceDimension SetDimension(int width = 10, int height = 10)
        {
            Width = width <= 0 ? 10 : width;
            Height = height <= 0 ? 10 : height;

            for (var w = 1; w <= Width; w++)
                for (var h = 1; h <= Height; h++)
                    AddQuadrant(w, h);

            return this;
        }

        public ISurfaceRover SetRover(Rover rover)
        {
            Rover = rover;
            return this;
        }

        public Surface Build()
        {
            if (IsReady()) return this;

            throw new Exception("Invalid surface");
        }

        /// <summary>
        /// Add Quadrant to HashSet in specific position.
        /// Set Sand or Rocks object inside (based on specific probability)
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        private void AddQuadrant(int w, int h)
        {
            if (Random.GetRandom(trueWeight: 90))
            {
                Quadrants.Add(Quadrant.Create()
                    .SetPosition(w, h)
                    .SetTerrain(Enums.Terrain.Sand)
                    .Build());
            }
            else
            {
                Quadrants.Add(Quadrant.Create()
                    .SetPosition(w, h)
                    .SetTerrain(Enums.Terrain.Rock)
                    .Build());
            }
        }

        /// <summary>
        /// Return quadrant based on specific cartesian coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Quadrant GetQuadrant(int x, int y) => Quadrants.FirstOrDefault(quadrant => quadrant.Point.GetX() == x
                                                                                          && quadrant.Point.GetY() == y);

        public int GetWidth() => Width;
        public int GetHeight() => Height;
        public Rover GetRover() => Rover;

        /// <summary>
        /// Surface has:
        /// - Width and Height
        /// - Quadrants
        /// </summary>
        /// <returns></returns>
        public bool IsReady() => Width > 0
                                 && Height > 0
                                 && Quadrants != null
                                 && Quadrants.Count() == Width * Height;

    }
}
