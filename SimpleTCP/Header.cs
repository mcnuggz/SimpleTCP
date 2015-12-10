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
        public class tcpHeader
        {
            public tcpHeader(int sourcePort, int destinationPort, int sequencePort, int ackNumb, int dataOffset, bool[] reserved, int window, int checkSum, int urgentPointer)
            {
                BitArray _sourcePort = new BitArray(BitConverter.GetBytes(sourcePort));
                BitArray _destinationPort = new BitArray(BitConverter.GetBytes(destinationPort));
                BitArray _sequencePort = new BitArray(BitConverter.GetBytes(sequencePort));
                BitArray _ackknowledgmentNumber = new BitArray(BitConverter.GetBytes(ackNumb));
                BitArray _dataOffset = new BitArray(BitConverter.GetBytes(dataOffset));
                  bool[] _reserved = new bool[] { false, false, false };
                BitArray _window = new BitArray(BitConverter.GetBytes(window));
                BitArray _checkSum = new BitArray(BitConverter.GetBytes(checkSum));
                BitArray _urgentPointer = new BitArray(BitConverter.GetBytes(urgentPointer));

            bool URG;
            bool ACK;
            bool PSH;
            bool RST;
            bool SYN;
            bool FIN;
            

            }

            
        }

        public static void packetConstructor()
        {

        }

    }
}
