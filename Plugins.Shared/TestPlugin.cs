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
        public override bool Load(IConfiguration configuration)
        {
            if (IsConfigurationLoaded)
                return true;
            Console.WriteLine("TestPlugin Loading configuration!");
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration), $"TestPlugin configuration is null!");
            Configuration = configuration;
            var filePath = ((CsvFileConfiguration)Configuration).FilePath;
            ((CsvFileConfiguration)Configuration).LoadConfiguration();
            IsConfigurationLoaded = true;
            return IsConfigurationLoaded;
        }
        public override bool Run()
        {
            Console.WriteLine("TestPlugin Running!");
            var config = Configuration as CsvFileConfiguration;
            foreach (var kvp in config.KVP)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
            return true;
        }
    }
}
