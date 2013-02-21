using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSoft.Extensibility.Mvc.Client
{
	public abstract class PluginInit
	{
		public PluginInit()
		{
		}

		public virtual void Load()
		{
		}

		public virtual void AddServices()
		{
		}

		public virtual void RegisterRoutes()
		{
		}
	}
}
