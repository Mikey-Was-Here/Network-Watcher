using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Raven.Client;

namespace NetworkWatcher
{
    [StructLayout(LayoutKind.Explicit)]
    struct ip4
    {
        [FieldOffset(0)]
        public byte b1;
        [FieldOffset(1)]
        public byte b2;
        [FieldOffset(2)]
        public byte b3;
        [FieldOffset(3)]
        public byte b4;
        [FieldOffset(0)]
        public uint u;
    }

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Saving changes using the session API
            using (IDocumentSession session = Program.DocumentStore.OpenSession())
            {    // Operations against session
                IpLocation loc = new IpLocation();
                loc.fromIp = 16777216;
                loc.toIp = 16777471;
                loc.locationId = 17;
                session.Store(loc);
                session.SaveChanges();
            }

            Api.MIB_TCPROW_OWNER_PID[] cn = Api.GetAllTcpConnections();
            foreach (Api.MIB_TCPROW_OWNER_PID item in cn)
            {
                ip4 i = new ip4 { u = item.remoteAddr };
                string msg = string.Format("{0}.{1}.{2}.{3}:{4}", i.b1, i.b2, i.b3, i.b4, item.remotePort1);
                ListViewItem lvi = new ListViewItem(msg);
                listView1.Items.Add(msg);
            }

            dynamic d = Api.GetAllTcpConnections();
        }
    }
}
