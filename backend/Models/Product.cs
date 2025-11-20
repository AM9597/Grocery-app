using System;

namespace backend.Models
{
    public class Product
    {
        public string ProductID { get; set; } // <-- Use only one
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string CategoryID { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}
