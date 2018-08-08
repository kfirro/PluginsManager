using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity;

namespace Plugins.Core
{
    public class PluginsManager
    {
        List<IPlugin> plugins = null;
        IUnityContainer container = null;
        private static readonly Lazy<PluginsManager> _instance =
            new Lazy<PluginsManager>(() => new PluginsManager());
        public static PluginsManager Instance { get { return _instance.Value; } }
        public int PluginCount { get { return plugins.Count; } }
        public bool HasType(PluginType pluginType)
        {
            return Instance.plugins?.Where(p => p.Type.Equals(pluginType)).Any() ?? false;
        }
        public bool RunPlugin(PluginType pluginType, string pluginName,IConfiguration configuration)
        {
            var plugin = Instance.plugins?.FirstOrDefault(p =>
                p.Type.Equals(pluginType) && p.Name == pluginName);
            if (plugin == null)
                throw new PlatformNotSupportedException($"There's no such plugin name and type! {nameof(pluginName)}: {pluginName}, {nameof(pluginType)}: {pluginType}");
            plugin.Load(configuration);
            return plugin.Run();
        }
        private PluginsManager(bool autoLoad = true)
        {
            LoadContainer();
            if (autoLoad)
                LoadPluginsToContainer();
        }
        private void LoadContainer()
        {
            container = new UnityContainer();
            string[] files = Directory.GetFiles(Settings.Default.PluginsPath, "*.dll");
            foreach (String file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);
                assembly.GetTypes().
                    Where(t=> typeof(IPlugin).IsAssignableFrom(t)).ToList().
                    ForEach(p =>
                    {
                        IPlugin pluginInstance = (IPlugin)Activator.CreateInstance(p, new object[]{  });
                        container.RegisterInstance<IPlugin>(pluginInstance.Name, pluginInstance);
                    });
            }
        }
        private void LoadPluginsToContainer()
        {
            if (container != null)
            {
                var loadedPlugins = container.ResolveAll<IPlugin>();
                plugins = loadedPlugins?.ToList();
                //plugins?.ForEach(p => p.Load());
            }
        }
    }
}
