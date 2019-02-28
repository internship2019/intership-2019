using System;

namespace Intership.Core.ConsoleMenu
{
	public class ExitMenuCommand : MenuCommandBase
	{
		public static readonly ExitMenuCommand Instance = new ExitMenuCommand();

		public ExitMenuCommand() : base("[EXIT]")
		{
		}

		public override void ExecuteCore()
		{
			Console.WriteLine("Press ENTER to exit...");
			Console.ReadLine();
			Environment.Exit(0);
		}
	}
}