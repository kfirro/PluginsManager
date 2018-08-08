﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Core
{
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

        public BasePlugin(string name,string description,PluginType type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
        public abstract bool Load(IConfiguration configuration);
        public abstract bool Run();
    }
}