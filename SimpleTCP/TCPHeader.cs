using System;
using System.Collections;
using System.Net;

namespace SimpleTCP
{
    public class TCPHeader
    {
        public class tcpHeader
        {
            public int sourcePort { get; private set; }
            public int destinationPort { get; private set; }
            public int sequencePort { get; private set; }
            public int ackNumb { get; private set; }
            public int dataOffset { get; private set; }
            public int window { get; private set; }
            public int checkSum { get; private set; }
            public int urgentPointer { get; private set; }
            public bool[] reserved { get; private set; }
            public bool[] flags { get; private set; }         

            public tcpHeader(int sourcePort, int destinationPort, int sequencePort, int ackNumb, int dataOffset, bool[] reserved, bool[] flags, int window, int checkSum, int urgentPointer)
            {
                BitArray _sourcePort = new BitArray(BitConverter.GetBytes(sourcePort)); //16
                BitArray _destinationPort = new BitArray(BitConverter.GetBytes(destinationPort)); //16
                BitArray _sequencePort = new BitArray(BitConverter.GetBytes(sequencePort)); //32
                BitArray _ackknowledgmentNumber = new BitArray(BitConverter.GetBytes(ackNumb)); //32
                BitArray _dataOffset = new BitArray(BitConverter.GetBytes(dataOffset)); //4
                BitArray _reserved = new BitArray(new bool[]{ false, false, false }); //3
                BitArray flagArgs = new BitArray(new bool[] { false, false, false, false, false, false, false, false, false }); //9
                BitArray _window = new BitArray(BitConverter.GetBytes(window)); //16
                BitArray _checkSum = new BitArray(BitConverter.GetBytes(checkSum)); //16
                BitArray _urgentPointer = new BitArray(BitConverter.GetBytes(urgentPointer)); //16
            }

            
        }
        


    }
}
