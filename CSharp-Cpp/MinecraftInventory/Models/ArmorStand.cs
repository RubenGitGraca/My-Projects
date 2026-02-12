using System.Collections.Generic;

namespace MinecraftInventory.Models
{
    public class ArmorStand
    {
        //Variables
        public string Head;
        public string ChestPlate;
        public string Leggings;
        public string Boots;

        //Constructor
        public ArmorStand(string head, string chestPlate, string leggings, string boots)
        {
            Head = head;
            ChestPlate = chestPlate;
            Leggings = leggings;
            Boots = boots;
        }
        public void DisplayArmor()
        {
            Console.WriteLine($"Head: {Head}");
            Console.WriteLine($"Chest Plate: {ChestPlate}");
            Console.WriteLine($"Leggings: {Leggings}");
            Console.WriteLine($"Boots: {Boots}");
        }
    }
}
