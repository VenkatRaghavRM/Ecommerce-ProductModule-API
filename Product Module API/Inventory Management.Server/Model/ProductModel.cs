using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Server.Data
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, double.MaxValue)]
        public double DiscountedPrice { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
