using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimpleTCP
{
    class Icmpv6Head : ProtocolHead
    {
        private byte icmpType;
        private byte icmpCode;
        private ushort icmpChecksum;
        public Icmpv6Head ipv6Head;

        public static byte icmpv6EchoRequestType = 128;
        public static byte icmpv6EchoRequestCode = 0;
        public static byte icmpv6EchoReplyType = 129;
        public static byte icmpv6EchoReplyCode = 0;
        public static int icmpv6HeadLength = 4;

        public Icmpv6Head() : base()
        {
            icmpType = 0;
            icmpCode = 0;
            icmpChecksum = 0;
        }

        public Icmpv6Head (Icmpv6Head packetHead) : base(){
            icmpType = 0;
            icmpCode = 0;
            icmpChecksum = 0;
            ipv6Head = packetHead;
        }
        public byte Code
        {
            get
            {
                return icmpCode;
            }
            set
            {
                icmpCode = value;
            }
        }
        public ushort CheckSum
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)icmpChecksum);
            }
            set
            {
                icmpChecksum = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public static Icmpv6Head createPacket (byte[] icmpv6Packet, ref int bytesCopied)
        {
            Icmpv6Head icpv6Head = new Icmpv6Head();
            int offset = 0;

            if (icmpv6Packet.Length < Icmpv6Head.icmpv6HeadLength)
                return null;
            icpv6Head.icmpType = icmpv6Packet[offset++];
            icpv6Head.icmpCode = icmpv6Packet[offset++];
            icpv6Head.icmpChecksum = BitConverter.ToUInt16(icmpv6Packet,offset);
            bytesCopied = Icmp4Head.icmpHeadLength;

            return icpv6Head;
        }
        public override byte[] getPacketBytes(byte[] payLoad)
        {
            throw new NotImplementedException();
        }
    }
}
