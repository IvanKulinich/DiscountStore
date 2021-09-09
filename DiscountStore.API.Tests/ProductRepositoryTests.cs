using DiscountStore.DAL.Data;
using DiscountStore.DAL.Repositories;
using DiscountStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DiscountStore.API.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void GetByIdTests_ProductId_Exist()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_Product1")
                .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (StoreDbContext context = new StoreDbContext(options))
            {
                context.Discounts.AddRange(
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

                context.Products.AddRange(
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

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                ProductRepository productRepository = new ProductRepository(context);

                var product = productRepository.GetByIdAsync(1)
                    .GetAwaiter()
                    .GetResult();

                Assert.NotNull(product);
                Assert.Equal(1, product.Id);
                Assert.Equal("Vase", product.Name);
                Assert.Equal(1.2m, product.Price);
            }
        }

        [Fact]
        public void GetByIdTests_ProductId_Not_Exist()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_Product2")
                .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (StoreDbContext context = new StoreDbContext(options))
            {
                context.Discounts.AddRange(
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

                context.Products.AddRange(
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

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                ProductRepository productRepository = new ProductRepository(context);

                var product = productRepository.GetByIdAsync(7)
                    .GetAwaiter()
                    .GetResult();

                Assert.Null(product);
            }
        }
    }
}
