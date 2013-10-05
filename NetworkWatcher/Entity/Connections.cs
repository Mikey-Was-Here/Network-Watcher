using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace NetworkWatcher.Entity
{
    public class Connections : IList<Connection>
    {
        private List<Connection> list = new List<Connection>();

        private void Fill()
        {
            Connections connections = Historical.Add();

            Api.MIB_TCPROW_OWNER_PID[] cn = Api.GetAllTcpConnections();
            foreach (Api.MIB_TCPROW_OWNER_PID item in cn)
            {
                ip4 remoteAddr = new ip4 { u = item.remoteAddr };
                int remotePort = Functions.FirstNonZero(item.remotePort1, item.remotePort2, item.remotePort3, item.remotePort4);
                ip4 localAddr = new ip4 { u = item.localAddr };
                int localPort = Functions.FirstNonZero(item.localPort1, item.remotePort2, item.remotePort3, item.remotePort4);

                string CountryCode = string.Empty;

                if (item.remoteAddr != 0 && remoteAddr.b1 != 127 && (!(remoteAddr.b1 == 192 && remoteAddr.b2 == 168)))
                {
                    IPAddress remoteIpa = new IPAddress(new byte[] { remoteAddr.b1, remoteAddr.b2, remoteAddr.b3, remoteAddr.b4 });

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

                    Connection connection = new Connection(remoteIpa, localIpa, item.owningPid);

                    //string localData = string.Format("{0}.{1}.{2}.{3}:{4}", localAddr.b1, localAddr.b2, localAddr.b3, localAddr.b4, localPort);

                    //ListViewItem lvi = new ListViewItem(remoteData);

                    //lvi.SubItems.Add(localData);
                    //lvi.SubItems.Add(item.owningPid.ToString());

                    //Connection connection = new Connection(remoteIpa, localIpa, null, null, null, null);
                    //connections.Add(connection);

                    //lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInProcessName(lvi, item.owningPid, connection); });

                    //lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInGeoLocation(lvi, remoteIpa); });

                    //lvi.SubItems.Add(string.Empty);

                    //lvi.SubItems.Add(string.Empty);

                    //Task.Run(() => { FillInHostName(lvi, remoteIpa); });
                    //Task.Run(() => { FillInArinInfo(lvi, remoteIpa); });

                    //lvMain.Items.Add(lvi);
                }
            }
        }

        public Connections(long version)
        {
            Version = version;
        }

        public long Version { get; private set; }

        public int IndexOf(Connection item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (list[i].GetHashCode() == item.GetHashCode())
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, Connection item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Connection this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public void Add(Connection item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Connection item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (list[i].GetHashCode() == item.GetHashCode())
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Connection[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Connection item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (list[i].GetHashCode() == item.GetHashCode())
                {
                    list.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<Connection> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
