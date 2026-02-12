namespace NuclearReactorSim.Models
{
    public class CoolingSystem
    {
        public double WaterLevel { get; set; } // 0.0 to 100.0
        public int IceQuantity { get; set; }
        public double CoolingEfficiency { get; private set; } = 2.5;

        public CoolingSystem()
        {
            WaterLevel = 100.0;
            IceQuantity = 5;
        }

        public double CalculateCooling(double currentTemperature)
        {
            // Passive evaporation: Water evaporates faster at high temperatures
            if (currentTemperature > 100)
            {
                double evaporationRate = (currentTemperature / 1000.0) * 0.2;
                WaterLevel -= evaporationRate;
            }

            if (WaterLevel < 0) WaterLevel = 0;

            // Cooling power depends on having water
            double waterCooling = (WaterLevel / 100.0) * CoolingEfficiency * (currentTemperature * 0.05);
            
            double iceCooling = 0;
            if (IceQuantity > 0 && currentTemperature > 500)
            {
                iceCooling = 50.0; 
                // Logic to consume ice can be added here
            }

            return waterCooling + iceCooling;
        }

        public void AddIce(int amount) => IceQuantity += amount;
    }
}