using P01_BillsPaymentSystem.Data.Models;
using System;

namespace BillsPaymentSystemInitializer
{
    public class UserInitilize
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User() {FirstName = "Petyr", LastName = "Petrov", Email = "petyrpetrov@softuni.bg", Password = "1234567"},
                new User() {FirstName = "Georgi", LastName = "Petrov", Email = "georgipetrov@softuni.bg", Password = "11111"},
                new User() {FirstName = "Anton", LastName = "Petrov", Email = "antonpetrov@softuni.bg", Password = "1937"},
                new User() {FirstName = "Ivan", LastName = "Petrov", Email = "ivanpetrov@softuni.bg", Password = "4673"},
                new User() {FirstName = "Stoqn", LastName = "Petrov", Email = "stoqnpetrov@softuni.bg", Password = "123654"},
                new User() {FirstName = "Iliq", LastName = "Petrov", Email = "iliqpetrov@softuni.bg", Password = "222222"},
            };

            return users;
        }
    }
}
