using System;
using System.Text;

namespace PrimeX.Agent.Component
{
    public class ComponentEventArgs : EventArgs
    {
        public bool IsBinary { get; set; }
        public uint Size { get; set; }
        public byte[] Data { get; set; }
        public String Timestamp { get; set; }
        
        private void SetVariables(byte[] data, String timestamp, bool isBinary)
        {
            if (data.Length == 0)
            {
                Data = new byte[] { };
                Size = 0;
            }
            else
            {
                Data = new byte[data.Length];
                data.CopyTo(Data, 0);
                Size = (uint)data.Length;
            }
            if (String.IsNullOrWhiteSpace(timestamp))
            {
                Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            }
            else
            {
                Timestamp = timestamp;
            }
            IsBinary = isBinary;
        }

        public ComponentEventArgs(String data, String timestamp)
        {
            SetVariables(Encoding.Unicode.GetBytes(data.ToCharArray()), timestamp, false);
        }

        public ComponentEventArgs(String data, DateTime timestamp)
        {
            SetVariables(Encoding.Unicode.GetBytes(data.ToCharArray()), timestamp.ToString("yyyyMMddHHmmssffff"), false);
        }

        public ComponentEventArgs(String data)
        {
            SetVariables(Encoding.Unicode.GetBytes(data.ToCharArray()), DateTime.Now.ToString("yyyyMMddHHmmssffff"), false);
        }

        public ComponentEventArgs(char[] data, String timestamp)
        {
            SetVariables(Encoding.Unicode.GetBytes(data), timestamp, false);
        }

        public ComponentEventArgs(char[] data, DateTime timestamp)
        {
            SetVariables(Encoding.Unicode.GetBytes(data), timestamp.ToString("yyyyMMddHHmmssffff"), false);
        }

        public ComponentEventArgs(char[] data)
        {
            SetVariables(Encoding.Unicode.GetBytes(data), DateTime.Now.ToString("yyyyMMddHHmmssffff"), false);
        }

        public ComponentEventArgs(byte[] data, String timestamp)
        {
            SetVariables(data, timestamp, true);
        }

        public ComponentEventArgs(byte[] data, DateTime timestamp)
        {
            SetVariables(data, timestamp.ToString("yyyyMMddHHmmssffff"), true);
        }

        public ComponentEventArgs(byte[] data)
        {
            SetVariables(data, DateTime.Now.ToString("yyyyMMddHHmmssffff"), true);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("timestamp:'{0}'\0", Timestamp);
            if (!IsBinary)
            {
                sb.AppendFormat("data:'{0}'\n", Encoding.Unicode.GetString(Data));
            }
            else
            {
                sb = new StringBuilder("new byte[] { \n");
                sb.Append(BitConverter.ToString(Data));
                sb.Append("}");
            }
            return base.ToString();
        }
    }
}