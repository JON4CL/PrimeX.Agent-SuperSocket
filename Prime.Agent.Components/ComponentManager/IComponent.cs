using System;

namespace PrimeX.Agent.Component
{
    public interface IComponent
    {
        String GetName(); //8CHARS
        String GetDescription();
        String GetVersion();
        UInt16 GetCommandCode(); //

        void Start();
        void Stop();

        event ComponentEventHandler OnNewComponentEvent;
    }
}