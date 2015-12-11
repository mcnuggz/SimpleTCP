using System;
using System.Net;

namespace SimpleTCP
{
    class socketConverter
    {
        static public byte[] socketByte (IPEndPoint endPoint)
        {
            SocketAddress sockAddress = endPoint.Serialize();
            byte[] sockBytes;
            sockBytes = new byte[sockAddress.Size];

            for(int i = 0; i < sockAddress.Size; i++)
            {
                sockBytes[i] = sockAddress[i];
            }
            return sockBytes;
        }

        public static IPEndPoint getEndPoint(byte[] sockBytes)
        {
            IPEndPoint unpackedEndPoint = null;
            IPAddress UnpackedAddress;
            ushort addressFam;
            ushort unpackedPort;

            addressFam = BitConverter.ToUInt16(sockBytes, 0);
            if(addressFam == 2)
            {
                byte[] addressBytes = new byte[4];
                unpackedPort = BitConverter.ToUInt16(sockBytes, 2);
                UnpackedAddress = new IPAddress(BitConverter.ToUInt32(sockBytes, 4));
                unpackedEndPoint = new IPEndPoint(UnpackedAddress, unpackedPort);
            }
            else if (addressFam == 23)
            {
                byte[] addressBytes = new byte[16];
                unpackedPort = BitConverter.ToUInt16(sockBytes, 2);
                Array.Copy(sockBytes, 8, addressBytes, 0, 16);
                UnpackedAddress = new IPAddress(addressBytes);
                unpackedEndPoint = new IPEndPoint(UnpackedAddress, unpackedPort);
            }
            else
            {
                Console.WriteLine("GetEndPoint: Unknown address family: {0}", addressFam);
            }
            return unpackedEndPoint;
        }
    }
}
