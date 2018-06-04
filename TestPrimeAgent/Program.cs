using PrimeX.Agent;
using PrimeX.Base.MachineInfo;
using PrimeX.Base.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestPrimeAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            //String aIP = Convert.ToString(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));
            //String bIP = string.Join(".", Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork).GetAddressBytes().Select(a => a.ToString("d3")));
            //Console.WriteLine("a: {0}", aIP);
            //Console.WriteLine("b: {0}", bIP);

            //Console.WriteLine("b: {0}", ByteOperations.EncodeStringNumberToByteArray(MachineInfo.GetIP(false)));

            PrimeAgent pa = new PrimeAgent();
            pa.Start();
            Console.WriteLine("PRIME AGENT STARTED");

            //UInt16 num = 80;
            //byte[] bytes = BitConverter.GetBytes(num);
            //Console.WriteLine("byte array: " + BitConverter.ToString(bytes));
            //Console.WriteLine("Num:" + BitConverter.ToUInt16(bytes, 0));

            //byte[] val = ConvertNumberToByteArray("201710091223341234");
            //Console.WriteLine("byte array: " + BitConverter.ToString(val));
            //Console.WriteLine("string: " + DecodeByteArrayToStringNumber(val));

            //val = ConvertNumberToByteArray("001710091223341234");
            //Console.WriteLine("byte array: " + BitConverter.ToString(val));
            //Console.WriteLine("string: " + DecodeByteArrayToStringNumber(val));

            ////byte[] file = File.ReadAllBytes(AppDomain.CurrentDomain.BaseDirectory + "EVT\\201709281550223402.BIN");

            Console.ReadKey();

            pa.Stop();
        }

        //public static string DecodeByteArrayToStringNumber(byte[] data)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    if (data.Length == 0)
        //    {
        //        sb.Append("");
        //    }
        //    foreach (int num in data)
        //    {
        //        if (num < 10)
        //        {
        //            sb.Append("0");
        //        }
        //        sb.Append(num.ToString());
        //    }

        //    return sb.ToString();
        //}

        //public static byte[] ConvertNumberToByteArray(string data)
        //{
        //    //Validate if the string has pair values
        //    //If not append a 0 to the beggining
        //    if (data.Length % 2 != 0)
        //    {
        //        data = "0" + data;
        //    }
        //    byte[] returnData = new byte[data.Length / 2];
        //    for (int i = 0; i < data.Length; i = i + 2)
        //    {
        //        if (int.TryParse(data.Substring(i, 2), out int num))
        //        {
        //            returnData[i / 2] = (byte)num;
        //        }
        //        else
        //        {
        //            throw new Exception("Error to try to parse data.");
        //        }
        //    }
        //    return returnData;
        //}
    }
}
