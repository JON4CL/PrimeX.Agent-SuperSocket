using PrimeX.Agent.Component;
using PrimeX.Agent.Networking;
using PrimeX.Base.ConfigurationContainer;
using PrimeX.Base.Log;
using PrimeX.Base.Networking;
using PrimeX.Base.MachineInfo;

namespace PrimeX.Agent
{
    public class PrimeAgent
    {
        private ConfigurationModule ConfigModule;
        private CommunicationManager CommManager;
        private ComponentManager CompManager;

        public PrimeAgent()
        {
            Logger.LogInfo(this, "PrimeAgent()");

            ConfigModule = ConfigurationContainer.Instance.GetConfigurationByModule("PrimeAgent");

            CommManager = new CommunicationManager();
            CompManager = new ComponentManager();
            CompManager.OnNewComponentManagerEvent += OnNewComponentManagerEvent;

            CommManager.Init();
            CompManager.Init();
        }

        private void OnNewComponentManagerEvent(ComponentManagerEventArgs e)
        {
            Logger.LogInfo(this, "OnNewComponentManagerEvent()");
            string machineIp = MachineInfo.GetIP(false);

            Logger.LogDebug(this, "machineIp:{0}", machineIp);
            CommMessage msg = new CommMessage(  machineIp, 
                                                e.Component.GetCommandCode(), 
                                                e.ComponentArgs.IsBinary, 
                                                e.Component.GetName(), 
                                                e.ComponentArgs.Timestamp, 
                                                e.ComponentArgs.Size, 
                                                e.ComponentArgs.Data );
            CommManager.AppendMessage(msg);
        }

        public bool Start()
        {
            Logger.LogInfo(this, "Start()");
            CommManager.Start();
            CompManager.Start();

            return true;
        }

        public bool Stop()
        {
            Logger.LogInfo(this, "Stop()");
            CommManager.Stop();
            CompManager.Stop();

            return true;
        }
    }
}
