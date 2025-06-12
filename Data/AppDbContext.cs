using Microsoft.EntityFrameworkCore;
using InvoiceSystemAPI.Models;

namespace InvoiceSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascade delete loop between Invoice and User
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent cascade delete loop between Invoice and Customer
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Delete invoice items when invoice is deleted
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany()
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Delete payment record when invoice is deleted
            modelBuilder.Entity<InvoicePayment>()
                .HasOne(ip => ip.Invoice)
                .WithMany()
                .HasForeignKey(ip => ip.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent cascade loop when deleting User with service items
            modelBuilder.Entity<ServiceItem>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent cascade loop when deleting User with customers
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
