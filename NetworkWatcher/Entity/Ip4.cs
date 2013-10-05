using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NetworkWatcher.Entity
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct ip4
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
}
