using System;
using MinecraftInventory.Models;

public class Program {

    public static void Main(string[] args)
    {
        // Initialize Chest and Armor Stand
        Chest ItemChest = new Chest("ItemChest");
        ArmorStand playerStand = new ArmorStand("Empty", "Empty", "Empty", "Empty");

        //Add some initial items to the chest for demonstration
        ItemChest.AddItem("Diamonds", 64);
        ItemChest.AddItem("Iron Ingots", 32);
        ItemChest.AddItem("Bread", 16);

        //Main loop
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Minecraft Inventory System ===");
            Console.WriteLine("1. Open Chest");
            Console.WriteLine("2. Open Armor Stand");
            Console.WriteLine("3. Add Item to Chest");
            Console.WriteLine("4. Add Armor to Armor Stand");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine($"\n--- {ItemChest.type} Contents ---");
                    if (ItemChest.Contents.Count == 0)
                    {
                        Console.WriteLine("The chest is currently empty.");
                    }
                    else
                    {
                        foreach (var item in ItemChest.Contents)
                        {
                            Console.WriteLine($"- {item.Key}: x{item.Value}");
                        }
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.WriteLine("\n--- Armor Stand Status ---");
                    playerStand.DisplayArmor();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Write("Enter Item Name: ");
                    string itemName = Console.ReadLine();
                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        ItemChest.AddItem(itemName, quantity);
                        Console.WriteLine($"{quantity}x {itemName} added to the chest!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please enter a number.");
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "4":
                    // Corrected names to match ArmorStand.cs
                    Console.Write("Enter Helmet: ");
                    playerStand.Head = Console.ReadLine(); 
                    Console.Write("Enter Chestplate: ");
                    playerStand.ChestPlate = Console.ReadLine();
                    Console.Write("Enter Leggings: ");
                    playerStand.Leggings = Console.ReadLine();
                    Console.Write("Enter Boots: ");
                    playerStand.Boots = Console.ReadLine();
                    
                    Console.WriteLine("\nArmor updated!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Closing Inventory... Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid selection. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}