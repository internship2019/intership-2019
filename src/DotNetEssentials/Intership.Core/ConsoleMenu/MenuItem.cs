using System;

namespace Intership.Core.ConsoleMenu
{
	public abstract class MenuItem
	{
		public string Name { get; }
		public MenuItem Parent { get; internal set; }

		protected MenuItem(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
			}

			Name = name;
		}

		public abstract void Execute();
	}
}
