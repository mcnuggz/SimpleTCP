using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimpleTCP
{
    class Icmp4Head : ProtocolHead
    {
        #region values
        byte icmpType;
        byte icmpCode;
        ushort icmpCheckSum;
        ushort icmpID;
        ushort icmpSequence;

        public static byte echoRequestType = 8;
        public static byte echoRequestCode = 0;
        public static byte echoReplyType = 0;
        public static byte echoReplyCode = 0;
        public static int icmpHeadLength = 8;
        #endregion
        public Icmp4Head() : base()
        {
            icmpType = 0;
            icmpCode = 0;
            icmpCheckSum = 0;
            icmpID = 0;
            icmpSequence = 0;
        }
        public byte Type
        {
            get
            {
                return icmpType;
            }
            private set
            {
                icmpType = value;
            }
        }
        public byte Code
        {
            get
            {
                return icmpCode;
            }
            private set
            {
                icmpCode = value;
            }
        }
        public ushort CheckSum
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)icmpCheckSum);
            }
            private set
            {
                icmpCheckSum = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public ushort ID
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)icmpID);
            }
            private set
            {
                icmpID = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public static Icmp4Head CreateIGMP (byte[] icmpPacket, ref int bytesCopied)
        {
            Icmp4Head icmpHead = new Icmp4Head();
            int offset = 0;

            if (icmpPacket.Length < Icmp4Head.icmpHeadLength)
                return null;

            icmpHead.icmpType = icmpPacket[offset++];
            icmpHead.icmpCode = icmpPacket[offset++];
            icmpHead.icmpCheckSum = BitConverter.ToUInt16(icmpPacket, offset);
            offset += 2;
            icmpHead.icmpID = BitConverter.ToUInt16(icmpPacket, offset);
            offset += 2;
            icmpHead.icmpSequence = BitConverter.ToUInt16(icmpPacket, offset);

            bytesCopied = Icmp4Head.icmpHeadLength;
            return icmpHead;
        }
        public override byte[] getPacketBytes(byte[] payLoad)
        {
            byte[] icmpPacket;
            byte[] byteValue;
            int offset = 0;

            icmpPacket = new byte[icmpHeadLength + payLoad.Length];
            icmpPacket[offset++] = icmpType;
            icmpPacket[offset++] = icmpCode;
            icmpPacket[offset++] = 0;

            byteValue = BitConverter.GetBytes(icmpID);
            Array.Copy(byteValue, 0, icmpPacket, offset, byteValue.Length);
            offset += byteValue.Length;

            byteValue = BitConverter.GetBytes(icmpSequence);
            Array.Copy(byteValue, 0, icmpPacket, offset, byteValue.Length);
            offset += byteValue.Length;

            if(payLoad.Length > 0)
            {
                Array.Copy(payLoad, 0, icmpPacket, offset, payLoad.Length);
                offset += payLoad.Length;
            }

            CheckSum = computeCheckSum(icmpPacket);

            byteValue = BitConverter.GetBytes(icmpCheckSum);
            Array.Copy(byteValue, 0, icmpPacket, 2, byteValue.Length);
            return icmpPacket;
        }
    }
}
