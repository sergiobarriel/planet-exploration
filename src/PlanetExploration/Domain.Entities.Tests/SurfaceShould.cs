using System.Linq;
using Domain.Entities.Abstractions.Rover;
using Domain.Entities.Enums;
using Xunit;

namespace Domain.Entities.Tests
{
    public class SurfaceShould
    {
        private IRover GetDefaultRover() => Rover.Create()
            .SetLimits(10, 10)
            .SetPosition(1, 1)
            .SetDirection(Direction.North)
            .SetEnergy(100)
            .Build();


            [Fact]
        public void surface_default_instance_with_default_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension()
                .SetRover(GetDefaultRover())
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }

        [Fact]
        public void surface_default_instance_with_dimensions_returns_dimension()
        {
            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(20, 20)
                .SetRover(GetDefaultRover())
                .Build();

            Assert.True(surface.GetWidth() == 20);
            Assert.True(surface.GetHeight() == 20);
        }

        [Fact]
        public void surface_default_instance_with_zero_based_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(0, 0)
                .SetRover(GetDefaultRover())
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }

        [Fact]
        public void surface_default_instance_with_negative_based_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(-10, -10)
                .SetRover(GetDefaultRover())
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }


        [Fact]
        public void surface_with_obstacles_contains_obstacles()
        {
            var surface = Surface.Create()
                .WithObstacles()
                .SetDimension(10, 10)
                .SetRover(GetDefaultRover())
                .Build();

            Assert.True(surface.GetQuadrants().Any(x => x.GetObject().IsObstacle));
        }

        [Fact]
        public void surface_without_obstacles_not_contains_obstacles()
        {
            var surface = Surface.Create()
                .WithoutObstacles()
                .SetDimension(10, 10)
                .SetRover(GetDefaultRover())
                .Build();

            Assert.False(surface.GetQuadrants().Any(x => x.GetObject().IsObstacle));
        }
    }
}
