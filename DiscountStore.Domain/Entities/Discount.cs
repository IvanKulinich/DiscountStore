using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountStore.Domain.Entities
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
