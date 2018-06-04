using Prime.Base.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            CommMessage test = new CommMessage();
            int sizeTest = test.GetMsgByteSize();

            byte[] data = Encoding.Unicode.GetBytes("DATA ENCODED");
            CommMessage msg = new CommMessage((ushort)1, false, "COMP01", DateTime.Now.ToString("yyyyMMddHHmmssffff"), (uint)data.Length, data);
            byte[] dataParsed = new byte[msg.GetMsgByteSize()];
            msg.ToByteArray().CopyTo(dataParsed, 0);
            File.WriteAllBytes("TESTMSG.TMP", dataParsed);

            byte[] dataReaded = File.ReadAllBytes("TESTMSG.TMP");
            CommMessage msgReaded = new CommMessage(dataReaded);
            Console.WriteLine(msgReaded.GetComponentName());
            Console.WriteLine(msgReaded.GetDataString());

            Console.ReadKey();
        }
    }
}
