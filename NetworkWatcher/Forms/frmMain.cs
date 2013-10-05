using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;
using NetworkWatcher.Entity;

namespace NetworkWatcher.Forms
{
    public partial class frmMain : Form
    {
        public NotifyIcon Tray { get; private set; }

        private SetHostNameDelegate shnd;
        private FillInArinInfoDelegate fhnd;
        private SetProcessNameDelegate spnd;
        private SetGeoLocationDelegate sgld;

        public void cmRefresh(Object sender, EventArgs e)
        {
            Color saved = lvMain.BackColor;
            lvMain.BackColor = Color.Black;
            lvMain.Refresh();
            this.LoadList();
            lvMain.BackColor = saved;
            lvMain.Refresh();
        }

        public void cmExit(Object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public frmMain()
        {
            InitializeComponent();
            Tray = new NotifyIcon();
            ContextMenu cm = new System.Windows.Forms.ContextMenu();
            cm.MenuItems.Add("&Refresh", cmRefresh);
            cm.MenuItems.Add("E&xit", cmExit);
            Tray.ContextMenu = cm;
            Tray.Visible = true;
            Tray.Icon = this.Icon;

            shnd = SetHostName;
            fhnd = SetArinInfo;
            spnd = SetProcessName;
            sgld = SetGeoLocation;
        }

        public void SetHostName(ListViewItem lvi, string hostName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(shnd, lvi, hostName);
            }
            else
            {
                lvi.SubItems[5].Text = hostName;
            }
        }

        public delegate void SetHostNameDelegate(ListViewItem lvi, string hostName);

        public void FillInHostName(ListViewItem lvi, IPAddress ipa)
        {
            string hostName = string.Empty;
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipa);
                hostName = entry.HostName;
            }
            catch { }

            this.Invoke(shnd, lvi, hostName);
        }

        public delegate void FillInArinInfoDelegate(ListViewItem lvi, string hostName);

        public void SetArinInfo(ListViewItem lvi, string ArinInfo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(fhnd, lvi, ArinInfo);
            }
            else
            {
                lvi.SubItems[6].Text = ArinInfo;
            }
        }

        public void FillInArinInfo(ListViewItem lvi, IPAddress ipa)
        {
            string ArinInfo = ArinApi.GetOrginization(ipa);

            this.Invoke(fhnd, lvi, ArinInfo);
        }

        public delegate void SetProcessNameDelegate(ListViewItem lvi, string hostName);

        public void SetProcessName(ListViewItem lvi, string processName)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(spnd, lvi, processName);
            }
            else
            {
                lvi.SubItems[3].Text = processName;
            }
            
        }

        public void FillInProcessName(ListViewItem lvi, int pid, Connection connection)
        {
            Process p = null;
            try
            {
                p = Process.GetProcessById(pid);
            }
            catch { }

            string name = p == null ? "Unknown" : p.ProcessName;
            this.Invoke(spnd, lvi, name);
            connection.ProcessInfo = new ProcessInfo(pid, name);
        }

        public delegate void SetGeoLocationDelegate(ListViewItem lvi, string location);

        public void SetGeoLocation(ListViewItem lvi, string location)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(sgld, lvi, location);
            }
            else
            {
                lvi.SubItems[4].Text = location;
            }
        }

        public void FillInGeoLocation(ListViewItem lvi, IPAddress ipa)
        {
            GeoLocationData geoData = null;
            try
            {
                geoData = GeoLocationApi.GetLocation(ipa);
            }
            catch { }

            string data = string.Empty;

            if (geoData != null && !string.IsNullOrEmpty(geoData.CountryCode) && !string.IsNullOrEmpty(geoData.CountryName))
            {
                data = geoData.CountryCode + "/" + geoData.CountryName;
                if (!string.IsNullOrEmpty(geoData.RegionName) && !string.IsNullOrEmpty(geoData.City))
                {
                    data += "/" + geoData.RegionName + "/" + geoData.City;
                    if (geoData.Longitude.HasValue && geoData.Latitude.HasValue)
                    {
                        data += "/" + geoData.Longitude.Value + "/" + geoData.Latitude.Value;
                    }
                }
            }
            else
            {
                data = "Unknown";
            }

            this.Invoke(sgld, lvi, data);
        }

        private Dictionary<int, ListViewItem> xl = new Dictionary<int, ListViewItem>();

        private void LoadList()
        {
            Connections connections = Historical.Add();

            Api.MIB_TCPROW_OWNER_PID[] cn = Api.GetAllTcpConnections();
            foreach (Api.MIB_TCPROW_OWNER_PID item in cn)
            {
                ip4 remoteAddr = new ip4 { u = item.remoteAddr };
                int remotePort = item.remotePort1;
                ip4 localAddr = new ip4 { u = item.localAddr };
                int localPort = item.localPort1;

                string CountryCode = string.Empty;

                if (item.remoteAddr != 0 && remoteAddr.b1 != 127 && (!(remoteAddr.b1 == 192 && remoteAddr.b2 == 168)))
                {
                    IPAddress ipa = new IPAddress(new byte[] { remoteAddr.b1, remoteAddr.b2, remoteAddr.b3, remoteAddr.b4 });

                    IPAddress localIpa = new IPAddress(new byte[] { localAddr.b1, localAddr.b2, localAddr.b3, localAddr.b4 });

                    string remoteData =
                        string.Format("{0}.{1}.{2}.{3}:{4}",
                            remoteAddr.b1,
                            remoteAddr.b2,
                            remoteAddr.b3,
                            remoteAddr.b4,
                            Functions.FirstNonZero(
                                item.remotePort1,
                                item.remotePort2,
                                item.remotePort3,
                                item.remotePort4));

                    string localData = string.Format("{0}.{1}.{2}.{3}:{4}", localAddr.b1, localAddr.b2, localAddr.b3, localAddr.b4, localPort);

                    ListViewItem lvi = new ListViewItem(remoteData);

                    lvi.SubItems.Add(localData);
                    lvi.SubItems.Add(item.owningPid.ToString());

                    Connection connection = new Connection(ipa, localIpa, item.owningPid);
                    connections.Add(connection);
                    connection.ConnectionDataComplete += connection_ConnectionDataComplete;
                    if (!xl.ContainsKey(connection.GetHashCode()))
                    {
                        xl.Add(connection.GetHashCode(), lvi);
                    }

                    lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInProcessName(lvi, item.owningPid, connection); });

                    lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInGeoLocation(lvi, ipa); });

                    lvi.SubItems.Add(string.Empty);

                    lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInHostName(lvi, ipa); });
                    //Task.Run(() => { FillInArinInfo(lvi, ipa); });

                    lvMain.Items.Add(lvi);
                    
                }
            }
        }

        void connection_ConnectionDataComplete(object sender, EventArgs e)
        {
            Connection connection = sender as Connection;
            if (connection != null)
            {
                ListViewItem lvi = xl[connection.GetHashCode()];
                spnd.Invoke(lvi, connection.ProcessInfo.ProcessName);
                shnd.Invoke(lvi, connection.HostName);
                fhnd.Invoke(lvi, connection.Owner);
                sgld.Invoke(lvi, Functions.GetDisplayGeoLocation(connection.Location));
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string localName = Dns.GetHostName();
            IPHostEntry localHost = Dns.GetHostEntry(localName);
            IPAddress[] localAddress = localHost.AddressList;

            try
            {
                IPAddress external = null;
                for (int i = 1; i < 30; i++)
                {
                    IPAddress ipa = Functions.Ping("www.google.com", i);
                    if (!Functions.IsLocal(ipa))
                    {
                        external = ipa;
                        break;
                    }
                }

                if (external != null)
                {
                    ListViewItem lvi = lvInfo.Items.Add("External Address");
                    lvi.SubItems.Add(Functions.FormatIp(external));
                }
            }
            catch { }

            for (int i = 0; i < localAddress.Length; i++)
            {
                ListViewItem lvi = lvInfo.Items.Add("Local Address");
                lvi.SubItems.Add(Functions.FormatIp(localAddress[i]));
            }

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

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //lvInfo.Columns[0].Width = lvInfo.Columns[1].Width = (this.Width - 2) / 2;
        }

        private void splitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.CreateSubKey);
            try
            {
                key = key.CreateSubKey("NetworkWatcher", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
            }
            catch (Exception ex)
            {
                key = key.OpenSubKey("NetworkWatcher");
            }
            key.SetValue("Splitter", splitter.SplitterDistance);
        }

        private void lvInfo_SizeChanged(object sender, EventArgs e)
        {
            lvInfo.Columns[1].Width = lvInfo.Width - lvInfo.Columns[0].Width - 7;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
    }
}