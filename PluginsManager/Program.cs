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
            Console.WriteLine($"Current plugin count: {pManager.PluginCount}");
            Console.WriteLine($"HasType DataLoader? Answer: {pManager.HasType(PluginType.DataLoader)}");
            Console.WriteLine($"Running plugin TestPlugin, result is: {pManager.RunPlugin(PluginType.DataLoader, "TestPlugin")}");
            //Should invoke exception (no such plugin)
            //Console.WriteLine($"Running plugin Kfir, result is: {pManager.RunPlugin(PluginType.DataLoader, "Kfir")}");
            Console.ReadKey();
        }
    }
}
