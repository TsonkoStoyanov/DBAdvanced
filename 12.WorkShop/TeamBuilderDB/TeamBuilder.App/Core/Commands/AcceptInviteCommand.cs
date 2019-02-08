using System;
using System.Linq;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class AcceptInviteCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);

            string teamName = args[0];

            User user = AuthenticationManager.GetCurrentUser();

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamNotFound);
            }

            if (!CommandHelper.IsInviteExisting(teamName, user.Username))
            {
                throw new ArgumentException(Constants.ErrorMessages.InviteNotFound);
            }

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Invitation invitation = context.Invitations.First(i => i.Team.Name == teamName && i.InvaitedUserId == user.Id);

                invitation.IsActive = false;


                context.SaveChanges();
            }

            return $"User {user.Username} joined team {teamName}!";
        }

  }
}