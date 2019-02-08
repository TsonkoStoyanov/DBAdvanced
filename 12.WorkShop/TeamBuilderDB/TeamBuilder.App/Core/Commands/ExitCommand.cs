﻿using System;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            Environment.Exit(0);

            return "Bye!";
        }
    }
}