using Shop.App.Core.Contracts;
using System;
using Shop.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.App.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var initializeDb = this.serviceProvider.GetService<IDbInitializerServices>();

            initializeDb.InitializeDatabase();

            var commandIntrepreter = this.serviceProvider.GetService<ICommandIntrepreter>();

            while (true)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string result = commandIntrepreter.Read(input);

                Console.WriteLine(result);

            }
        }
    }
}
