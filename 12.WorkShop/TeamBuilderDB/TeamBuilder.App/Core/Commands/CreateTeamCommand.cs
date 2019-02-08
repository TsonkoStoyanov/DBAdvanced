using System;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateTeamCommand:ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);
            string name = args[0];

            if (CommandHelper.IsTeamExisting(name))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamExists);
            }

            string acronym = args[1];

            if (acronym.Length != Constants.AcronymLength)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidAcronym);
            }

            string description = args.Length == 3 ? args[2] : null;

            User creator = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Authorize();


            Team team = new Team
            {
                Name = name,
                Description = description,
                Acronym = acronym,
                CreatorId = creator.Id,
                
            };

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Teams.Add(team);
               
                context.SaveChanges();
            }
           

            return $"Team {name} successfully created!";
        }
    }
}