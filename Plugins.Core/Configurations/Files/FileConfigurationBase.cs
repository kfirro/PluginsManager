using System;
using System.Collections.Generic;
using System.IO;

namespace Plugins.Core
{
    public abstract class FileConfigurationBase : IConfiguration
    {
        public ConfigurationBased Type
        {
            get
            {
                return ConfigurationBased.File;
            }
        }
        public string FilePath { get; protected set; }
        public Dictionary<string,object> KVP { get; protected set; }
        public FileConfigurationBase(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Configuration file wasn't found!", FilePath);
            FilePath = filePath;
        }
        public abstract bool LoadConfiguration();
    }
}
