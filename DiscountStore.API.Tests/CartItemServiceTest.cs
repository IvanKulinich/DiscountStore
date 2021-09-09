using DiscountStore.BL.Services;
using DiscountStore.DAL.Data;
using DiscountStore.DAL.Repositories;
using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace DiscountStore.API.Tests
{
    public class CartItemServiceTest
    {
        [Fact]
        public void AddAsyncTest_NewItem()
        {
            CartItemModel item = new CartItemModel
            {
                ProductId = 1,
                ProductCount = 4
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_CartServiceAdd1")
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

                var cartRepositoryMock = new Mock<CartItemRepository>(context);
                CartService cartService = new CartService(cartRepositoryMock.Object);

                var dbCartItem = new CartItem
                {
                    Id = 3,
                    ProductId = 1,
                    Count = 4
                };
                var result = cartService.AddAsync(item).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.Equal(dbCartItem.Id, result.Id);
                Assert.Equal(dbCartItem.ProductId, result.ProductId);
                Assert.Equal(dbCartItem.Count, result.Count);
            }
        }

        [Fact]
        public void AddAsyncTest_EditCount()
        {
            CartItemModel item = new CartItemModel
            {
                ProductId = 3,
                ProductCount = 4
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_CartServiceAdd2")
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

                var cartRepositoryMock = new Mock<CartItemRepository>(context);
                CartService cartService = new CartService(cartRepositoryMock.Object);

                var result = cartService.AddAsync(item).GetAwaiter().GetResult();

                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
                Assert.Equal(3, result.ProductId);
                Assert.Equal(7, result.Count);
            }
        }

        [Fact]
        public void RemoveAsyncTest_EditCount()
        {
            CartItemModel item = new CartItemModel
            {
                ProductId = 3,
                ProductCount = 2
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_CartServiceRemove1")
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

                var cartRepositoryMock = new Mock<CartItemRepository>(context);
                CartService cartService = new CartService(cartRepositoryMock.Object);

                cartService.RemoveAsync(item).GetAwaiter().GetResult();
                var resultCartItem = context.CartItems
                    .Where(x => x.ProductId == item.ProductId)
                    .FirstOrDefault();

                Assert.NotNull(resultCartItem);
                Assert.Equal(item.ProductId, resultCartItem.ProductId);
                Assert.Equal(1, resultCartItem.Count);
            }
        }

        [Fact]
        public void RemoveAsyncTest_DeleteItem()
        {
            CartItemModel item = new CartItemModel
            {
                ProductId = 3,
                ProductCount = 8
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_CartServiceRemove2")
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

                var cartRepositoryMock = new Mock<CartItemRepository>(context);
                CartService cartService = new CartService(cartRepositoryMock.Object);

                cartService.RemoveAsync(item).GetAwaiter().GetResult();
                var resultCartItem = context.CartItems
                    .Where(x => x.ProductId == item.ProductId)
                    .FirstOrDefault();

                Assert.Null(resultCartItem);
            }
        }

        [Fact]
        public void RemoveAsyncTest_Product_NotFound()
        {
            CartItemModel item = new CartItemModel
            {
                ProductId = 99,
                ProductCount = 8
            };

            //Create In Memory Database
            var options = new DbContextOptionsBuilder<StoreDbContext>()
                .UseInMemoryDatabase(databaseName: "DiscountStorageTest_CartServiceRemove3")
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

                var cartRepositoryMock = new Mock<CartItemRepository>(context);
                CartService cartService = new CartService(cartRepositoryMock.Object);

                Action act = () => cartService.RemoveAsync(item).GetAwaiter().GetResult();

                Exception exception = Assert.Throws<Exception>(act);
                Assert.Equal($"Cart item with product id {item.ProductId} not found.", exception.Message);
            }
        }
    }
}
