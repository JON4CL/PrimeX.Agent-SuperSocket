using System;
using System.Text;

namespace PrimeX.Base.Tools
{
    public static class ByteOperations
    {
        public static byte[] EncodeStringNumberToByteArray(string data)
        {
            if (data.Length % 2 != 0)
            {
                data = "0" + data;
            }
            byte[] returnData = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i = i + 2)
            {
                if (int.TryParse(data.Substring(i, 2), out int num))
                {
                    returnData[i / 2] = (byte)num;
                }
                else
                {
                    throw new Exception("Error to try to parse data.");
                }
            }
            return returnData;
        }

        public static string DecodeByteArrayToStringNumber(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            if (data.Length == 0)
            {
                sb.Append("");
            }
            foreach (int num in data)
            {
                if (num < 10)
                {
                    sb.Append("0");
                }
                sb.Append(num.ToString());
            }

            return sb.ToString();
        }
    }
}
