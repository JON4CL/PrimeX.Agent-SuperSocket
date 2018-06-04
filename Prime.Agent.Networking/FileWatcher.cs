using PrimeX.Base.ConfigurationContainer;
using PrimeX.Base.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PrimeX.Agent.Networking
{
    public class FileWatcher
    {
        private ConfigurationModule ConfigModule;
        private Timer FileWatcherTimer;
        private int TimerSleepTime;
        private string FolderToMonitor;

        public List<String> FilesInFolder = new List<string>();

        public event FileWatcherEventHandler OnNewFileWatcherEvent;

        public FileWatcher()
        {
            Logger.LogInfo(this, "FileWatcher()");

            ConfigModule = ConfigurationContainer.Instance.GetConfigurationByModule("FileWatcher");
            int.TryParse(ConfigModule.GetValue("SLEEP_TIME_MS", "1000"), out TimerSleepTime);
            FolderToMonitor = ConfigModule.GetValue("PATH_MONITORED_FOLDER", AppDomain.CurrentDomain.BaseDirectory + "EVT");
        }

        public void Stop()
        {
            //throw new NotImplementedException();
        }

        public FileWatcher(String pathToMonitor, int sleepTime)
        {
            Logger.LogInfo(this, "FileWatcher() pathToMonitor:'{0}' sleepTime:'{1}'", pathToMonitor, sleepTime);

            TimerSleepTime = sleepTime;
            FolderToMonitor = pathToMonitor;
        }

        public void Start()
        {
            Logger.LogInfo(this, "Start()");

            FileWatcherTimer = new Timer(TimerSleepTime)
            {
                AutoReset = false
            };
            FileWatcherTimer.Elapsed += TimerElapsed;
            FileWatcherTimer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Logger.LogInfo(this, "TimerElapsed()");

            //FileWatcherTimer.Stop();

            FilesInFolder = Directory.EnumerateFiles(FolderToMonitor, "*.BIN").ToList();
            FilesInFolder.Sort();
            Logger.LogDebug(this, "FilesInFolder:{0}", FilesInFolder.Count);

            if (FilesInFolder.Count > 0)
            { 
                OnNewFileWatcherEvent?.Invoke(new FileWatcherEventArgs(this));
            }
            else
            {
                FileWatcherTimer.Start();
            }
            //FileWatcherTimer.Start();
        }
    }
}
