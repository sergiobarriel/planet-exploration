namespace Domain.Entities.Abstractions.Energy
{
    public interface IEnergy : IEnergyInstance
    {
        void Charge();
        void Discharge(decimal energy);

        bool HasEnergy();
        bool HasSpecificEnergy(decimal energy);
        decimal GetLoad();
    }
}
