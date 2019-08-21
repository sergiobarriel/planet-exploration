using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Abstractions.Quadrant;
using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Abstractions.Surface;
using Random = Domain.Entities.Utils.Random;

namespace Domain.Entities
{
    public class Surface : ISurface
    {
        private HashSet<IQuadrant> Quadrants { get; set; }
        private IRover Rover { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        private int WeightOfObstacules { get; set; }

        private Surface() => Quadrants = new HashSet<IQuadrant>();

        public static ISurfaceInstance Create() => new Surface();

        /// <summary>
        /// Set surface dimension and create all quadrants with objects inside
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="withObstacles"></param>
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


        public ISurfaceObstacle WithObstacles(int weight = 10)
        {
            WeightOfObstacules = weight;
            return this;
        }

        public ISurfaceObstacle WithoutObstacles()
        {
            WeightOfObstacules = 0;
            return this;
        }

        /// <summary>
        /// Set surface (this) on rover
        /// </summary>
        /// <param name="rover"></param>
        /// <returns></returns>
        public ISurfaceRover SetRover(IRover rover)
        {
            if(rover is null) throw new Exception($"Rover is null.");

            Rover = rover.SetSurface(this);

            return this;
        }


        public ISurface Build()
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
            var weight = 100 - WeightOfObstacules;

            if (Random.GetRandom(weight: weight))
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
        public IQuadrant GetQuadrant(int x, int y)
        {
            var element = Quadrants.FirstOrDefault(quadrant => quadrant.GetPoint().GetX() == x && quadrant.GetPoint().GetY() == y);

            if (element != null) return element;

            throw new Exception($"{nameof(Quadrant)} ({x},{y}) not found.");
        }

        public IEnumerable<IQuadrant> GetQuadrants() => Quadrants;

        public int GetWidth() => Width;
        public int GetHeight() => Height;
        public IRover GetRover() => Rover;

        /// <summary>
        /// Surface has:
        /// - Width and Height
        /// - Quadrants
        /// </summary>
        /// <returns></returns>
        private bool IsReady() => Width >= 10
                                 && Height >= 10
                                 && Quadrants != null
                                 && Quadrants.Count() == Width * Height;


    }
}
