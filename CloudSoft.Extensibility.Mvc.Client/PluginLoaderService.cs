using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSoft.Extensibility.Mvc.Client
{
	public class PluginLoaderService : IPluginLoaderService
	{
		public PluginLoaderService()
		{
		}

		#region IPluginService Members

		public void Load(PluginInfo[] plugins)
		{
			var builderList = new List<PluginBuilder>();

			// 1 - Load all services before initialize
			foreach (var plugin in plugins)
			{
				var asm = System.Reflection.Assembly.LoadFrom(plugin.AssemblyFile);
				var pluginMetaData = new PluginBuilder(asm);

				pluginMetaData.LoadServices();

				builderList.Add(pluginMetaData);
			}

			// 2 - Initialize plugin and register routes
			foreach (var builder in builderList)
			{
				builder.InitializeModuleClasses();
				builder.RegisterRoutes();
			}
		}

		#endregion

	}
}
