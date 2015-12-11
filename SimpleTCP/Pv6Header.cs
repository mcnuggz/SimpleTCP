using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimpleTCP
{
     class Pv6Header : ProtocolHead
    {
        byte version;
        byte trafficClass;
        uint flow;
        ushort payloadLength;
        byte nextHeader;
        byte hopLimit;
        IPAddress sourceAddress;
        IPAddress destinationAccess;

        public static int pv6HeaderLength = 40;

        public Pv6Header() : base()
        {
            version = 6;
            trafficClass = 0;
            flow = 0;
            payloadLength = 0;
            nextHeader = 0;
            hopLimit = 32;
            sourceAddress = IPAddress.IPv6Any;
            destinationAccess = IPAddress.IPv6Any;
        }
        public byte Version
        {
            get
            {
                return version;
            }
            private set
            {
                version = value;
            }
        }
        public byte TrafficClass
        {
            get
            {
                return trafficClass;
            }
            private set
            {
                trafficClass = value;
            }
        }
        public uint Flow
        {
            get
            {
                return (uint)IPAddress.NetworkToHostOrder((int)Flow);
            }
            private set
            {
                Flow = (uint)IPAddress.HostToNetworkOrder((int)value);
            }
        }
        public ushort PayloadLength
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)payloadLength);
            }
            private set
            {
                payloadLength = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public byte NextHeader
        {
            get
            {
                return nextHeader;
            }
            private set
            {
                nextHeader = value;
            }
        }
        public byte HopLimit
        {
            get
            {
                return hopLimit;
            }
            private set
            {
                hopLimit = value;
            }
        }
        public IPAddress SourceAddress
        {
            get
            {
                return sourceAddress;
            }
            private set
            {
                sourceAddress = value;
            }
        }
        public IPAddress DestinationAddress
        {
            get
            {
                return destinationAccess;
            }
            private set
            {
                destinationAccess = value;
            }
        }


        public static Pv6Header CreateInstance(byte[] pv6Packet, ref int bytesCopied)
        {
            Pv6Header pv6Header = new Pv6Header();
            byte[] addressBytes = new byte[16];
            uint value = 0;
            uint value2 = 0;

            if (pv6Packet.Length < Pv6Header.pv6HeaderLength)
                return null;

            value = pv6Packet[0];
            value = (value >> 4) & 0xF;
            pv6Header.version = (byte)value;

            value = pv6Packet[0];
            value = (value & 0xF) >> 4;
            pv6Header.trafficClass = (byte)(value | (uint)((pv6Packet[1] >> 4) & 0xF));

            value2 = pv6Packet[1];
            value2 = (value2 & 0xF) << 16;
            value = pv6Packet[2];
            value = value << 8;
            pv6Header.flow = value2 | value | pv6Packet[3];

            pv6Header.nextHeader = pv6Packet[4];
            pv6Header.hopLimit = pv6Packet[5];

            Array.Copy(pv6Packet, 6, addressBytes, 0, 16);
            pv6Header.SourceAddress = new IPAddress(addressBytes);
            Array.Copy(pv6Packet, 24, addressBytes, 0, 16);
            pv6Header.DestinationAddress = new IPAddress(addressBytes);

            bytesCopied = Pv6Header.pv6HeaderLength;

            return pv6Header;
        }

        public override byte[] getPacketBytes(byte[] payLoad)
        {
            byte[] byteValue;
            byte[] pv6Packet;
            int offset = 0;

            pv6Packet = new byte[pv6HeaderLength + payLoad.Length];
            pv6Packet[offset++] = (byte)((version << 4) | ((trafficClass >> 4) & 0xF));

            pv6Packet[offset++] = (byte)((uint)((trafficClass << 4) & 0xF0) | ((Flow >> 16) & 0xF));
            pv6Packet[offset++] = (byte)((Flow >> 8) & 0xFF);
            pv6Packet[offset++] = (byte)(Flow & 0xFF);

            Console.WriteLine("Next header = {0}", nextHeader);

            byteValue = BitConverter.GetBytes(payloadLength);
            Array.Copy(byteValue, 0, pv6Packet, offset, byteValue.Length);
            offset += byteValue.Length;

            pv6Packet[offset++] = nextHeader;
            pv6Packet[offset++] = hopLimit;

            byteValue = sourceAddress.GetAddressBytes();
            Array.Copy(byteValue, 0, pv6Packet, offset, byteValue.Length);
            offset += byteValue.Length;

            byteValue = destinationAccess.GetAddressBytes();
            Array.Copy(byteValue, 0, pv6Packet, offset, byteValue.Length);
            offset += byteValue.Length;

            Array.Copy(payLoad, 0, pv6Packet, offset, payLoad.Length);
            return pv6Packet;
        }
    }
}
