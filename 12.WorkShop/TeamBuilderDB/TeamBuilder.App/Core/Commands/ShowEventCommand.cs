using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowEventCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);

            string @event = args[0];

            throw new System.NotImplementedException();
        }
    }
}