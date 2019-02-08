using System;
using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Any(t => t.Username == username);
            }
        }

        public static bool IsInviteExisting(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.FirstOrDefault(x => x.Username == username);
                
                if (user == null)
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserNotFound);
                }

                return context.Invitations.Any(i => i.Team.Name == teamName && i.InvaitedUserId == user.Id && i.IsActive);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(x => x.Name == teamName);
                return team.CreatorId == user.Id;
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return user.CreatedEvents.Any(x => x.Name == eventName);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Single(t => t.Name == teamName).Members.Any(ut => ut.User.Username == username);
            }

        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(x => x.Name == eventName);
            }
        }

    }
}