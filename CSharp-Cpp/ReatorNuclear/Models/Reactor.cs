namespace NuclearReactorSim.Models
{
    public class Reactor
    {
        // Core State
        public double Temperature { get; private set; } = 20.0;
        public double TotalEnergyGenerated { get; private set; }
        public double StructuralIntegrity { get; private set; } = 100.0;
        public double ControlRodPosition { get; set; } = 1.0; 
        
        // Active components
        public Fuel ActiveFuel { get; set; }
        public CoolingSystem Cooling { get; set; }

        // INDUSTRIAL STORAGE (Inventory)
        public double UraniumStorage { get; set; } = 5000.0;
        public double CoalStorage { get; set; } = 2000.0;
        public int IceStorage { get; set; } = 20;

        public Reactor()
        {
            Cooling = new CoolingSystem();
            ActiveFuel = Fuel.CreateUranium(0); // Starts empty
        }

        public void Update()
        {
            // 1. Physics Loop
            double intensity = 1.0 - ControlRodPosition;
            double heatProduced = ActiveFuel.Burn(intensity);
            double coolingEffect = Cooling.CalculateCooling(Temperature);

            Temperature += (heatProduced - coolingEffect);
            Temperature -= (Temperature - 20.0) * 0.005; // Ambient dissipation

            // 2. Energy Output (Efficiency based on Steam)
            if (Temperature > 100 && Cooling.WaterLevel > 10)
                TotalEnergyGenerated += (Temperature - 100) * 0.02;

            // 3. Structural Damage
            if (Temperature > 2000) StructuralIntegrity -= 0.15;
            
            // 4. Auto-Refuel Logic from Storage (Simulating internal conveyors)
            if (ActiveFuel.Amount < 10 && UraniumStorage > 0 && ActiveFuel.Type == "Uranium")
            {
                double amountToTransfer = Math.Min(UraniumStorage, 100);
                ActiveFuel.Amount += amountToTransfer;
                UraniumStorage -= amountToTransfer;
            }
        }

        public void ResetEnergyCounter()
        {
            TotalEnergyGenerated = 0;
        }
    }
}