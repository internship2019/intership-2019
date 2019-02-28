using System;
using System.Collections.Generic;
using Intership.Core.Utils;

namespace Intership.Core.ConsoleMenu
{
	public class CompositeMenuItem : MenuItem
	{
		private readonly SortedDictionary<int, MenuItem> subMenus;

		public CompositeMenuItem(string name, IEnumerable<MenuItem> nestedItems) : this(name, null, nestedItems)
		{
		}

		public CompositeMenuItem(string name, MenuItem menuToReturn, IEnumerable<MenuItem> nestedItems) : base(name)
		{
			if (nestedItems == null)
			{
				throw new ArgumentNullException(nameof(nestedItems));
			}

			subMenus = new SortedDictionary<int, MenuItem>
			{
				[0] = menuToReturn ?? new GoBackMenuItem(this)
			};

			int index = 1;
			foreach (var nestedItem in nestedItems)
			{
				nestedItem.Parent = this;
				subMenus.Add(index++, nestedItem);
			}
		}

		public override void Execute()
		{
			Console.WriteLine(Name);

			var decimalStringLength = 1 + StringUtils.GetDecimalStringLength(subMenus.Count);

			string format = "{0," + decimalStringLength.ToString() + "}. {1}";

			foreach (var menuItem in subMenus)
			{
				Console.WriteLine(format, menuItem.Key.ToString(), menuItem.Value.Name);
			}

			var userEnteredIndex = ReadMenuIndex();

			subMenus[userEnteredIndex].Execute();
		}

		private int ReadMenuIndex()
		{
			while (true)
			{
				Console.Write("Type menu number and press ENTER: ");

				var input = Console.ReadLine();

				if (int.TryParse(input, out int result) && result >= 0)
				{
					return result;
				}
			}
		}
	}
}