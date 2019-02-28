using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.MetadataMenu;

namespace ObjectBrowser
{
	class Program
	{
		static void Main(string[] args)
		{
			 AppDomain.CurrentDomain.GetAssemblies().GetAssembliesMenuItem("Loaded assemblies").Execute();
		}
	}
}
