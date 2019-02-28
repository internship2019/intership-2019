using System;

namespace Intership.Core.ConsoleMenu
{
	public sealed class CommandMenuItem : MenuItem
	{
		private readonly Action command;

		public CommandMenuItem(string name, Action command, MenuItem parent = null) : base(name)
		{
			this.command = command ?? throw new ArgumentNullException(nameof(command));
			Parent = parent;
		}

		public override void Execute()
		{
			command();

			var parent = Parent;
			if (parent != null)
			{
				parent.Execute();

				Console.WriteLine("Press ENTER to go back...");
				Console.ReadLine();
			}
			else
			{
				ExitMenuItem.Instance.Execute();
			}
		}
	}
}