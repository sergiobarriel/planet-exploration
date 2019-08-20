using Domain.Entities.Abstractions.Energy;

namespace Domain.Entities
{
    public class Energy : IEnergy
    {
        private decimal Load { get; set; }

        private Energy(decimal energy) => Load = energy;
        public static IEnergyInstance Create(decimal energy) => new Energy(energy);

        public IEnergy Build() => this;

        public void Charge() => Load = 100;
        public void Discharge(decimal energy) => Load -= energy;

        public bool HasEnergy() => Load > 0;
        public decimal GetLoad() => Load;
    }
}
