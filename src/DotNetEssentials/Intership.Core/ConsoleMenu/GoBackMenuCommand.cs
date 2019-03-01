using System;

namespace Intership.Core.ConsoleMenu
{
	public class GoBackMenuCommand : MenuCommandBase
	{
		public GoBackMenuCommand(IMenuCommand parent) : base("[GO BACK]")
		{
			Parent = parent ?? throw new ArgumentNullException(nameof(parent));
		}

		protected override void ExecuteCore()
		{
			Parent.Execute();
		}
	}
}