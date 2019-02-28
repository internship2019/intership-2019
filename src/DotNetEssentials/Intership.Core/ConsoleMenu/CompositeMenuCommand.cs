using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Intership.Core.Utils;

namespace Intership.Core.ConsoleMenu
{
	public class CompositeMenuCommand : MenuCommandBase
	{
		private readonly SortedDictionary<int, IMenuCommand> subMenus;
        private int index;
		public CompositeMenuCommand(string name, IMenuCommand parent, IEnumerable<IMenuCommand> menuCommands = null)
            : base(name)
		{
			subMenus = new SortedDictionary<int, IMenuCommand>()
            {
                [0] = Parent = new GoBackMenuCommand(parent ?? ExitMenuCommand.Instance)
            };

            index = 1;

            if (menuCommands != null)
            {
                foreach (var nestedItem in menuCommands)
                {
                    nestedItem.Parent = this;
                    subMenus.Add(index++, nestedItem);
                }
            }
        }

        public void AddCommand(IMenuCommand menuCommand)
        {
            if (menuCommand == null) throw new ArgumentNullException(nameof(menuCommand));
            if (ReferenceEquals(this, menuCommand))
            {
                throw new InvalidOperationException("Can't add reference to self.");
            }

            menuCommand.Parent = this;
            subMenus.Add(index++, menuCommand);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void AddCommands(IEnumerable<IMenuCommand> menuCommands)
        {
            if (menuCommands == null) throw new ArgumentNullException(nameof(menuCommands));

            if (menuCommands.Any(x => ReferenceEquals(this, x)))
            {
                throw new InvalidOperationException("Can't add reference to self.");
            }
            foreach (var nestedItem in menuCommands)
            {
                nestedItem.Parent = this;
                subMenus.Add(index++, nestedItem);
            }
        }

        public override void ExecuteCore()
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

				if (int.TryParse(input, out int result) && result >= 0 && result < index)
				{
					return result;
				}
			}
		}
	}
}