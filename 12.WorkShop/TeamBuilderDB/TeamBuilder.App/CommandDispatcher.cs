using System;
using System.Linq;
using TeamBuilder.App.Core.Commands;

namespace TeamBuilder.App
{
    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string[] inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result = registerUser.Execute(inputArgs);
                    break;

                case "Login":
                    LoginCommand login = new LoginCommand();
                    result = login.Execute(inputArgs);
                    break;

                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(inputArgs);
                    break;

                case "CreateTeam":
                    CreateTeamCommand createTeam = new CreateTeamCommand();
                    result = createTeam.Execute(inputArgs);
                    break;

                case "InviteToTeam":
                    InviteToTeamCommand inviteToTeam = new InviteToTeamCommand();
                    result = inviteToTeam.Execute(inputArgs);
                    break;

                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand();
                    result = deleteUser.Execute(inputArgs);
                    break;

                case "CreateEvent":
                    CreateEventCommand createEvent = new CreateEventCommand();
                    result = createEvent.Execute(inputArgs);
                    break;

                case "AcceptInvite":
                    AcceptInviteCommand acceptInvite = new AcceptInviteCommand();
                    result = acceptInvite.Execute(inputArgs);
                    break;

                case "DeclineInvite":
                    DeclineInviteCommand declineInvite = new DeclineInviteCommand();
                    result = declineInvite.Execute(inputArgs);
                    break;

                case "Disband":
                    DisbandCommand disband = new DisbandCommand();
                    result = disband.Execute(inputArgs);
                    break;

                case "ShowEvent":
                    ShowEventCommand showEvent = new ShowEventCommand();
                    result = showEvent.Execute(inputArgs);
                    break;

                case "ShowTeam":
                    ShowTeamCommand showTeam = new ShowTeamCommand();
                    result = showTeam.Execute(inputArgs);
                    break;

                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    exit.Execute(inputArgs);
                    break;

                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }

            return result;
        }
    }
}