using Domain.Entities.Enums;

namespace Domain.Entities
{
    public class Energy
    {
        private decimal Load { get; set; }

        public Energy(decimal energy)
        {
            Load = energy;
        }

        public decimal GetEnergy() => Load;

        public void Charge()
        {
            Load = 100;
        }

        public void Discharge(decimal energy)
        {
            Load -= energy;
        }

        public bool HasEnergy() => Load > 0;
        public decimal GetLoad() => Load;
    }
}
