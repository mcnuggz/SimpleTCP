using System;
using System.Collections;

namespace SimpleTCP
{
    class Header
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
                BitArray _sourcePort = new BitArray(BitConverter.GetBytes(sourcePort));
                BitArray _destinationPort = new BitArray(BitConverter.GetBytes(destinationPort));
                BitArray _sequencePort = new BitArray(BitConverter.GetBytes(sequencePort));
                BitArray _ackknowledgmentNumber = new BitArray(BitConverter.GetBytes(ackNumb));
                BitArray _dataOffset = new BitArray(BitConverter.GetBytes(dataOffset));
                  bool[] _reserved = new bool[] { false, false, false };
                BitArray _window = new BitArray(BitConverter.GetBytes(window));
                BitArray _checkSum = new BitArray(BitConverter.GetBytes(checkSum));
                BitArray _urgentPointer = new BitArray(BitConverter.GetBytes(urgentPointer));

                //still need to implement the flags
                //if loops for everything?
                //it'll work but it'll be massive

            }

            
        }

        public static void packetConstructor()
        {

        }

    }
}
