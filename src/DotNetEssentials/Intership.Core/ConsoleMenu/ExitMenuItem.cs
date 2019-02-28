using System;

namespace Intership.Core.ConsoleMenu
{
	public class ExitMenuItem : MenuItem
	{
		public static readonly ExitMenuItem Instance = new ExitMenuItem();

		public ExitMenuItem() : base("[EXIT]")
		{
		}

		public override void Execute()
		{
			Console.WriteLine("Press ENTER to exit...");
			Console.ReadLine();
			Environment.Exit(0);
		}
	}
}