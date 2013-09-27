using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        Timer timer = new Timer();

        private void Splash_Activated(object sender, EventArgs e)
        {
            timer.Tick += timer_Tick;
            timer.Interval = 10;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 100)
            {
                this.Opacity++;
                return;
            }
            timer.Stop();
        }
    }
}
