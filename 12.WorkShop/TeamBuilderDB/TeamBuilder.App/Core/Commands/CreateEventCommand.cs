using System;
using System.Globalization;
using TeamBuilder.App.Core.Commands.Interfaces;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(6, args);

            string eventName = args[0];
            string description = args[1];
            string startDateStr = args[2] + " " + args[3];
            string endDateStr = args[4] + " " + args[5];

            ValidateDates(startDateStr, endDateStr, out DateTime startDate, out DateTime endDate);

            User creator = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Authorize();


            Event @event = new Event
            {
                Name = eventName,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = creator.Id
            };

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Events.Add(@event);
                context.SaveChanges();
            }

            return $"Event  was created successfully!";
        }

        private void ValidateDates(string startDateStr, string endDateStr, out DateTime startDate, out DateTime endDate)
        {
            startDate = default(DateTime);
            endDate = default(DateTime);

            try
            {
                startDate = DateTime.ParseExact(startDateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                endDate = DateTime.ParseExact(endDateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDate > endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.StartDateBeforeEndDate);
            }
        }
    }
}