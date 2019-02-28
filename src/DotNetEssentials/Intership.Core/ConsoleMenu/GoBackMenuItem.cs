using System;

namespace Intership.Core.ConsoleMenu
{
	public class GoBackMenuItem : MenuItem
	{
		public GoBackMenuItem(MenuItem parent) : base("[GO BACK]")
		{
			Parent = parent ?? throw new ArgumentNullException(nameof(parent));
		}

		public override void Execute()
		{
			Parent.Execute();
		}
	}
}