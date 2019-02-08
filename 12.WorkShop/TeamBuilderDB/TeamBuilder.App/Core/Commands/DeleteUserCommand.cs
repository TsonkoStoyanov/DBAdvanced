using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);

            AuthenticationManager.Authorize();

            User curentUser = AuthenticationManager.GetCurrentUser();

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                curentUser.IsDeleted = true;
                context.SaveChanges();

                AuthenticationManager.Logout();
            }

            return $"User {curentUser.Username} was deleted successfully!";
        }
    }
}