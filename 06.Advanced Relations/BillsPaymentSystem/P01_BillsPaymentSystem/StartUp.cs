using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main()
        {
            using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            {
                //Initialize.Seed(context);
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                User user = GetUser(context);
                GetInfo(user);
                PayBills(user, 200);

            }
        }

        private static void PayBills(User user, decimal amount)
        {
            var bankAccount = user.PaymentMethods.Where(x => x.BankAccount != null).Sum(x => x.BankAccount.Balance);
            var creditCard = user.PaymentMethods.Where(x => x.CreditCard != null).Sum(x => x.CreditCard.LimitLeft);

            var totalAmount = bankAccount + creditCard;

            if (totalAmount >= amount)
            {
                var bankAccounts = user.PaymentMethods.Where(x => x.BankAccount != null).Select(x => x.BankAccount)
                    .OrderBy(x => x.BankAccountId).ToArray();

                foreach (var ba in bankAccounts)
                {
                    if (ba.Balance >= amount)
                    {
                        ba.Withdraw(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= ba.Balance;
                        ba.Withdraw(ba.Balance);
                    }

                    if (amount == 0)
                    {
                        return;
                    }

                    var creditCards = user.PaymentMethods.Where(x => x.CreditCard != null).Select(x => x.CreditCard)
                        .OrderBy(x => x.CreditCardId).ToArray();

                    foreach (var ca in creditCards)
                    {
                        if (ca.LimitLeft >= amount)
                        {
                            ca.Withdraw(amount);
                            amount = 0;
                        }
                        else
                        {
                            amount -= ca.LimitLeft;
                            ca.Withdraw(ca.LimitLeft);
                        }

                        if (amount == 0)
                        {
                            return;
                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("Insufficient found!!");
            }
        }

        private static void GetInfo(User user)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Bank Accounts:");

            var bankAccounts = user.PaymentMethods.Where(x => x.BankAccount != null).Select(x => x.BankAccount)
                .ToArray();
            foreach (var ba in bankAccounts)
            {
                Console.WriteLine($"-- ID: {ba.BankAccountId}");
                Console.WriteLine($"--- Balance: {ba.Balance}");
                Console.WriteLine($"--- Bank: {ba.BankName}");
                Console.WriteLine($"--- SWIFT: {ba.SWIFTCode}");

            }

            var creditCards = user.PaymentMethods.Where(x => x.CreditCard != null).Select(x => x.CreditCard).ToArray();
            foreach (var ca in creditCards)
            {
                Console.WriteLine($"-- ID: {ca.CreditCardId}");
                Console.WriteLine($"--- Limit: {ca.Limit}");
                Console.WriteLine($"--- Money Owed: {ca.MoneyOwed}");
                Console.WriteLine($"--- Limit Left: {ca.LimitLeft}");
                Console.WriteLine($"--- Expiration Date: {ca.ExpirationDate}");

            }
        }

        private static User GetUser(BillsPaymentSystemContext context)
        {
            int userId = int.Parse(Console.ReadLine());

            var user = context.Users
                .Where(x => x.UserId == userId)
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.BankAccount)
                .Include(x => x.PaymentMethods)
                .ThenInclude(x => x.CreditCard)
                .FirstOrDefault();


            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
            }

            return user;
        }
    }
}
