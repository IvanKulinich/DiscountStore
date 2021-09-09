using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using System.Threading.Tasks;

namespace DiscountStore.Domain.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetByProductIdAsync(int productId);

        Task<CartItem> AddAsync(CartItemModel item);

        Task<CartItem> UpdateCountAsync(CartItem item, int count);

        Task RemoveAsync(CartItem item);

        Task<decimal> GetTotalPriceAsync();
    }
}
