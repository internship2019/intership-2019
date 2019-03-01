using System;
using System.Reflection;
using Intership.Core.ConsoleMenu;
using ObjectBrowser.MetadataMenu;

namespace ObjectBrowser
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Reflection metadata explorer demo.");
			Console.Write("Force load referenced assemblies? (y/n) ");

			var answer = (char) Console.Read();

			if (char.ToUpper(answer) == 'Y')
			{
				int loaded = 0, failed = 0;
				foreach (AssemblyName referencedAssembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
				{
					try
					{
						Assembly.Load(referencedAssembly);
						loaded++;
					}
					catch
					{
						failed++;
					}
				}

				Console.WriteLine("Loaded assemblies: {0}. Failed to load: {1}.", loaded, failed);
			}

			CompositeMenuCommand command = AppDomain.CurrentDomain.GetAssemblies().GetAssembliesMenuItem("Loaded assemblies");
			command.Execute();
		}
	}
}
