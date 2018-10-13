using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Core
{
    internal delegate void general(dynamic Context); 
    public abstract class BasePlugin : IPlugin
    {
        public string Description
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public PluginType Type
        {
            get;
            set;
        }
        public IConfiguration Configuration
        {
            get;
            protected set;
        }
        public bool IsConfigurationLoaded
        {
            get;
            protected set;
        }
        public string Version
        {
            get;
        }
        event general OnLoad;
        event general OnRun;
        public BasePlugin(string name,string description,PluginType type,string version)
        {
            Name = name;
            Description = description;
            Type = type;
            Version = version;
            OnLoad += BasePlugin_OnLoad;
            OnRun += BasePlugin_OnRun;   
        }
        public abstract void BasePlugin_OnRun(dynamic Context);
        public abstract void BasePlugin_OnLoad(dynamic Context);
        public virtual bool Load(IConfiguration configuration)
        {
            OnLoad?.Invoke(configuration);
            return true;
        }
        public virtual bool Run()
        {
            OnRun?.Invoke(null);
            return true;
        }

    }
}
