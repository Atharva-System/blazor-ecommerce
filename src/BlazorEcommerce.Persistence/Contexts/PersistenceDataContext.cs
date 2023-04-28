using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorEcommerce.Server.Contexts
{
    public class PersistenceDataContext : DbContext
    {
        public PersistenceDataContext(DbContextOptions<PersistenceDataContext> options) : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.UserId, ci.ProductId, ci.ProductTypeId });

            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductTypeId });

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Address>().HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
