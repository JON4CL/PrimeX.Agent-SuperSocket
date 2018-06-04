using Prime.Base.Networking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prime.Backend.Data
{
    public class MessageRecord
    {
        public int Id { get; set; }

        public String SourceIP { get; set; }
        public String ComponentName { get; set; }
        public int MessageCommand { get; set; } //Convert.ToInt32
        public String MessageTimestamp { get; set; }
        public bool IsBinary { get; set; }
        public byte[] DataBinary { get; set; }
        public string DataString { get; set; }

        public MessageRecord(CommMessage msg)
        {
            SourceIP = msg.GetSourceIP();
            ComponentName = msg.GetComponentName();
            MessageCommand = msg.GetMessageCommand();
            MessageTimestamp = msg.GetMessageTimestamp();
            IsBinary = msg.GetIsBinary();
            if (IsBinary)
            {
                DataBinary = new byte[msg.GetDataSize()];
                msg.Data.CopyTo(DataBinary, 0);
            }
            else
            {
                DataString = msg.GetDataString();
            }
        }
    }
}
