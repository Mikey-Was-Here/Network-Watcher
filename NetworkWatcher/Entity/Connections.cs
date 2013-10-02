using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkWatcher.Entity
{
    public class Connections : IList<Connection>
    {
        private List<Connection> list = new List<Connection>();

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
