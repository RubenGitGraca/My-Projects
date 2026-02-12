# ☢️ Nuclear Reactor Simulator

A professional C# desktop simulation built with .NET Windows Forms. Manage a nuclear power plant, balance thermal energy, and trade on the energy market.

## 🚀 Features

* **Real-time Core Simulation:** Advanced thermal dynamics balancing fuel consumption and cooling.
* **Dual Fuel Support:** Switch between high-yield **Uranium** or low-cost **Coal**.
* **Logistics Management:** Transfer fuel from stockpiles to the active core.
* **Economy System:** Sell generated MWh to the grid and reinvest in resources (Ice, Fuel).
* **Emergency Protocols:** Manual **SCRAM** system and emergency coolant injection.

## 🛠️ Tech Stack

* **Language:** C# 12
* **Framework:** .NET 8.0 / Windows Forms
* **Architecture:** Model-View-Controller (MVC) approach.

## 🕹️ How to Play

1.  **Start the Pump:** Ensure water level is above 10% to generate steam.
2.  **Adjust Control Rods:** Pull them out (0%) to increase temperature and power output.
3.  **Monitor Integrity:** Keeping the core above 1200°C for too long will cause structural damage.
4.  **Trade:** Go to the **Market** to convert your energy buffer into Credits.
5.  **Refuel:** Don't let the core run out of fuel, or the temperature will drop and power generation will stop.

## 📦 Installation & Run

Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed.

```bash
# Clone the repository
git clone [https://github.com/your-username/NuclearReactorSim.git](https://github.com/your-username/NuclearReactorSim.git)

# Navigate to the folder
cd NuclearReactorSim

# Build and Run
dotnet run