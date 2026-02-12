using System;
using System.Drawing;
using System.Windows.Forms;

namespace NuclearReactorSim.UI
{
    public class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Nuclear Plant Management Hub";
            this.Size = new Size(400, 500);
            this.BackColor = Color.FromArgb(20, 20, 25);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Label
            Label lblTitle = new Label {
                Text = "CENTRAL MANAGEMENT UNIT",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };

            // Reactor Button
            Button btnReactor = CreateMenuButton("CORE CONTROL ROOM", Color.DarkSlateGray, 100);
            btnReactor.Click += (s, e) => {
                ReactorForm reactorWindow = new ReactorForm();
                reactorWindow.Show(); 
            };

            // Market Button - UPDATED
            Button btnMarket = CreateMenuButton("ENERGY MARKET & LOGISTICS", Color.OliveDrab, 180);
            btnMarket.Click += (s, e) => {
                MarketForm marketWindow = new MarketForm();
                marketWindow.Show(); 
            };

            // Exit Button
            Button btnExit = CreateMenuButton("SYSTEM SHUTDOWN", Color.Maroon, 350);
            btnExit.Click += (s, e) => Application.Exit();

            // Footer
            Label lblFooter = new Label {
                Text = "Authorized Personnel Only",
                ForeColor = Color.Gray,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40
            };

            this.Controls.AddRange(new Control[] { btnReactor, btnMarket, btnExit, lblTitle, lblFooter });
        }

        private Button CreateMenuButton(string text, Color color, int top)
        {
            return new Button {
                Text = text,
                Location = new Point(50, top),
                Size = new Size(300, 60),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }
    }
}