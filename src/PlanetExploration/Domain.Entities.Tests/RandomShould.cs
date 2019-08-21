using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Utils;
using Xunit;

namespace Domain.Entities.Tests
{
    public class RandomShould
    {
        [Fact]
        public void get_random_number_in_range_returns_random_number_in_range()
        {
            var random = Random.GetRandom(1, 100);

            Assert.InRange(random, 1, 100);
        }

        [Fact]
        public void get_random_number_with_full_true_weight_returns_true()
        {
            for (var i = 0; i < 100000; i++)
            {
                var random = Random.GetRandom(100);
                Assert.True(random);
            }
        }

        [Fact]
        public void get_random_number_with_full_false_weight_returns_false()
        {
            for (var i = 0; i < 100000; i++)
            {
                var random = Random.GetRandom(0);
                Assert.False(random);
            }
        }

        [Fact]
        public void get_random_number_with_middle_weight_returns_true_and_false()
        {
            var results = new List<bool>();

            for (var i = 0; i < 100000; i++)
            {
                results.Add(Random.GetRandom(50));
            }

            Assert.Contains(results, item => item == true);
            Assert.Contains(results, item => item == false);
        }
    }
}
