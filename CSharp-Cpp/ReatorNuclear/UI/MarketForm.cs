using System;
using System.Drawing;
using System.Windows.Forms;
using NuclearReactorSim.Models;

namespace NuclearReactorSim.UI
{
    /// <summary>
    /// Handles the trading of energy for credits and the procurement of reactor resources.
    /// </summary>
    public class MarketForm : Form
    {
        // UI Components
        private readonly System.Windows.Forms.Timer _uiRefreshTimer;
        private Label _lblFinancialSummary = null!;
        private FlowLayoutPanel _pnlTradeItems = null!;

        // Constants for styling
        private static readonly Color BackgroundColor = Color.FromArgb(25, 25, 30);
        private static readonly Color HeaderColor = Color.Black;
        private static readonly Color AccentColor = Color.Gold;

        public MarketForm()
        {
            InitializeComponent();

            // Setup UI Refresh Timer (500ms interval for smooth updates)
            _uiRefreshTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _uiRefreshTimer.Tick += (sender, e) => RefreshMarketData();
            _uiRefreshTimer.Start();
        }

        private void InitializeComponent()
        {
            // Form Configuration
            this.Text = "ENERGY TRADING & LOGISTICS TERMINAL";
            this.Size = new Size(650, 550);
            this.BackColor = BackgroundColor;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // Financial Summary Header
            _lblFinancialSummary = new Label
            {
                Dock = DockStyle.Top,
                Height = 130,
                ForeColor = AccentColor,
                BackColor = HeaderColor,
                Font = new Font("Consolas", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Text = "SYNCHRONIZING WITH GLOBAL MARKET..."
            };

            // Trade Items Container
            _pnlTradeItems = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25),
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            // Register Market Rows
            PopulateTradeOptions();

            this.Controls.Add(_pnlTradeItems);
            this.Controls.Add(_lblFinancialSummary);
        }

        private void PopulateTradeOptions()
        {
            // 1. SELL ACCUMULATED ENERGY
            AddTradeRow("SELL ACCUMULATED POWER", "Exchange MWh for Credits", Color.ForestGreen, (s, e) =>
            {
                double currentEnergyBuffer = Program.GlobalReactor.TotalEnergyGenerated;

                if (currentEnergyBuffer <= 0)
                {
                    MessageBox.Show("Energy storage is empty. No power available to sell.", "Trade Refused", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Program.GlobalEconomy.SellEnergy(ref currentEnergyBuffer))
                {
                    Program.GlobalReactor.ResetEnergyCounter();
                }
            });

            // 2. PURCHASE URANIUM
            AddTradeRow("PROCURE URANIUM (500u)", "Cost: 1,250 Credits", Color.DarkOliveGreen, (s, e) =>
            {
                if (!Program.GlobalEconomy.BuyResource("uranium", 500, Program.GlobalReactor))
                {
                    ShowFundsWarning();
                }
            });

            // 3. PURCHASE COAL
            AddTradeRow("PROCURE COAL (1000u)", "Cost: 500 Credits", Color.Sienna, (s, e) =>
            {
                if (!Program.GlobalEconomy.BuyResource("coal", 1000, Program.GlobalReactor))
                {
                    ShowFundsWarning();
                }
            });

            // 4. EMERGENCY COOLANT (ICE)
            AddTradeRow("RESTOCK EMERGENCY ICE (5u)", "Cost: 50 Credits", Color.SteelBlue, (s, e) =>
            {
                if (!Program.GlobalEconomy.BuyResource("ice", 5, Program.GlobalReactor))
                {
                    ShowFundsWarning();
                }
            });
        }

        private void AddTradeRow(string actionTitle, string detailInfo, Color buttonTheme, EventHandler onActionClick)
        {
            Panel rowContainer = new Panel { Size = new Size(580, 80), Margin = new Padding(0, 0, 0, 10) };

            Button actionButton = new Button
            {
                Text = actionTitle,
                Size = new Size(320, 45),
                Location = new Point(0, 5),
                BackColor = buttonTheme,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            actionButton.Click += onActionClick;

            Label infoLabel = new Label
            {
                Text = detailInfo,
                ForeColor = Color.LightGray,
                Location = new Point(330, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };

            rowContainer.Controls.Add(actionButton);
            rowContainer.Controls.Add(infoLabel);
            _pnlTradeItems.Controls.Add(rowContainer);
        }

        private void RefreshMarketData()
        {
            var economy = Program.GlobalEconomy;
            var reactor = Program.GlobalReactor;

            _lblFinancialSummary.Text = "GLOBAL ECONOMY INTERFACE\n" +
                                        "------------------------------\n" +
                                        $"ACCOUNT BALANCE  : {economy.Credits,10:N2} Cr\n" +
                                        $"UNSOLD ENERGY    : {reactor.TotalEnergyGenerated,10:F4} MWh\n" +
                                        $"URANIUM RESERVES : {reactor.UraniumStorage,10:F0} units";
        }

        private void ShowFundsWarning()
        {
            MessageBox.Show("Transaction Failed: Insufficient credits in account.", "Financial Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uiRefreshTimer?.Stop();
                _uiRefreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}