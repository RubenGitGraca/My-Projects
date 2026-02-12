namespace NuclearReactorSim.Models
{
    public class BusinessModel
    {
        public double Credits { get; set; } = 5000.0; // Starting money
        public double PowerGridPrice { get; private set; } = 0.12; // Credits per MWh
        
        // Resource Prices (Can be dynamic later)
        public double UraniumPricePerUnit { get; } = 2.5;
        public double CoalPricePerUnit { get; } = 0.5;
        public double IcePricePerUnit { get; } = 10.0;

        public bool SellEnergy(ref double energyBuffer)
        {
            if (energyBuffer <= 0) return false;

            double profit = energyBuffer * PowerGridPrice;
            Credits += profit;
            energyBuffer = 0; // Reset buffer after sale
            return true;
        }

        public bool BuyResource(string type, int amount, Reactor reactor)
        {
            double cost = 0;

            switch (type.ToLower())
            {
                case "uranium": cost = amount * UraniumPricePerUnit; break;
                case "coal": cost = amount * CoalPricePerUnit; break;
                case "ice": cost = amount * IcePricePerUnit; break;
            }

            if (Credits >= cost)
            {
                Credits -= cost;
                if (type == "uranium") reactor.UraniumStorage += amount;
                if (type == "coal") reactor.CoalStorage += amount;
                if (type == "ice") reactor.Cooling.AddIce(amount);
                return true;
            }

            return false; // Not enough money
        }
    }
}