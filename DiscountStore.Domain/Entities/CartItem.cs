using System.ComponentModel.DataAnnotations;

namespace DiscountStore.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
