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
        private const string VERSION = "1.0.0.0";
        private const string PLUGIN_NAME = "TestPlugin";
        private const string PLUGIN_DESCRIPTION = "TestPlugin - first plugin to test the actual plugins manager";
        private const PluginType PLUGIN_TYPE = PluginType.DataLoader;
        public TestPlugin() : base(PLUGIN_NAME, PLUGIN_DESCRIPTION, PLUGIN_TYPE, VERSION)
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
