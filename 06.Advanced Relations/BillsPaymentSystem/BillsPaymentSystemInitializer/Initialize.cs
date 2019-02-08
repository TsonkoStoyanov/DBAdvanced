using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystemInitializer
{
    public class Initialize
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            InsertUsers(context);
            InsertCreditCards(context);
            InsertBankAccounts(context);
            InsertPaymentMethods(context);
        }

        private static void InsertBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccount = BankAccountInitilize.GetBankAccounts();

            for (int i = 0; i < bankAccount.Length; i++)
            {
                if (IsValid(bankAccount[i]))
                {
                    context.BankAccounts.Add(bankAccount[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertCreditCards(BillsPaymentSystemContext context)
        {
            var creditCard = CreditCardInitilize.GetCreditCard();

            for (int i = 0; i < creditCard.Length; i++)
            {
                if (IsValid(creditCard[i]))
                {
                    context.CreditCards.Add(creditCard[i]);
                }
            }

            context.SaveChanges();
        }

        private  static void InsertUsers(BillsPaymentSystemContext context)
        {
            var users = UserInitilize.GetUsers();

            for (int i = 0; i < users.Length; i++)
            {
                if (IsValid(users[i]))
                {
                    context.Users.Add(users[i]);
                }
            }

            context.SaveChanges();
        }

        private static void InsertPaymentMethods(BillsPaymentSystemContext context)
        {
            var payments = PaymentMethodInitialize.GetPaymentMethods();

            for (int i = 0; i < payments.Length; i++)
            {
                if (IsValid(payments[i]))
                {
                    context.PaymentMethods.Add(payments[i]);
                }
            }

            context.SaveChanges();
        }

        public static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
