using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using PrimeX.Base.Tools;

namespace PrimeX.Base.Networking
{
    [Serializable]
    public struct CommMessage
    {
        public byte[] SourceIp;         //4//CHAR12
        public byte[] MessageCommand;   //2//UINT16
        public byte IsBinary;           //1//BOOL
        public byte[] ComponentName;    //8// String(8) "01234567"
        public byte[] DataTimestamp;    //9//String(18) yy yy MM dd HH mm ss ff ff
        public byte[] DataSize;         //4//UINT32 65535 - 28
        public byte[] Data;             //VARIABLE

        public CommMessage( String sourceIP, 
                            UInt16 messageCommand, 
                            Boolean isBinary, 
                            String componentName, 
                            string dataTimestamp, 
                            UInt32 dataSize, 
                            byte[] data )
        {
            SourceIp = ByteOperations.EncodeStringNumberToByteArray(sourceIP);
            MessageCommand = new byte[2];
            IsBinary = new byte();
            ComponentName = new byte[8];
            DataTimestamp = new byte[9];
            DataSize = new byte[4];
            Data = new byte[0];

            MessageCommand = BitConverter.GetBytes(messageCommand);
            IsBinary = Convert.ToByte(isBinary);
            if (componentName.Length > 8)
            {
                componentName = componentName.Substring(0, 8);
            }
            else
            {
                componentName = componentName.PadRight(8, ' ');
            }
            ComponentName = Encoding.Unicode.GetBytes(componentName);
            DataTimestamp = ByteOperations.EncodeStringNumberToByteArray(dataTimestamp);
            DataSize = BitConverter.GetBytes(dataSize);
            Data = new byte[dataSize];
            data.CopyTo(Data, 0);
        }

        public CommMessage(byte[] data)
        {
            MessageCommand = new byte[2];
            IsBinary = new byte();
            ComponentName = new byte[8];
            DataTimestamp = new byte[9];
            DataSize = new byte[4];
            Data = new byte[0];

            this = FromBytes<CommMessage>(data);
        }

        public string GetSourceIP()
        {
            String baseIP = ByteOperations.DecodeByteArrayToStringNumber(SourceIp);
            return (
                baseIP.Substring(0, 3) + "." +
                baseIP.Substring(3, 3) + "." +
                baseIP.Substring(6, 3) + "." +
                baseIP.Substring(9)
            );
        }

        public int GetMessageCommand()
        {
            return Convert.ToInt32(BitConverter.ToUInt16(MessageCommand, 0));
        }

        public String GetMessageTimestamp()
        {
            return ByteOperations.DecodeByteArrayToStringNumber(DataTimestamp);
        }

        public String GetComponentName()
        {
            return Encoding.Unicode.GetString(ComponentName);
        }

        public bool GetIsBinary()
        {
            return Convert.ToBoolean(IsBinary);
        }

        public byte[] ToByteArray()
        {
            return GetBytes(this);
        }

        public UInt32 GetDataSize()
        {
            return BitConverter.ToUInt32(DataSize, 0);
        }

        static byte[] GetBytes(CommMessage str)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, str);
                return stream.ToArray();
            }
        }

        public int GetMsgByteSize()
        {
            return GetBytes(this).Length;
        }

        static CommMessage FromBytes<CommMessage>(byte[] arr)
        {
            using (var stream = new MemoryStream(arr))
            {
                return (CommMessage)new BinaryFormatter().Deserialize(stream);
            }
        }

        public String GetDataString()
        {
            return Encoding.Unicode.GetString(Data);
        }
    }
}
