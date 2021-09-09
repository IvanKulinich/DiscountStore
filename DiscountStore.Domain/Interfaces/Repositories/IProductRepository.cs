using DiscountStore.Domain.Entities;
using System.Threading.Tasks;

namespace DiscountStore.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
    }
}
