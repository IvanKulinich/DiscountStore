using DiscountStore.DAL.Data;
using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using DiscountStore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountStore.DAL.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public CartItemRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public async Task<CartItem> GetByProductIdAsync(int productId)
        {
            return await _storeDbContext.CartItems
                .Where(x => x.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<CartItem> AddAsync(CartItemModel item)
        {
            var newCartItem = _storeDbContext.CartItems.Add(new CartItem
            {
                ProductId = item.ProductId,
                Count = item.ProductCount
            });

            await _storeDbContext.SaveChangesAsync();

            return newCartItem.Entity;
        }

        public async Task<CartItem> UpdateCountAsync(CartItem item, int count)
        {
            item.Count = count;

            _storeDbContext.Entry(item).State = EntityState.Modified;
            await _storeDbContext.SaveChangesAsync();

            return item;
        }

        public async Task RemoveAsync(CartItem item)
        {
            _storeDbContext.Remove(item);
            await _storeDbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            return await _storeDbContext.CartItems
                .SumAsync(x => x.Product.DiscountId.HasValue
                    ? (x.Count / x.Product.Discount.Amount * x.Product.Discount.Price)
                        + (x.Count % x.Product.Discount.Amount * x.Product.Price)
                    : x.Count * x.Product.Price);
        }
    }
}
