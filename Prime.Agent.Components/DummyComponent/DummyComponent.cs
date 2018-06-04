using System;

namespace PrimeX.Agent.Component
{
    public class DummyComponent : IComponent
    {
        public event ComponentEventHandler OnNewComponentEvent;
        
        public void Start()
        {
            OnNewComponentEvent(this, new ComponentEventArgs("Start()"));
        }

        public void Stop()
        {
            OnNewComponentEvent(this, new ComponentEventArgs("Stop()"));
        }

        UInt16 IComponent.GetCommandCode()
        {
            return 0;
        }

        string IComponent.GetDescription()
        {
            return "Description";
        }

        string IComponent.GetName()
        {
            return "Name";
        }

        string IComponent.GetVersion()
        {
            return "Version";
        }
    }
}
