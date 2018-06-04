using PrimeX.Base.Log;
using System;
using System.IO;
using PrimeX.Base.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PrimeX.Agent.Networking
{
    public class CommunicationManager
    {
        private String EventFilePath = AppDomain.CurrentDomain.BaseDirectory + "EVT\\";
        private FileWatcher FileWatcherMonitor;
        private MessageSender MessageProcessor;

        public CommunicationManager()
        {
            Logger.LogInfo(this, "CommunicationManager()");

            if (!Directory.Exists(EventFilePath))
            {
                Directory.CreateDirectory(EventFilePath);
            }

            FileWatcherMonitor = new FileWatcher();
            FileWatcherMonitor.OnNewFileWatcherEvent += OnNewFileWatcherEvent;

            MessageProcessor = new MessageSender();
        }

        private async Task PutTaskDelay()
        {
            await Task.Delay(5000);
        }

        //private async void OnNewFileWatcherEvent(FileWatcherEventArgs e)
        private void OnNewFileWatcherEvent(FileWatcherEventArgs e)
        {
            Logger.LogInfo(this, "OnNewFileWatcherEvent() filesCount:'{0}'", e.GetFileNameList().Count);
            
            foreach (String fileName in e.GetFileNameList())
            {
                byte[] msg = File.ReadAllBytes(fileName);
                CommMessage commMessage = new CommMessage(msg);
                Console.WriteLine("-> Request FileName:{0}, data:{1}", fileName, commMessage.GetDataString());
                while (!SendData(msg))
                {
                    Console.WriteLine("SendData fail... retry...");
                    //await PutTaskDelay();
                }
                File.Delete(fileName);
            }
            e.Resume();
        }

        private bool SendData(byte[] msg)
        {
            Logger.LogInfo(this, "SendData() msg:'{0}'", BitConverter.ToString(msg));

            //SEND HTTP REQUEST
            return MessageProcessor.SendMessage(msg);

            //throw new NotImplementedException();
        }

        public void Init()
        {
            Logger.LogInfo(this, "Init()");
        }

        public void Start()
        {
            Logger.LogInfo(this, "Init()");

            FileWatcherMonitor.Start();
        }

        public void Stop()
        {
            Logger.LogInfo(this, "Stop()");
            //throw new NotImplementedException();
            FileWatcherMonitor.Stop();
        }

        public void AppendMessage(CommMessage msg)
        {
            Logger.LogInfo(this, "AppendMessage() msg:'{0}'", msg.GetComponentName());

            byte[] writeByte = msg.ToByteArray();
            string fileName = EventFilePath + msg.GetMessageTimestamp();
            Logger.LogDebug(this, "Writing '{0}' bytes in file '{1}'", writeByte.Length, fileName);
            File.WriteAllBytes(fileName + ".TMP", writeByte);
            Logger.LogDebug(this, "rename temp file '{0}' to '{1}'", fileName + ".TMP", fileName + ".BIN");
            File.Move(fileName + ".TMP", fileName + ".BIN");
        }
    }
}
