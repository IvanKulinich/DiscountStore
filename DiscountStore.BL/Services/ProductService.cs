using DiscountStore.Domain.Entities;
using DiscountStore.Domain.Interfaces.Repositories;
using DiscountStore.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace DiscountStore.BL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
