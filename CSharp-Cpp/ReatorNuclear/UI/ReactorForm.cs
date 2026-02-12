using System;
using System.Drawing;
using System.Windows.Forms;
using NuclearReactorSim.Models;

namespace NuclearReactorSim.UI
{
    public class ReactorForm : Form
    {
        private System.Windows.Forms.Timer _updateTimer;
        
        // UI Elements - Initialized with null! to silence warnings
        private ProgressBar pbTemp = null!;
        private ProgressBar pbWater = null!;
        private ProgressBar pbUranium = null!;
        private ProgressBar pbCoal = null!;
        private TrackBar trkRods = null!;
        private Label lblMonitor = null!;
        private Label lblIntegrity = null!;
        private Label lblIce = null!;
        private Label lblPowerGenerated = null!;
        private Button btnPump = null!;
        private bool _isPumpRunning = false;

        public ReactorForm()
        {
            InitializeComponents();
            _updateTimer = new System.Windows.Forms.Timer { Interval = 100 };
            _updateTimer.Tick += (s, e) => UpdateUI();
            _updateTimer.Start();
        }

        private void InitializeComponents()
        {
            this.Text = "CORE CONTROL UNIT - OPERATIONAL VIEW";
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(1000, 650);
            this.BackColor = Color.FromArgb(20, 20, 20);

            // 1. MAIN LAYOUT (3 Columns)
            TableLayoutPanel mainLayout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 3, RowCount = 1,
                BackColor = Color.Transparent
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250)); // Left: Storage
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // Center: Terminal
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250)); // Right: Controls
            this.Controls.Add(mainLayout);

            // --- LEFT COLUMN: STORAGE & SUPPLIES ---
            FlowLayoutPanel pnlLeft = new FlowLayoutPanel { 
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, 
                Padding = new Padding(15), BackColor = Color.FromArgb(30, 30, 35) 
            };
            
            pnlLeft.Controls.Add(new Label { Text = "STORAGE & SUPPLIES", ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), Size = new Size(220, 30) });
            pbUranium = CreateLabeledBar(pnlLeft, "URANIUM STOCKPILE", Color.YellowGreen);
            pbCoal = CreateLabeledBar(pnlLeft, "COAL STOCKPILE", Color.SaddleBrown);

            Button btnLoadU = CreateActionButton("TRANSFER URANIUM (500u)", Color.DarkGreen);
            btnLoadU.Click += (s, e) => TransferFuel("uranium");
            pnlLeft.Controls.Add(btnLoadU);

            Button btnLoadC = CreateActionButton("TRANSFER COAL (500u)", Color.Chocolate);
            btnLoadC.Click += (s, e) => TransferFuel("coal");
            pnlLeft.Controls.Add(btnLoadC);

            lblIce = new Label { Text = "ICE UNITS: 0", ForeColor = Color.Cyan, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true, Margin = new Padding(0,20,0,10)};
            pnlLeft.Controls.Add(lblIce);

            Button btnIce = CreateActionButton("INJECT EMERGENCY ICE", Color.DeepSkyBlue);
            btnIce.Click += (s, e) => Program.GlobalReactor.Cooling.AddIce(1);
            pnlLeft.Controls.Add(btnIce);

            // --- CENTER COLUMN: MAIN TELEMETRY ---
            Panel pnlCenter = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            
            pnlCenter.Controls.Add(new Label { Text = "CORE TEMPERATURE GAUGE (°C)", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            pbTemp = new ProgressBar { Location = new Point(20, 45), Size = new Size(540, 40), Maximum = 2500, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            pnlCenter.Controls.Add(pbTemp);

            pnlCenter.Controls.Add(new Label { Text = "COOLANT RESERVOIR (H2O)", ForeColor = Color.LightBlue, Location = new Point(20, 100), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) });
            pbWater = new ProgressBar { Location = new Point(20, 125), Size = new Size(540, 25), Maximum = 100, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            pnlCenter.Controls.Add(pbWater);

            lblMonitor = new Label { 
                Location = new Point(20, 180), Size = new Size(540, 380), 
                BackColor = Color.Black, ForeColor = Color.Lime, 
                Font = new Font("Consolas", 11), Padding = new Padding(15),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlCenter.Controls.Add(lblMonitor);

            // --- RIGHT COLUMN: CONTROLS ---
            Panel pnlRight = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(30, 30, 35), Padding = new Padding(15) };
            
            Button btnScram = new Button { Text = "SCRAM", Dock = DockStyle.Top, Height = 80, BackColor = Color.DarkRed, ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), FlatStyle = FlatStyle.Flat };
            btnScram.Click += (s, e) => { trkRods.Value = 100; _isPumpRunning = true; };
            pnlRight.Controls.Add(btnScram);

            trkRods = new TrackBar { Orientation = Orientation.Vertical, Height = 250, Location = new Point(100, 130), Maximum = 100, Value = 100 };
            pnlRight.Controls.Add(new Label { Text = "RODS POSITION", ForeColor = Color.White, Location = new Point(85, 100), AutoSize = true });
            pnlRight.Controls.Add(trkRods);

            btnPump = new Button { Text = "START PUMP", Location = new Point(25, 400), Size = new Size(200, 50), BackColor = Color.DarkBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnPump.Click += (s, e) => _isPumpRunning = !_isPumpRunning;
            pnlRight.Controls.Add(btnPump);

            // FIXED SIZE LABELS to prevent text overflow
            lblIntegrity = new Label { Text = "INTEGRITY: 100%", ForeColor = Color.Lime, Location = new Point(25, 470), Size = new Size(210, 30), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            lblPowerGenerated = new Label { Text = "PWR: 0.00 MWh", ForeColor = Color.White, Location = new Point(25, 510), Size = new Size(210, 45), Font = new Font("Consolas", 10, FontStyle.Bold) };
            pnlRight.Controls.Add(lblIntegrity);
            pnlRight.Controls.Add(lblPowerGenerated);

            mainLayout.Controls.Add(pnlLeft, 0, 0);
            mainLayout.Controls.Add(pnlCenter, 1, 0);
            mainLayout.Controls.Add(pnlRight, 2, 0);
        }

        private ProgressBar CreateLabeledBar(Control parent, string text, Color color) {
            parent.Controls.Add(new Label { Text = text, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8), AutoSize = true, Margin = new Padding(0,10,0,0) });
            ProgressBar p = new ProgressBar { Size = new Size(190, 15), Maximum = 5000, Style = ProgressBarStyle.Continuous };
            parent.Controls.Add(p);
            return p;
        }

        private Button CreateActionButton(string text, Color color) => new Button { Text = text, Size = new Size(190, 40), BackColor = color, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Margin = new Padding(0,5,0,5), Font = new Font("Segoe UI", 8, FontStyle.Bold) };

        private void UpdateUI()
        {
            var r = Program.GlobalReactor;
            r.ControlRodPosition = trkRods.Value / 100.0;
            if (_isPumpRunning) r.Cooling.WaterLevel = Math.Min(r.Cooling.WaterLevel + 0.8, 100);
            r.Update();

            pbTemp.Value = (int)Math.Clamp(r.Temperature, 0, 2500);
            pbWater.Value = (int)r.Cooling.WaterLevel;
            pbUranium.Value = (int)Math.Clamp(r.UraniumStorage, 0, 5000);
            pbCoal.Value = (int)Math.Clamp(r.CoalStorage, 0, 5000);
            
            lblIce.Text = $"ICE UNITS: {r.Cooling.IceQuantity}";
            lblIntegrity.Text = $"INTEGRITY: {r.StructuralIntegrity:F1}%";
            lblPowerGenerated.Text = $"GENERATED:\n{r.TotalEnergyGenerated:N2} MWh";
            
            btnPump.Text = _isPumpRunning ? "STOP PUMP" : "START PUMP";
            btnPump.BackColor = _isPumpRunning ? Color.Maroon : Color.DarkBlue;

            lblMonitor.Text = $">> CORE TELEMETRY <<\n----------------------------\n" +
                              $"CORE TEMP   : {r.Temperature,8:F2} °C\n" +
                              $"WATER LVL   : {r.Cooling.WaterLevel,8:F1} %\n" +
                              $"FUEL LOAD   : {r.ActiveFuel.Amount,8:F2} u\n" +
                              $"FUEL TYPE   : {r.ActiveFuel.Type.ToUpper()}\n\n" +
                              $"[POWER OUTPUT BUFFER]\n{r.TotalEnergyGenerated,12:F4} MWh\n" +
                              $"----------------------------\n" +
                              $"PUMP STATUS : {(_isPumpRunning ? "ACTIVE" : "IDLE")}\n" +
                              $"CONTROL RODS: {trkRods.Value,3}% IN";
        }

        private void TransferFuel(string type) {
            var r = Program.GlobalReactor;
            if (type == "uranium" && r.UraniumStorage >= 500) { r.UraniumStorage -= 500; r.ActiveFuel.Amount += 500; r.ActiveFuel.Type = "Uranium"; }
            else if (type == "coal" && r.CoalStorage >= 500) { r.CoalStorage -= 500; r.ActiveFuel.Amount += 500; r.ActiveFuel.Type = "Coal"; }
            else { MessageBox.Show("Insufficient Stock!"); }
        }
    }
}