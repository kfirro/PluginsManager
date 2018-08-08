using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Core
{
    public enum PluginType { DataLoader, FormLoader }
    public interface IPlugin
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; }
        PluginType Type { get; set; }
        bool Load(IConfiguration configuration);
        bool Run();
    }
    public static class IPluginExtensions
    {
        public static PluginsWrapper ToPluginsWrapper(this IPlugin plugin)
        {
            return new PluginsWrapper()
            {
                Name = plugin.Name,
                Description = plugin.Description,
                Type = plugin.Type,
                Version = plugin.Version
            };
        }
    }
}
