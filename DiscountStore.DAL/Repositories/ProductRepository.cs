using DiscountStore.DAL.Data;
using DiscountStore.Domain.Entities;
using DiscountStore.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiscountStore.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public ProductRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _storeDbContext.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
