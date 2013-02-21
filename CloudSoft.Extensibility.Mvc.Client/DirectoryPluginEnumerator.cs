using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSoft.Extensibility.Mvc.Client
{
	public class DirectoryPluginEnumerator : IPluginEnumerator
	{
		string m_CurrentDirectory;

		public DirectoryPluginEnumerator(string currentDirectory)
		{
			m_CurrentDirectory = currentDirectory;
		}

		#region IPluginEnumerator Members

		public PluginInfo[] EnumeratePlugins()
		{
			var asmList = from file in System.IO.Directory.GetFiles(m_CurrentDirectory, "*.dll", System.IO.SearchOption.TopDirectoryOnly)
						  let asm = System.Reflection.Assembly.LoadFrom(file)
						  where file.ToLower().IndexOf("nunit") == -1
								&& file.ToLower().IndexOf("microsoft.practices") == -1
								&& file.ToLower().IndexOf("serialcoder.erp.services.dll") == -1
								&& file.ToLower().IndexOf("ERPStore.core.dll") == -1
								&& file.ToLower().IndexOf("serialcoder.erp.data.dll") == -1
								&& file.ToLower().IndexOf("serialcoder.erp.data.sqlclient.dll") == -1
								&& file.ToLower().IndexOf("serialcoder.erp.unittests.dll") == -1
								&& file.ToLower().IndexOf("serialcoder.erp.entities.dll") == -1
								&& file.ToLower().IndexOf("serialcoder.reporting.dll") == -1
								&& !file.ToLower().EndsWith("zedgraph.dll")
								&& file.ToLower().IndexOf("resources.dll") == -1
						  select new
						  {
							  asm,
							  file,
						  };

			var result = new List<PluginInfo>();
			//foreach (var item in asmList)
			//{
			//    try
			//    {
			//        foreach (var type in item.asm.GetTypes())
			//        {
			//            foreach (var inter in type.GetInterfaces())
			//            {
			//                if (inter.FullName == typeof(Services.IPaymentService).FullName)
			//                {
			//                    // result.Add(type);
			//                }
			//            }
			//        }
			//        // Todo : Migrer toutes les interfaces en module
			//    }
			//    catch (Exception ex)
			//    {
			//        Logger.Error("Failed to check attribute in assembly : {0}\r\n{1}", item.file, ex.Message);
			//    }
			//}
			return result.ToArray();
		}

		#endregion
	}
}
