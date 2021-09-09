using DiscountStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountStore.DAL.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discount>()
                .HasMany(d => d.Products)
                .WithOne(p => p.Discount);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(c => c.Product);

            modelBuilder.Entity<CartItem>()
                .HasIndex(x => x.ProductId)
                .IsUnique();

            modelBuilder.Entity<Discount>().HasData(
                new Discount
                {
                    Id = 1,
                    Amount = 2,
                    Price = 1.5m
                },
                new Discount
                {
                    Id = 2,
                    Amount = 3,
                    Price = 0.9m
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Vase",
                    Price = 1.2m
                },
                new Product
                {
                    Id = 2,
                    Name = "Big mug",
                    Price = 1.0m,
                    DiscountId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Napkins pack",
                    Price = 0.45m,
                    DiscountId = 2
                });
        }
    }
}
