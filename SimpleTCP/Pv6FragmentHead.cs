using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace SimpleTCP
{
    class Pv6FragmentHead
    {
        byte fragmentNextHeader;
        byte fragmentReserved;
        ushort fragmentOffset;
        uint fragmentID;
        public static int pv6FragHeaderLength = 8;

        public Pv6FragmentHead()
        {
            fragmentNextHeader = 0;
            fragmentReserved = 0;
            fragmentOffset = 0;
            fragmentID = 0;
        }

        public byte NextHeader
        {
            get
            {
                return fragmentNextHeader;
            }
            private set
            {
                fragmentNextHeader = value;
            }
        }
        public byte Reserved
        {
            get
            {
                return fragmentReserved;
            }
            private set
            {
                fragmentReserved = value;
            }
        }
        public ushort Offset
        {
            get
            {
                return (ushort)IPAddress.HostToNetworkOrder((short)fragmentOffset);
            }
            private set
            {
                fragmentOffset = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public uint ID
        {
            get
            {
                return (uint)IPAddress.NetworkToHostOrder((int)fragmentID);
            }
            private set
            {
                fragmentID = (uint)IPAddress.HostToNetworkOrder((int)value);
            }
        }
    }
}
