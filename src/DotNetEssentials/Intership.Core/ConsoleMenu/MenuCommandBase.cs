using System;

namespace Intership.Core.ConsoleMenu
{
    public abstract class MenuCommandBase : IMenuCommand
    {
        public string Name { get; }

        public IMenuCommand Parent { get; set; }

        protected MenuCommandBase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        public void Execute()
        {
            Console.Clear();

            ExecuteCore();

            var parent = Parent;
            if (parent != null)
            {
                parent.Execute();
            }
            else
            {
                ExitMenuCommand.Instance.Execute();
            }
        }

        public abstract void ExecuteCore();
    }
}