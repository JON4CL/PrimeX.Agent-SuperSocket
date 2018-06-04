using System.Collections.Generic;

namespace PrimeX.Agent.Networking
{
    public class FileWatcherEventArgs
    {
        private static FileWatcher FileWatcherInstance;

        public FileWatcherEventArgs(FileWatcher fileWatcher)
        {
            FileWatcherInstance = fileWatcher;
        }

        public List<string> GetFileNameList()
        {
            return FileWatcherInstance.FilesInFolder;
        }

        public void Resume()
        {
            FileWatcherInstance.Start();
        }
    }
}