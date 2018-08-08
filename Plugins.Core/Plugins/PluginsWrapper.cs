using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Core
{
    public class PluginsWrapper
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public PluginType Type { get; set; }
    }
}
