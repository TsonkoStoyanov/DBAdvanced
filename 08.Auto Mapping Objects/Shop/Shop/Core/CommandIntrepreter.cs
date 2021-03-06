﻿using System;
using System.Linq;
using System.Reflection;
using Shop.App.Core.Contracts;

namespace Shop.App.Core
{
    public class CommandIntrepreter : ICommandIntrepreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandIntrepreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        public string Read(string[] input)
        {

            string commandName = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes().FirstOrDefault(x => x.Name==commandName);

            if (type == null)
            {
                throw new ArgumentException("Invalid command");
            }
            
            var constructor = type.GetConstructors()
                .First();

            var constructorParameters = constructor.GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();


            var service = constructorParameters.Select(serviceProvider.GetService).ToArray();

            var command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);

            return result;
        }
    }
}