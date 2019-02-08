using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasOne(x => x.PaymentMethod)
                   .WithOne(x => x.BankAccount)
                   .HasForeignKey<PaymentMethod>(x => x.BankAccountId);
        }
    }
}