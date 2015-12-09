using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTCP
{
    class Header
    {
        //public class ipHeader
        //{
        //    Version;
        //    IHL;
        //    serviceType;
        //    totalLength;
        //    ID;
        //    flags;
        //    fragmentOffset;
        //    timeToLive;
        //    headerCheckSum;
        //    sourceAddress;
        //    destinationAddress;
        //    options;
        //    padding;
        //}

        public class tcpHeader
        {
            BitArray sourcePort = new BitArray(16);
            BitArray destinationPort = new BitArray(16);
            BitArray sequenceNumber = new BitArray(32);
            BitArray acknowledgmentNumber = new BitArray(32);
            BitArray dataOffset = new BitArray(4);
            BitArray reserved = new BitArray(6);
            //controlBits
            bool URG;
            bool ACK;
            bool PSH;
            bool RST;
            bool SYN;
            bool FIN;
            ushort window;
            int checkSum;
            int urgentPoint;
            tcpOptions;
            padding;
            tcpData;
            
        }

        public static void packetConstructor()
        {

        }

    }
}
