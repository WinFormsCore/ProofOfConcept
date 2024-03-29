﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Geolocation;
using Windows.Devices.Sensors;

namespace CsWinFormsCoreW10Api
{
    public partial class Form1 : Form
    {
        private Geolocator _locator;
        private Accelerometer _accelerometor;
        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _statusLabel;

        public Form1()
        {
            InitializeComponent();
            _statusStrip = new StatusStrip();
            _statusLabel = new ToolStripStatusLabel("Not Initialized.");
            _statusStrip.Items.Add(_statusLabel);
            this.Controls.Add(_statusStrip);
            button1.Click += TestButton_Click;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _locator = new Geolocator();
            _locator.ReportInterval = 250;
            _locator.StatusChanged += Locator_StatusChanged;
            _locator.PositionChanged += Locator_PositionChanged;

            _accelerometor = Accelerometer.GetDefault(AccelerometerReadingType.Standard);
            _accelerometor.ReadingChanged += Accelerometor_ReadingChanged;
        }

        private void Accelerometor_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => Accelerometor_ReadingChanged(sender, args)));
                return;
            }

            lblXAccel.Text = $"{args.Reading.AccelerationX:0.00}";
            lblYAccel.Text = $"{args.Reading.AccelerationY:0.00}";
            lblZAccel.Text = $"{args.Reading.AccelerationZ:0.00}";
        }

        private void Locator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            _statusLabel.Text = args.Status.ToString();
        }

        private void Locator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => Locator_PositionChanged(sender, args)));
                return;
            }

            var position = args.Position.Coordinate.Point.Position;
            lblLatitude.Text = $"{position.Latitude:0.0000}";
            lblLongitude.Text = $"{position.Longitude:0.0000}";
            lblSpeed.Text = $"{args.Position.Coordinate.Speed:0.00}";
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            //var accelerator = Accelerometer.GetDefault(AccelerometerReadingType.Standard);
            //accelerator.
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _locator = null;
            _accelerometor = null;
        }
    }
}
