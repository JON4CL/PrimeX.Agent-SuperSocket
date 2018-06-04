using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using PrimeX.Base.Log;

namespace PrimeX.Base.ConfigurationContainer
{
    public class ConfigurationModule
    {
        private string moduleFileName;
        private string moduleId;
        private Dictionary<string, string> moduleConfigurations = new Dictionary<string, string>();
        private static readonly object ModuleObjectLock = new object();

        public ConfigurationModule(string fileNameWithPath)
        {
            Logger.LogInfo(this, "ConfigurationModule() fileNameWithPath:'{0}'", fileNameWithPath);

            moduleFileName = fileNameWithPath;
            moduleId = Path.GetFileNameWithoutExtension(fileNameWithPath);

            Logger.LogDebug(this, "Check if file '{0}' exist", fileNameWithPath);
            if (File.Exists(fileNameWithPath))
            {
                Logger.LogDebug(this, "Try to read the file '{0}'", fileNameWithPath);
                moduleConfigurations = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(fileNameWithPath));
            }
            else
            {
                Logger.LogDebug(this, "File '{0}' not exist", fileNameWithPath);
                SaveModule();
            }
        }
        
        public bool SaveModule()
        {
            Logger.LogInfo(this, "SaveModule()");

            var serializer = new JsonSerializer();
            lock (ModuleObjectLock)
            {
                using (var writer = new StreamWriter(moduleFileName))
                {
                    Logger.LogDebug(this, "StreamWriter opened {0}", moduleFileName);
                    var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented };
                    serializer.Serialize(jsonWriter, moduleConfigurations);
                    Logger.LogDebug(this, "StreamWriter saved");
                }
            }
            return true;
        }

        public string GetValue(string keyName, string defaultValue)
        {
            Logger.LogInfo(this, "GetValue() keyName:'{0}', defaultValue:'{1}'", keyName, defaultValue);

            string value = defaultValue;
            Logger.LogDebug(this, "Check if exist the key '{0}'", keyName);
            if (moduleConfigurations.ContainsKey(keyName))
            {
                Logger.LogDebug(this, "Getting the value for the key '{0}'", keyName);
                moduleConfigurations.TryGetValue(keyName, out value);
            }
            else
            {
                Logger.LogDebug(this, "Parameter doesn't exist");
                Logger.LogDebug(this, "Creating parameter '{0}' with value '{1}'", keyName, defaultValue);
                moduleConfigurations.Add(keyName, defaultValue);
                SaveModule();
            }
            return value;
        }

        public string GetValue(string keyName)
        {
            Logger.LogInfo(this, "GetValue() keyName:'{0}'", keyName);
            return GetValue(keyName, "");
        }

        public bool SetValue(string keyName, string keyValue)
        {
            Logger.LogInfo(this, "SetValue() keyName:'{0}', keyValue:{1}", keyName, keyValue);

            Logger.LogDebug(this, "Check if exist the key '{0}'", keyName);
            if (moduleConfigurations.ContainsKey(keyName))
            {
                Logger.LogDebug(this, "Updating the value for the key '{0}'", keyName);
                moduleConfigurations[keyName] = keyValue;
            }
            else
            {
                Logger.LogDebug(this, "Parameter doesn't exist");
                Logger.LogDebug(this, "Creating parameter '{0}' with value '{1}'", keyName, keyValue);
                moduleConfigurations.Add(keyName, keyValue);
            }
            
            return SaveModule();
        }
    }
}
