using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.App.Core;
using Shop.App.Core.Contracts;
using Shop.App.Core.Controlers;
using Shop.Data;
using Shop.Services;
using Shop.Services.Contracts;

namespace Shop.App
{
    class StartUp
    {
        static void Main()
        {
            var service = ConfigureServices();

            IEngine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ShopContext>(op=>op.UseSqlServer(Configuration.connectionString ));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<ShopProfile>());

            serviceCollection.AddTransient<IDbInitializerServices, DbInitializerServices>();

            serviceCollection.AddTransient<ICommandIntrepreter, CommandIntrepreter>();

            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
