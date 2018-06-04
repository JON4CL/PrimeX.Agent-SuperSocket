using PrimeX.Base.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PrimeX.Agent.Component
{
    public class ComponentManager
    {
        private readonly List<IComponent> ComponentList;
        public event ComponentManagerEventHandler OnNewComponentManagerEvent;
        
        public ComponentManager()
        {
            Logger.LogInfo(this, "ComponentManager()");
            ComponentList = new List<IComponent>();
            LoadComponents();
        }

        private void LoadComponents()
        {
            Logger.LogInfo(this, "LoadComponents()");
            String componentDirectory = AppDomain.CurrentDomain.BaseDirectory + "component";
            if(!Directory.Exists(componentDirectory))
            {
                Directory.CreateDirectory(componentDirectory);
            }
            List<String> fileNameList = Directory.EnumerateFiles(componentDirectory, "*.dll").ToList();
            foreach (String fileName in fileNameList)
            {
                Logger.LogDebug(this, "File found: {0}", fileName);
                try
                {
                    Logger.LogDebug(this, "Loading assembly from file");
                    Assembly ptrAssembly = Assembly.LoadFile(fileName);
                    foreach (Type item in ptrAssembly.GetTypes())
                    {
                        if (!item.IsClass)
                        {
                            continue;
                        }
                        Logger.LogDebug(this, "Check if it has a valid component sign");
                        if (item.GetInterfaces().Contains(typeof(IComponent)))
                        {
                            IComponent inst = (IComponent)Activator.CreateInstance(item);
                            ComponentList.Add(inst);
                            Logger.LogDebug(this, "Component '{0}' added to list", inst.GetName());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(this, "LoadComponents() Exception found: {0}", ex.ToString());
                }
            }
            Logger.LogDebug(this, "Components Loaded Correctly: {0}", ComponentList.Count());
        }

        public void Start()
        {
            foreach (IComponent component in ComponentList)
            {
                Logger.LogDebug(this, "Starting Component '{0}'", component.GetName());
                component.Start();
            }
        }

        private void OnNewComponentEvent(IComponent sender, ComponentEventArgs e)
        {
            Logger.LogInfo(this, "OnNewComponentEvent() sender:'{0}', e.Size:'{1}'", sender.GetName(), e.Size);
            OnNewComponentManagerEvent?.Invoke(new ComponentManagerEventArgs(sender, e));
        }

        public void Init()
        {
            Logger.LogInfo(this, "Init()");
            foreach (IComponent component in ComponentList)
            {
                Logger.LogDebug(this, "Attached 'OnNewComponentEvent' to Component '{0}'", component.GetName());
                component.OnNewComponentEvent += OnNewComponentEvent;
            }
        }

        public void Stop()
        {
            Logger.LogInfo(this, "Stop()");
            foreach (IComponent component in ComponentList)
            {
                Logger.LogDebug(this, "Stoping Component '{0}'", component.GetName());
                component.Stop();
                Logger.LogDebug(this, "Disattached 'OnNewComponentEvent' to Component '{0}'", component.GetName());
                component.OnNewComponentEvent -= OnNewComponentEvent;
            }
        }
    }
}