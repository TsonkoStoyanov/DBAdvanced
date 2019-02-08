using System;
using System.Globalization;
using Shop.App.Core.Contracts;

namespace Shop.App.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeController employeeController;


        public SetBirthdayCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            
            this.employeeController.SetBirthday(id, date);

            return "Command acomplished successfull";
        }
    }
}