using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimpleTCP
{
    class Igmp4Head : ProtocolHead
    {
        byte igmpVersion;
        byte igmpResponseTime;
        ushort igmpCheckSum;
        IPAddress igmpGroupAddress;

        public static int igmpHeadLength = 8;
        public static byte igmpMembershipQuery = 0x11;
        public static byte igmpMembershipReport = 0x12;
        public static byte igmpMembershipReportv2 = 0x16;
        public static byte igmpLeaveGroup = 0x17;
        public static IPAddress AllSystemsAddress = IPAddress.Parse("224.0.0.1");

        public Igmp4Head() : base()
        {
            igmpVersion = igmpMembershipQuery;
            igmpResponseTime = 0;
            igmpCheckSum = 0;
            igmpGroupAddress = IPAddress.Any;
        }

        public byte VersionType
        {
            get
            {
                return igmpVersion;
            }
            private set
            {
                igmpVersion = value;
            }
        }
        public byte MaximumResponseTime
        {
            get
            {
                return igmpResponseTime;
            }
            private set
            {
                igmpResponseTime = value;
            }
        }
        public IPAddress GroupAddress
        {
            get
            {
                return igmpGroupAddress;
            }
            private set
            {
                igmpGroupAddress = value;
            }
        }
        public ushort CheckSum
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)igmpCheckSum);
            }
            private set
            {
                igmpCheckSum = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public override byte[] getPacketBytes(byte[] payLoad)
        {
            byte[] igmpPacket;
            byte[] addressBytes;
            byte[] byteValue;
            int offset = 0;

            igmpPacket = new byte[igmpHeadLength + payLoad.Length];
            igmpPacket[offset++] = igmpVersion;
            igmpPacket[offset++] = igmpResponseTime;
            igmpPacket[offset++] = 0;

            addressBytes = igmpGroupAddress.GetAddressBytes();
            Array.Copy(addressBytes, 0, igmpPacket, offset, addressBytes.Length);
            offset += addressBytes.Length;

            if (payLoad.Length > 0)
            {
                Array.Copy(payLoad, 0, igmpPacket, offset, payLoad.Length);
                offset += payLoad.Length;
            }
            CheckSum = computeCheckSum(igmpPacket);
            byteValue = BitConverter.GetBytes(igmpCheckSum);
            Array.Copy(byteValue, 0, igmpPacket, 2, byteValue.Length);

            return igmpPacket;
        }
    }
}
