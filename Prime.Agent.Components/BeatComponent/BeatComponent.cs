using PrimeX.Base.ConfigurationContainer;
using PrimeX.Base.Log;
using System;
using System.Reflection;
using System.Timers;

namespace PrimeX.Agent.Component.BeatComponent
{
    public class BeatComponent : IComponent
    {
        private ConfigurationModule ConfigModule;

        public event ComponentEventHandler OnNewComponentEvent;

        private Timer ComponentTimer;
        private int TickCount;

        public BeatComponent()
        {
            Logger.LogInfo(this, "BeatComponent()");
            
            ConfigModule = ConfigurationContainer.Instance.GetConfigurationByModule("BeatComponent");
        }

        public void Start()
        {
            Logger.LogInfo(this, "Start()");
            
            int.TryParse(ConfigModule.GetValue("BEAT_TIME_MS", "1000"), out int sleepTime);

            TickCount = 0;
            ComponentTimer = new Timer(sleepTime)
            {
               AutoReset = true
            };
            ComponentTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            ComponentTimer.Start();

            //OnNewComponentEvent(this, new ComponentEventArgs("Start()"));
        }

        public void Stop()
        {
            Logger.LogInfo(this, "Stop()");

            ComponentTimer.Stop();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Logger.LogInfo(this, "TimerElapsed()");

            OnNewComponentEvent?.Invoke(this, new ComponentEventArgs("BEAT" + TickCount.ToString()));

            if (TickCount == int.MaxValue)
            {
                TickCount = 0;
            }
            TickCount++;
            //((Timer)sender).Start();
        }

        public UInt16 GetCommandCode()
        {
            return 1000;
        }

        public string GetDescription()
        {
            return "Generate a beat in the time configured";
        }

        public string GetName()
        {
            return "BEAT";
        }

        public string GetVersion()
        {
            return "1.0.0";
        }
    }
}
