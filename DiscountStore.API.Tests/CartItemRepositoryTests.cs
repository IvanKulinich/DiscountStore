using DiscountStore.DAL.Data;
using DiscountStore.DAL.Repositories;
using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace DiscountStore.API.Tests
{
    public class CartItemRepositoryTests
    {
        [Fact]
        public void GetTotalPriceTest_WithCartItemData()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest1")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 5
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 7
                    },
                    new CartItem
                    {
                        Id = 3,
                        ProductId = 1,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                decimal totalProductPrice = cartItemRepository.GetTotalPriceAsync()
                    .GetAwaiter()
                    .GetResult();

                Assert.Equal(9.85m, totalProductPrice);
            }
        }

        [Fact]
        public void GetTotalPriceTest_EmptyCartItems()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest2")
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
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                decimal totalProductPrice = cartItemRepository.GetTotalPriceAsync()
                    .GetAwaiter()
                    .GetResult();

                Assert.Equal(0, totalProductPrice);
            }
        }

        [Fact]
        public void GetTotalPriceTest_WithCartItemData_Count_Less_DiscountAmount()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest3")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 1
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 3,
                        ProductId = 1,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                decimal totalProductPrice = cartItemRepository.GetTotalPriceAsync()
                    .GetAwaiter()
                    .GetResult();

                Assert.Equal(5.5m, totalProductPrice);
            }
        }

        [Fact]
        public void GetTotalPriceTest_WithCartItemData_Count_Equal_DiscountAmount()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest4")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    },
                    new CartItem
                    {
                        Id = 3,
                        ProductId = 1,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                decimal totalProductPrice = cartItemRepository.GetTotalPriceAsync()
                    .GetAwaiter()
                    .GetResult();

                Assert.Equal(6.0m, totalProductPrice);
            }
        }

        [Fact]
        public void AddCartItemTest_AddNewItem()
        {
            CartItemModel item = new CartItemModel()
            {
                ProductId = 1,
                ProductCount = 4
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_ForAdd2")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                var cartItem = cartItemRepository.AddAsync(item)
                    .GetAwaiter()
                    .GetResult();

                var carItem = context.CartItems
                    .Where(x => x.ProductId == item.ProductId && x.Count == item.ProductCount)
                    .FirstOrDefault();

                Assert.NotNull(cartItem);
            }
        }

        [Fact]
        public void UpdateCountAsyncTest()
        {
            CartItem item = new CartItem()
            {
                Id = 1,
                ProductId = 2,
                Count = 2
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_Update1")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                var cartItem = cartItemRepository.UpdateCountAsync(item, 9)
                    .GetAwaiter()
                    .GetResult();

                Assert.NotNull(cartItem);
                Assert.Equal(9, cartItem.Count);
            }
        }

        [Fact]
        public void RemoveAsyncTest()
        {
            CartItem item = new CartItem()
            {
                Id = 1,
                ProductId = 2,
                Count = 2
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_Remove1")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                cartItemRepository.RemoveAsync(item)
                    .GetAwaiter()
                    .GetResult();

                var cartItem = context.CartItems.FirstOrDefault(x => x.Id == item.Id);

                Assert.Null(cartItem);
            }
        }

        [Fact]
        public void GetByProductIdAsyncTest_Exist()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_GetbyProductId1")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                var cartItem = cartItemRepository.GetByProductIdAsync(3)
                    .GetAwaiter()
                    .GetResult();

                Assert.NotNull(cartItem);
                Assert.Equal(2, cartItem.Id);
                Assert.Equal(3, cartItem.ProductId);
                Assert.Equal(3, cartItem.Count);
            }
        }

        [Fact]
        public void GetByProductIdAsyncTest_NotExist()
        {
            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_GetbyProductId2")
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

                context.CartItems.AddRange(
                    new CartItem
                    {
                        Id = 1,
                        ProductId = 2,
                        Count = 2
                    },
                    new CartItem
                    {
                        Id = 2,
                        ProductId = 3,
                        Count = 3
                    });

                context.SaveChanges();
            }

            //Use a context instance with Data to run the test 
            using (StoreDbContext context = new StoreDbContext(options))
            {
                CartItemRepository cartItemRepository = new CartItemRepository(context);

                var cartItem = cartItemRepository.GetByProductIdAsync(11)
                    .GetAwaiter()
                    .GetResult();

                Assert.Null(cartItem);
            }
        }
    }
}
