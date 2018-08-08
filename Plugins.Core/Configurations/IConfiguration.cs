using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Core
{
    public enum ConfigurationBased { File, JSON, DataBase, Object }
    public interface IConfiguration
    {
        ConfigurationBased Type { get; }
    }
}
