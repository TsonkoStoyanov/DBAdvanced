using System;

namespace Shop.App.Core.Dtos
{
    public class EmployeeInfoDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string  Address { get; set; }
    }
}