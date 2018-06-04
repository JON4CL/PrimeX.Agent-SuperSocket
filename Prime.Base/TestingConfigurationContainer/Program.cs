using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Prime.Base.ConfigurationContainer;
using System.Reflection;

namespace ConfigurationContainerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, string> data = new Dictionary<string, string>();
            //data.Add("Item1", "valor1");
            //data.Add("Item2", "valor2");
            //Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            ////Console.WriteLine(Directory.GetCurrentDirectory());

            ConfigurationModule mod = ConfigurationContainer.Instance.GetConfigurationByModule("ConfigurationBase");
            string value1 = mod.GetValue("X", "0.5");
            string value2 = mod.GetValue("Y", "0.7");

            Console.WriteLine(Assembly.GetExecutingAssembly().CodeBase);
            Console.WriteLine(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            Console.ReadKey();
        }
    }
}
