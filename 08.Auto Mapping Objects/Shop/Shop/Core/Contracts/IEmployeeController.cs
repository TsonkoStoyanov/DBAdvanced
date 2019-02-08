using System;
using Shop.App.Core.Dtos;

namespace Shop.App.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeeInfoDto GetEmployeeInfoDto(int employeeId);


    }
}