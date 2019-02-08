using System;
using System.Threading;
using Shop.App.Core.Contracts;

namespace Shop.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i >= 1; i--)
            {
                Console.WriteLine($"Program Will close after {i} seconds");
                Thread.Sleep(1000);
            }

            Environment.Exit(0);

            return null;
        }
    }
}