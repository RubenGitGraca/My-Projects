using System;
using System.Collections.Generic;

namespace MinecraftInventory.Models
{
    public class Chest
    {
        // Variables
        public string type;
        public Dictionary<string, int> Contents = new Dictionary<string, int>();

        // Constructor
        public Chest(string chestType)
        {
            type = chestType;
        } 

        // Methods
        public void AddItem(string itemName, int quantity)
        {
            if (Contents.ContainsKey(itemName))
            {
                Contents[itemName] += quantity;
            }
            else
            {
                Contents.Add(itemName, quantity);
            }
            if (Contents[itemName] > 64)
            {
                Console.WriteLine($"Cannot add {quantity}x {itemName}. Maximum stack size is 64. Current quantity: 64");
                Contents[itemName] = 64;
            }
        } 
    } 
} 