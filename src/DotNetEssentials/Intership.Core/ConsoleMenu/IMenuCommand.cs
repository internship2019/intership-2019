namespace Intership.Core.ConsoleMenu
{
    public interface IMenuCommand
    {
        string Name { get; }

        void Execute();

        IMenuCommand Parent { get; set; }
    }
}