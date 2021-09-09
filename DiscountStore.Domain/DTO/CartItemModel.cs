using System;
using System.ComponentModel.DataAnnotations;

namespace DiscountStore.Domain.DTO
{
    public class CartItemModel
    {
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProductCount { get; set; }
    }
}
