using System;
using P01_BillsPaymentSystem.Data.Models;

namespace BillsPaymentSystemInitializer
{
    public class CreditCardInitilize
    {
        public static CreditCard[] GetCreditCard()
        {
            var creditCards = new CreditCard[]
            {
                new CreditCard() {Limit = 200, MoneyOwed = 0, ExpirationDate = "11/19"},
                new CreditCard() {Limit = 500, MoneyOwed = 0, ExpirationDate = "03/19"},
                new CreditCard() {Limit = 600, MoneyOwed = 0, ExpirationDate = "10/19"},
                new CreditCard() {Limit = 300, MoneyOwed = 0, ExpirationDate = "01/19"},
                new CreditCard() {Limit = 100, MoneyOwed = 0, ExpirationDate = "12/19"},

            };

            return creditCards;
        }
    }
}