using System;
using System.Windows.Forms;
using NuclearReactorSim.Models;
using NuclearReactorSim.UI;

namespace NuclearReactorSim
{
    static class Program
    {
        // We create these here so they persist between different windows
        public static Reactor GlobalReactor = new Reactor();
        public static BusinessModel GlobalEconomy = new BusinessModel();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Starts with the Main Menu
            Application.Run(new MainMenu());
        }
    }
}