using System;
using System.Collections;
using System.Net.Sockets;
using System.Net;

namespace SimpleTCP
{
    class TCPHeader : ProtocolHead
    {
        byte version; //4 bits
        byte length; //4 bits
        byte serviceType; // 8 bits
        ushort totalLength; //16 bits
        ushort _ID; // 16 bits
        ushort dataOffset; // flags and dataOffset = 8 bits

        // 0 = same host
        // 1 = same subnet (LAN or Intranet)
        // 32 = same site
        // 64 = same region
        // 128 = same continent
        // 255 = unrestricted

        byte timeToLive; // 8 bits
        byte protocol; // 8 bits
        ushort checkSum; // 16 bits
        IPAddress sourceAddress;
        IPAddress destinationAddress;
        public static int headerLength = 20;

        public TCPHeader() : base()
        {
            version = 4;
            length = (byte)headerLength;
            serviceType = 0;
            _ID = 0;
            dataOffset = 0;
            timeToLive = 1;
            protocol = 0;
            checkSum = 0;
            sourceAddress = IPAddress.Any;
            destinationAddress = IPAddress.Any;
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
        public byte Length
        {
            get
            {
                return (byte)(length * 4);
            }
            private set
            {
                length = (byte)(value / 4);
            }
        }
        public byte ServiceType
        {
            get
            {
                return serviceType;
            }
            private set
            {
                serviceType = value;
            }
        }
        public ushort TotalLength
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)totalLength);
            }
            private set
            {
                totalLength = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public ushort ID
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)_ID);
            }
            private set
            {
                _ID = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public ushort DataOffset
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)dataOffset);
            }
            private set
            {
                dataOffset = (ushort)IPAddress.HostToNetworkOrder((short)value);
            }
        }
        public byte TimeToLive
        {
            get
            {
                return timeToLive;
            }
            private set
            {
                timeToLive = value;
            }
        }
        public byte Protocol
        {
            get
            {
                return protocol;
            }
            private set
            {
                protocol = value;
            }
        }
        public ushort CheckSum
        {
            get
            {
                return (ushort)IPAddress.NetworkToHostOrder((short)checkSum);
            }
            private set
            {
                checkSum = (ushort)IPAddress.HostToNetworkOrder((short)value);
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
                return destinationAddress;
            }
            private set
            {
                destinationAddress = value;
            }
        }

        public static TCPHeader CreatePacket (byte[] ipv4Packet, ref int bytesCopied)
        {
            TCPHeader pv4Head = new TCPHeader();

            if (ipv4Packet.Length < headerLength)
            {
                return null;
            }

            //>> 4 shifts the bits over to the right 4 times  
            pv4Head.version = (byte)((ipv4Packet[0] >> 4) & 0xF);
            pv4Head.length = (byte)(ipv4Packet[0] & 0xF);
            pv4Head.serviceType = ipv4Packet[1];

            //ToUInt(value, index#)
            pv4Head.totalLength = BitConverter.ToUInt16(ipv4Packet, 2);
            pv4Head._ID = BitConverter.ToUInt16(ipv4Packet, 4);
            pv4Head.dataOffset = BitConverter.ToUInt16(ipv4Packet, 6);
            pv4Head.timeToLive = ipv4Packet[8];
            pv4Head.protocol = ipv4Packet[9];
            pv4Head.checkSum = BitConverter.ToUInt16(ipv4Packet, 10);

            pv4Head.sourceAddress = new IPAddress(BitConverter.ToUInt32(ipv4Packet, 12));
            pv4Head.destinationAddress = new IPAddress(BitConverter.ToUInt32(ipv4Packet, 16));

            bytesCopied = pv4Head.Length;
            return pv4Head;
        }

        public override byte[] getPacketBytes(byte[] payLoad)
        {
            //allocates space for header and payload
            byte[] pv4Packet = new byte[headerLength + payLoad.Length];
            byte[] byteValue;
            int index = 0;  

            // | is an OR operator
            pv4Packet[index++] = (byte)((version << 4) | length);
            pv4Packet[index++] = serviceType;

            byteValue = BitConverter.GetBytes(totalLength);
            Array.Copy(byteValue, 0, pv4Packet, index, byteValue.Length);
            index += byteValue.Length;

            byteValue = BitConverter.GetBytes(ID);
            Array.Copy(byteValue, 0, pv4Packet, index, byteValue.Length);
            index += byteValue.Length;

            byteValue = BitConverter.GetBytes(dataOffset);
            Array.Copy(byteValue, 0, pv4Packet, index, byteValue.Length);
            index += byteValue.Length;

            pv4Packet[index++] = timeToLive;
            pv4Packet[index++] = protocol;
            pv4Packet[index++] = 0; //checkSum

            byteValue = sourceAddress.GetAddressBytes();
            Array.Copy(byteValue, 0, pv4Packet, index, byteValue.Length);
            index += byteValue.Length;

            byteValue = destinationAddress.GetAddressBytes();
            Array.Copy(byteValue, 0, pv4Packet, index, byteValue.Length);
            index += byteValue.Length;

            Array.Copy(payLoad, 0, pv4Packet, index, byteValue.Length);
            index += payLoad.Length;

            //computes checksum throughout entire package
            CheckSum = computeCheckSum(pv4Packet);
            byteValue = BitConverter.GetBytes(checkSum);
            Array.Copy(byteValue, 0, pv4Packet, 10, byteValue.Length);

            return pv4Packet;
        }
    }
}
