using System;

namespace Intership.Core.ConsoleMenu
{
	public sealed class ActionMenuCommand : MenuCommandBase
	{
		private readonly Action command;

		public ActionMenuCommand(string name, Action command, IMenuCommand parent = null) : base(name)
		{
			this.command = command ?? throw new ArgumentNullException(nameof(command));
			Parent = parent;
		}

		public override void ExecuteCore()
		{
			command();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
	}
}