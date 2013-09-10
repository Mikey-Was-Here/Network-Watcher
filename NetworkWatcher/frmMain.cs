using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

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

        private void LoadList()
        {
            Api.MIB_TCPROW_OWNER_PID[] cn = Api.GetAllTcpConnections();
            foreach (Api.MIB_TCPROW_OWNER_PID item in cn)
            {
                ip4 remoteAddr = new ip4 { u = item.remoteAddr };
                int remotePort = item.remotePort1;
                ip4 localAddr = new ip4 { u = item.localAddr };
                int localPort = item.localPort1;

                string CountryCode = string.Empty;

                if (item.remoteAddr != 0 && remoteAddr.b1 != 127 && remoteAddr.b1 != 192)
                {
                    int foundLoc = Program.Iplocks.Find((long)item.remoteAddr);

                    CountryCode = Program.Locations.Find(foundLoc);

                    string remoteData = string.Format("{0}.{1}.{2}.{3}:{4}", remoteAddr.b1, remoteAddr.b2, remoteAddr.b3, remoteAddr.b4, item.remotePort1);
                    string localData = string.Format("{0}.{1}.{2}.{3}:{4}", localAddr.b1, localAddr.b2, localAddr.b3, localAddr.b4, localPort);

                    Process p = null;
                    try
                    {
                        p = Process.GetProcessById(item.owningPid);
                    }
                    catch { }

                    ListViewItem lvi = new ListViewItem(remoteData);

                    //lvi.SubItems.Add(remoteData);
                    lvi.SubItems.Add(localData);
                    lvi.SubItems.Add(item.owningPid.ToString());
                    lvi.SubItems.Add(p == null ? "Unknown" : p.ProcessName);
                    lvi.SubItems.Add(CountryCode);

                    string hostEntry = string.Empty;
                    try
                    {
                        IPHostEntry entry = Dns.GetHostEntry(string.Format("{0}.{1}.{2}.{3}", remoteAddr.b1, remoteAddr.b2, remoteAddr.b3, remoteAddr.b4));
                        hostEntry = entry.HostName;
                    }
                    catch { }

                    lvi.SubItems.Add(hostEntry);

                    lvMain.Items.Add(lvi);
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void frmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                lvMain.Items.Clear();
                LoadList();
            }
        }
    }
}
