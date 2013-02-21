using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace CloudSoft.Extensibility.Mvc.Client
{
	internal class PluginBuilder
	{
		Assembly assembly;
		bool loadedServices = false;
		bool initialized = false;
		bool routesRegistered = false;
		string name = null;

		List<Type> pluginTypes = new List<Type>();
		List<PluginInit> pluginClasses = new List<PluginInit>();

		public PluginBuilder(Assembly assembly)
		{
			this.assembly = assembly;

			foreach (Type type in assembly.GetExportedTypes())
			{
				if (!type.IsAbstract && typeof(PluginInit).IsAssignableFrom(type))
				{
					pluginTypes.Add(type);
				}
			}
		}

		public string Name
		{
			get
			{
				if (name == null)
					name = assembly.FullName;

				return name;
			}
			set { name = value; }
		}

		public void LoadServices()
		{
			if (loadedServices)
				return;

			loadedServices = true;
			EnsurePluginClassesExist();
			foreach (var pluginClass in pluginClasses)
			{
				pluginClass.AddServices();
			}
		}

		public void InitializeModuleClasses()
		{
			if (initialized)
				return;

			initialized = true;
			EnsurePluginClassesExist();

			foreach (var plugin in pluginClasses)
			{
				plugin.Load();
			}
		}

		public void RegisterRoutes()
		{
			if (routesRegistered)
			{
				return;
			}
			routesRegistered = true;
			foreach (PluginInit plugin in pluginClasses)
			{
				plugin.RegisterRoutes();
			}
		}

		private void EnsurePluginClassesExist()
		{
			if (pluginClasses.Count == pluginTypes.Count)
			{
				return;
			}

			foreach (Type pluginType in pluginTypes)
			{
				var plugin = Activator.CreateInstance(pluginType) as PluginInit;
				pluginClasses.Add(plugin);
			}
		}
	}
}
