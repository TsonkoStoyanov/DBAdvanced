using System.Linq;
using PhotoShare.Client.Core.Dtos;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;

        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string typeProperty = data[1];
            string newValue = data[2];

            var userExist = this.userService.Exists(username);

            if (!userExist)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;

            if (typeProperty == "Password")
            {
                SetPassword(userId, newValue);
            }
            else if (typeProperty == "BornTown")
            {
                SetBornTown(userId, newValue);
            }
            else if (typeProperty == "CurrentTown")
            {
                SetCurrentTown(userId, newValue);
            }
            else
            {
                throw new ArgumentException($"Property {typeProperty} not supported!");
            }

            return $"User {username} {typeProperty} is {newValue}.";

        }

        private void SetCurrentTown(int userId, string name)
        {
            var townExist = this.townService.Exists(name);
            if (!townExist)
            {
                throw new ArgumentException($"Value {name} not valid.\r\nTown {name} not found!\r\n");
            }

            var townId = this.townService.ByName<TownDto>(name).Id;
            this.userService.SetCurrentTown(userId, townId);
        }

        private void SetBornTown(int userId, string name)
        {
            var townExist = this.townService.Exists(name);
            if (!townExist)
            {
                throw new ArgumentException($"Value {name} not valid.\r\nTown {name} not found!\r\n");
            }

            var townId = this.townService.ByName<TownDto>(name).Id;
            this.userService.SetBornTown(userId, townId);
        }

        private void SetPassword(int userId, string password)
        {
            var isLower = password.Any(x => char.IsLower(x));
            var isDigit = password.Any(x => char.IsDigit(x));
            if (!isLower || !isDigit)
            {
                throw new ArgumentException($"Value {password} not valid.\r\nInvalid Password\r\n");
            }

            this.userService.ChangePassword(userId, password);
        }
    }
}
