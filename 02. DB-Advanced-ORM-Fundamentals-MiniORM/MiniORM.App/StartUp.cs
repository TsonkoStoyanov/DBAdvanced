using System;
using System.Linq;
using MiniORM.App.Data;
using MiniORM.App.Data.Entities;

namespace MiniORM.App
{
    class StartUp
    {
        static void Main()
        {
            var connectionString = "Server=.;Database=MiniORM;Integrated security=True";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
                {
                FirstName = "Tsonko",
                    LastName = "Inserted",
                    DepartmentId = context.Departments.First().Id,
                    IsEmployed = true,
                });

            var employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}
