using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using System.Threading.Tasks;

namespace DiscountStore.Domain.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartItem> AddAsync(CartItemModel item);

        Task RemoveAsync(CartItemModel item);

        Task<decimal> GetTotalPriceAsync();
    }
}
