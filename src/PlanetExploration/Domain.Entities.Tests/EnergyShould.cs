using Xunit;

namespace Domain.Entities.Tests
{
    public class EnergyShould
    {
        [Fact]
        public void energy_has_energy()
        {
            var energy = Energy.Create(100).Build();

            Assert.True(energy.HasEnergy());
        }

        [Fact]
        public void energy_discharge_works_correctly()
        {
            var energy = Energy.Create(100).Build();

            energy.Discharge(10);

            Assert.True(energy.GetLoad() == 90);
        }

        [Fact]
        public void energy_charge_works_correctly()
        {
            var energy = Energy.Create(100).Build();

            energy.Discharge(10);
            Assert.True(energy.GetLoad() == 90);

            energy.Charge();
            Assert.True(energy.GetLoad() == 100);
        }
    }
}
