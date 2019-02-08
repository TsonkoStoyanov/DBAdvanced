using System;
using System.Linq;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LoginCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            string username = args[0];
            string password = args[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            User user = this.GetUserByCredentials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            AuthenticationManager.Login(user);

            return $"User {user.Username} successfully logged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                if (!context.Users.Any(x => x.Username == username))
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
                }

                User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsDeleted == false);

                if (user == null)
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
                }

                return user;
            }
        }
    }
}