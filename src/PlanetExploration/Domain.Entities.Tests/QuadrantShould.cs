using System;
using Domain.Entities.Abstractions;
using Domain.Entities.Terrain;
using Xunit;

namespace Domain.Entities.Tests
{
    public class QuadrantShould
    {
        [Fact]
        public void quadrant_with_zero_zero_position_returns_exception()
        {
            Assert.Throws<Exception>(() =>
            {
                Quadrant.Create()
                    .SetPosition(0, 0)
                    .SetTerrain(Enums.Terrain.Rock)
                    .Build();
            });
        }

        [Fact]
        public void quadrant_with_non_zero_zero_position_returns_build_correctly()
        {
            var quadrant = Quadrant.Create()
                .SetPosition(1, 1)
                .SetTerrain(Enums.Terrain.Rock)
                .Build();

            Assert.True(quadrant.GetPoint().GetX() == 1);
            Assert.True(quadrant.GetPoint().GetY() == 1);
            Assert.True(quadrant.GetObject() is ITerrain);
        }
    }
}
