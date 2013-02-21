using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSoft.Extensibility.Mvc.Client
{
	public interface IPluginLoaderService
	{
		void Load(PluginInfo[] pluginInfos);
	}
}
