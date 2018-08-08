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
        PluginType Type { get; set; }
        bool Load(IConfiguration configuration);
        bool Run();
    }
}
