using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSoft.Extensibility.Mvc.Client
{
	public class XmlConfigPluginEnumerator : IPluginEnumerator
	{
		private string m_ConfigFile;
		private string m_BinPath;

		public XmlConfigPluginEnumerator(string configFile, string binPath)
		{
			m_ConfigFile = configFile;
			m_BinPath = binPath;
		}

		#region IPluginEnumerator Members

		public PluginInfo[] EnumeratePlugins()
		{
			if (!System.IO.File.Exists(m_ConfigFile))
			{
				return new List<PluginInfo>().ToArray();
			}

			var xdoc = new System.Xml.XmlDocument();
			xdoc.Load(m_ConfigFile);

			var nodes = xdoc.DocumentElement.SelectNodes("//plugins/pluginInfo");

			var result = new List<PluginInfo>();
			foreach (System.Xml.XmlNode item in nodes)
			{
				var assemblyFile = item.Attributes["assemblyFile"].InnerText;
				result.Add(new PluginInfo()
				{
					AssemblyFile = System.IO.Path.Combine(m_BinPath, assemblyFile),
				});
			}
			return result.ToArray();
		}

		#endregion
	}
}
