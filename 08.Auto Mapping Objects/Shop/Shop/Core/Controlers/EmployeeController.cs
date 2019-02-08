using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shop.App.Core.Contracts;
using Shop.App.Core.Dtos;
using Shop.Data;
using Shop.Models;

namespace Shop.App.Core.Controlers
{
    public class EmployeeController : IEmployeeController
    {
        private readonly ShopContext context;
        private readonly IMapper mapper;


        public EmployeeController(ShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = mapper.Map<Employee>(employeeDto);
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees
                .Find(employeeId);


            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeDto = mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeeInfoDto GetEmployeeInfoDto(int employeeId)
        {
            var employee = context.Employees
                .Find(employeeId);


            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            var employeeInfoDto = mapper.Map<EmployeeInfoDto>(employee);
               

           

            return employeeInfoDto;
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Address = address;
            this.context.SaveChanges();
        }

        public void SetBirthday (int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }


            employee.Birthday = date;

            context.SaveChanges();
        }
    }
}