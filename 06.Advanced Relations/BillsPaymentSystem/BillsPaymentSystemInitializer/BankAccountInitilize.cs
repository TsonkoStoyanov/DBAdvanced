using P01_BillsPaymentSystem.Data.Models;

namespace BillsPaymentSystemInitializer
{
    public class BankAccountInitilize
    {
        public static BankAccount[] GetBankAccounts()
        {
            var bankAccounts = new BankAccount[]
            {
                new BankAccount() {BankName = "Unicredit", SWIFTCode = "UN", Balance = 2320.50m},
                new BankAccount() {BankName = "DSK", SWIFTCode = "D", Balance = 1220.50m},
                new BankAccount() {BankName = "UBB", SWIFTCode = "U", Balance = 2320.50m},
                new BankAccount() {BankName = "Raifaisen", SWIFTCode = "Rai", Balance = 4320.50m},
                new BankAccount() {BankName = "Tokuda", SWIFTCode = "T", Balance = 3320.50m},
            };

            return bankAccounts;
        }
    }
}