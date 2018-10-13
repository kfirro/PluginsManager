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
            return Instance.plugins?.Any(p => p.Type.Equals(pluginType)) ?? false;
        }
        public bool IsExists(PluginType pluginType, string pluginName)
        {
            return Instance.plugins?.Any(p => p.Type.Equals(pluginType) && p.Name == pluginName) ?? false;
        }
        public bool ExecutePlugin(PluginType pluginType, string pluginName)
        {
            var plugin = Instance.plugins?.FirstOrDefault(p =>
                p.Type.Equals(pluginType) && p.Name == pluginName);
            if (plugin == null)
                throw new PlatformNotSupportedException($"There's no such plugin name and type! {nameof(pluginName)}: {pluginName}, {nameof(pluginType)}: {pluginType}");
            if(!((BasePlugin)plugin).IsConfigurationLoaded)
                throw new Exception($"Plugin's configuration isn't loaded!");
            return plugin.Run();
        }
        public bool LoadPluginConfiguration(PluginType pluginType, string pluginName, IConfiguration configuration)
        {
            var plugin = Instance.plugins?.FirstOrDefault(p =>
                p.Type.Equals(pluginType) && p.Name == pluginName);
            if (plugin == null)
                throw new PlatformNotSupportedException($"Plugin {pluginName} wasn't loaded!");
            if (((BasePlugin)plugin).IsConfigurationLoaded)
                return true; //Already loaded
            return plugin.Load(configuration);
        }
        public IEnumerable<PluginsWrapper> GetPlugins()
        {
            foreach (IPlugin plugin in Instance.plugins)
                yield return plugin.ToPluginsWrapper();
        }
        private PluginsManager()
        {
            LoadContainer();
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
                    Where(t => typeof(IPlugin).IsAssignableFrom(t)).ToList().
                    ForEach(p =>
                    {
                        IPlugin pluginInstance = (IPlugin)Activator.CreateInstance(p, new object[] { });
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
