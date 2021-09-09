using DiscountStore.Domain.DTO;
using DiscountStore.Domain.Entities;
using DiscountStore.Domain.Interfaces.Repositories;
using DiscountStore.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace DiscountStore.BL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItem> AddAsync(CartItemModel item)
        {
            var cartItem = await _cartItemRepository.GetByProductIdAsync(item.ProductId);

            return cartItem != null
                ? await _cartItemRepository.UpdateCountAsync(cartItem, cartItem.Count + item.ProductCount)
                : await _cartItemRepository.AddAsync(item);
        }

        public async Task<decimal> GetTotalPriceAsync()
        {
            return await _cartItemRepository.GetTotalPriceAsync();
        }

        public async Task RemoveAsync(CartItemModel item)
        {
            var cartItem = await _cartItemRepository.GetByProductIdAsync(item.ProductId);
            if (cartItem == null)
            {
                throw new Exception($"Cart item with product id {item.ProductId} not found.");
            }

            if (cartItem.Count > item.ProductCount)
            {
                await _cartItemRepository.UpdateCountAsync(cartItem, cartItem.Count - item.ProductCount);
            }
            else
            {
                await _cartItemRepository.RemoveAsync(cartItem);
            }
        }
    }
}
