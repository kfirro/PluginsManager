using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugins.Core;

namespace PluginsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginsManager pManager = PluginsManager.Instance;
            Console.WriteLine($"Current plugins count: {pManager.PluginCount}");
            Console.WriteLine($"HasType DataLoader? Answer: {pManager.HasType(PluginType.DataLoader)}");
            Console.WriteLine($"HasPlugin DataLoader? Answer: {pManager.HasType(PluginType.DataLoader)}");
            var config = new CsvFileConfiguration(@"C:\temp\config.txt");
            Console.WriteLine($"Running plugin TestPlugin, result is: {pManager.RunPlugin(PluginType.DataLoader, "TestPlugin",config)}");
            Console.WriteLine($"Printing current available plugins");
            foreach (var plugin in pManager.GetPlugins())
            {
                Console.WriteLine($"Name: {plugin.Name}, Description: {plugin.Description}, Version: {plugin.Version}, Type: {plugin.Type}");
            }
            Console.ReadKey();
        }
    }
}
