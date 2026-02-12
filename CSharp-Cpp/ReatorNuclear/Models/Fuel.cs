namespace NuclearReactorSim.Models
{
    public class Fuel
    {
        public string Type { get; set; } // Settable for fuel swaps
        public double Amount { get; set; }
        public double BurnRate { get; set; }

        public Fuel(string type, double amount, double burnRate)
        {
            Type = type;
            Amount = amount;
            BurnRate = burnRate;
        }

        public static Fuel CreateUranium(double amount) => new Fuel("Uranium", amount, 0.5);
        public static Fuel CreateCoal(double amount) => new Fuel("Coal", amount, 0.1);

        public double Burn(double intensity)
        {
            if (Amount <= 0) return 0;
            double consumption = BurnRate * intensity;
            Amount -= (consumption * 0.1); // Slow consumption for better gameplay
            return intensity * (Type == "Uranium" ? 150.0 : 25.0);
        }
    }
}