using System;

namespace backend.Models
{
    public class Category
    {
        public string CategoryID { get; set; } = Guid.NewGuid().ToString();
        public string CategoryName { get; set; } = "";
    }
}
