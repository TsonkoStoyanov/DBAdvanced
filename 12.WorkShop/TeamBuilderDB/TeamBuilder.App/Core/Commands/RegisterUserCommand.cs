using System;
using System.Linq;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;

namespace TeamBuilder.App.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(7, args);


            string username = args[0];

            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            string password = args[1];

            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            string repeatedPasrrowrd = args[2];

            if (password != repeatedPasrrowrd)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            string firstName = args[3];

            string lastName = args[4];

            int age;

            bool isNumber = int.TryParse(args[5], out age);

            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            GenderEnum gender;

            bool IsGenderValid = Enum.TryParse(args[6], out gender);

            if (!IsGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }


            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(String.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.RegisterUser(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully";
        }

        private void RegisterUser(string username, string password, string firstName, string lastName, int age, GenderEnum gender)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
               User user = new User()
               {
                   Username = username, 
                   Password = password,
                   FirstName = firstName, 
                   LastName = lastName, 
                   Age = age, 
                   Gender = gender
               };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}