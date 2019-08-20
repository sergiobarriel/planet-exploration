using Xunit;

namespace Domain.Entities.Tests
{
    public class SurfaceShould
    {
        [Fact]
        public void surface_default_instance_with_default_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .SetDimension()
                .SetRover(null)
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }

        [Fact]
        public void surface_default_instance_with_dimensions_returns_dimension()
        {
            var surface = Surface.Create()
                .SetDimension(20, 20)
                .SetRover(null)
                .Build();

            Assert.True(surface.GetWidth() == 20);
            Assert.True(surface.GetHeight() == 20);
        }

        [Fact]
        public void surface_default_instance_with_zero_based_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .SetDimension(0, 0)
                .SetRover(null)
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }

        [Fact]
        public void surface_default_instance_with_negative_based_dimension_returns_default_dimension()
        {
            var surface = Surface.Create()
                .SetDimension(-10, -10)
                .SetRover(null)
                .Build();

            Assert.True(surface.GetWidth() == 10);
            Assert.True(surface.GetHeight() == 10);
        }
    }
}
