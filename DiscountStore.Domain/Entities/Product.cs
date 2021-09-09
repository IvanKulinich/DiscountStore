using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountStore.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public int? DiscountId { get; set; }

        public Discount Discount { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
