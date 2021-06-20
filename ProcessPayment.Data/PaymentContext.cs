using Microsoft.EntityFrameworkCore;
using ProcessPayment.Domain.Entities;

namespace ProcessPayment.Data
{
    public class PaymentContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentState { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var payment = modelBuilder.Entity<Payment>();
            payment.HasKey(e => e.PaymentId);
            payment.Property(a => a.SecurityCode)
              .IsRequired()
              .HasMaxLength(3)
              .IsFixedLength();
            payment.Property(a => a.Amount)
                   .IsRequired();
            payment.Property(a => a.CreditCardNumber)
                   .IsRequired();

        }
    }
}
