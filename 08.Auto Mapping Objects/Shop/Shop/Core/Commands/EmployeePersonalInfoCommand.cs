using Shop.App.Core.Contracts;

namespace Shop.App.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public EmployeePersonalInfoCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            var employeeInfoDto = this.employeeController.GetEmployeeInfoDto(id);

            return $"ID: {employeeInfoDto.Id} - {employeeInfoDto.FirstName} {employeeInfoDto.LastName} - ${employeeInfoDto.Salary:f2}\n" +
                   $"Birthday: {employeeInfoDto.Birthday}\n" +
                   $"Address: {employeeInfoDto.Address}\n";

        }
    }
}