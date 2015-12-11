using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace SimpleTCP
{
    abstract class ProtocolHead
    {
        public abstract byte[] getPacketBytes(byte[] payLoad);
        public byte[] buildPacket(ArrayList headerList, byte[] payLoad)
        {
            ProtocolHead protocolHead;
            byte[] newPayload = null;
            for(int i = headerList.Count - 1; i >= 0; i--)
            {
                protocolHead = (ProtocolHead)headerList[i];
                newPayload = protocolHead.getPacketBytes(payLoad);
                payLoad = newPayload;
            }
            return payLoad;
        }

        public static ushort computeCheckSum(byte[] payLoad)
        {
            uint sum = 0;
            ushort shortValue = 0;
            ushort highWord = 0;
            ushort lowWord = 0;

            // sums up the 16 bits
            for (int i = 0; i < payLoad.Length / 2; i++)
            {
                highWord = (ushort)(payLoad[i * 2] << 8);
                lowWord = (ushort)payLoad[(i * 2) + 1];
                shortValue = (ushort)(highWord | lowWord);
                sum = sum + (uint)shortValue;
            }
            // padding if needed
            if((payLoad.Length % 2)!= 0)
            {
                sum += (uint)payLoad[payLoad.Length - 1];
            }
            sum = ((sum >> 16)+(sum & 0xFFFF));
            sum = (sum + (sum >> 16));
            shortValue = (ushort)(~sum);
            return shortValue;
        }
        public static void printByteArray (byte[] printBytes)
        {
            int index = 0;
            while(index < printBytes.Length)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (index >= printBytes.Length)
                        break;
                    for(int r = 0; r < 4; r++)
                    {
                        if (index >= printBytes.Length)
                            break;
                        Console.Write("{0}", printBytes[index++].ToString("x2"));
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }
    }
}
