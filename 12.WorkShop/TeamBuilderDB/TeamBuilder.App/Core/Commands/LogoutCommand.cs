using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            AuthenticationManager.Authorize();

            User user = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}