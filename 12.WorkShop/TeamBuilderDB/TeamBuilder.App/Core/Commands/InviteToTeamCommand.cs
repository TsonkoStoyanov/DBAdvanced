using System;
using System.Linq;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);

            string userToInvite = args[1];
            string teamName = args[0];

            User currentUser = AuthenticationManager.GetCurrentUser();

       


            if (!CommandHelper.IsUserExisting(userToInvite))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (CommandHelper.IsInviteExisting(teamName, userToInvite))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            ValidateIfIsAllowed(currentUser, teamName, userToInvite);

       
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                var invaitedUserToAdd = context.Users.FirstOrDefault(x => x.Username == userToInvite);

                
                var invaitedToTeam = context.Teams.FirstOrDefault(x => x.Name == teamName);
                Invitation invitation = new Invitation()
                {
                    InvaitedUserId = invaitedUserToAdd.Id,
                    TeamId = invaitedToTeam.Id
               
                };

                context.Invitations.Add(invitation);
                context.SaveChanges();
            }

            return $"Team {teamName} invited {userToInvite}!";
        }

        private void ValidateIfIsAllowed(User currentUser, string teamName, string userToInvite)
        {
            bool isCurrentUserCraetorOfTeam = CommandHelper.IsUserCreatorOfTeam(teamName, currentUser);

           bool isUserToIviteMemberOfTeam = CommandHelper.IsMemberOfTeam(teamName, userToInvite);

            if (!isCurrentUserCraetorOfTeam || isUserToIviteMemberOfTeam)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CommandNotAllowed);
            }
        }

    
    }
}