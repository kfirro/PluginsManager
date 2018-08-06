using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugins.Core;

namespace Plugins.Shared
{
    public class TestPlugin : BasePlugin
    {
        public TestPlugin() : base("TestPlugin", "TestPlugin", PluginType.DataLoader)
        {
        }
        public override bool Load()
        {
            Console.WriteLine("TestPlugin Loading!");
            return true;
        }
        public override bool Run()
        {
            Console.WriteLine("TestPlugin Running!");
            return true;
        }
    }
}
