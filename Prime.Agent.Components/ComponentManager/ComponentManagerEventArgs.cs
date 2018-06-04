using System;
using System.Text;

namespace PrimeX.Agent.Component
{
    public class ComponentManagerEventArgs : EventArgs
    {
        public IComponent Component;
        public ComponentEventArgs ComponentArgs;

        public ComponentManagerEventArgs(IComponent component, ComponentEventArgs componentEventArgs)
        {
            Component = component;
            ComponentArgs = componentEventArgs;
        }
    }
}