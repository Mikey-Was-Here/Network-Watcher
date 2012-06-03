using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetworkWatcher
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
             Api.MIB_TCPROW_OWNER_PID[] cn = Api.GetAllTcpConnections();
             foreach (Api.MIB_TCPROW_OWNER_PID item in cn)
             {
                 ListViewItem lvi = new ListViewItem(item.remoteAddr.ToString());
                 listView1.Items.Add(lvi);
             }
        }
    }
}
