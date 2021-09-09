using DiscountStore.Domain.Entities;
using System.Threading.Tasks;

namespace DiscountStore.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);
    }
}
