using System.Collections.Generic;
using System.IO;
using PrimeX.Base.Log;
using System;

namespace PrimeX.Base.ConfigurationContainer
{
    public class ConfigurationContainer
    {
        private static string configFileExtension = ".jconf";
        private static string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string configFilesPath = currentPath + "config";
        private static ConfigurationContainer privateInstance;

        private static Dictionary<string, ConfigurationModule> modules = new Dictionary<string, ConfigurationModule>();

        public static ConfigurationContainer Instance
        {
            get
            {
                if (privateInstance == null)
                {
                    privateInstance = new ConfigurationContainer();
                }
                return privateInstance;
            }
        }

        private ConfigurationContainer()
        {
            Logger.LogInfo(this, "ConfigurationContainer()");

            Logger.LogDebug(this, "Check if 'config' directory exist");
            if (Directory.Exists(configFilesPath))
            {
                Logger.LogDebug(this, "Process all 'jconfig' files in the 'config' folder");
                foreach (string fileNameWithPath in Directory.GetFiles(configFilesPath, "*" + configFileExtension))
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileNameWithPath);
                    Logger.LogDebug(this, "Loading '{0}' Module Configuration", fileName);
                    modules.Add(fileName, new ConfigurationModule(fileNameWithPath));
                }
            }
            else
            {
                Logger.LogError(this, "Configuration directory {0}, not exist.", configFilesPath);
                Logger.LogError(this, "Creating directory {0}.", configFilesPath);
                Directory.CreateDirectory(configFilesPath);
            }
        }

        public ConfigurationModule GetConfigurationByModule(string moduleName)
        {
            Logger.LogInfo(this, "GetConfigurationByModule() moduleName:'{0}'", moduleName);

            ConfigurationModule module;
            Logger.LogDebug(this, "Check id module '{0}' exist", moduleName);
            if (modules.ContainsKey(moduleName))
            {
                Logger.LogDebug(this, "Getting module {0}", moduleName);
                modules.TryGetValue(moduleName, out module);
            }
            else
            {
                Logger.LogError(this, "Module {0}, not exist.", moduleName);
                Logger.LogError(this, "Creating new module.", configFilesPath);
                module = new ConfigurationModule(configFilesPath + Path.DirectorySeparatorChar + moduleName + configFileExtension);
                modules.Add(moduleName, module);
            }
            return module;
        }
    }
}
