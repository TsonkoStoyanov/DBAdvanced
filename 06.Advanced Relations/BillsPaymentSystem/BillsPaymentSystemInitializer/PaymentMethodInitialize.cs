using P01_BillsPaymentSystem.Data.Models;

namespace BillsPaymentSystemInitializer
{
    public class PaymentMethodInitialize
    {
        public static PaymentMethod[] GetPaymentMethods()
        {
            PaymentMethod[] paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod() {UserId = 1, BankAccountId = 1, Type = PaymentMethodType.BankAccount},
                new PaymentMethod() {UserId = 1, BankAccountId = 2, Type = PaymentMethodType.CreditCard},
                new PaymentMethod() {UserId = 2, BankAccountId = 1, Type = PaymentMethodType.BankAccount},
                new PaymentMethod() {UserId = 2, BankAccountId = 3, Type = PaymentMethodType.CreditCard},
                new PaymentMethod() {UserId = 3, BankAccountId = 4, Type = PaymentMethodType.CreditCard},
                new PaymentMethod() {UserId = 4, BankAccountId = 4, Type = PaymentMethodType.BankAccount},
            };

            return paymentMethods;
        }
    }
}