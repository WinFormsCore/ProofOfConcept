using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsWinFormsCoreW10Api
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var testButton = new Button()
            {
                Text = "Get Geolocation",
                Location = new Point(100, 100),
                Size = new Size(100, 50)
            };

            testButton.Click += TestButton_Click;
            Controls.Add(testButton);
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            var locator = new Windows.Devices.Geolocation.Geolocator();
            var location = await locator.GetGeopositionAsync();
            var position = location.Coordinate.Point.Position;
        }
    }
}
