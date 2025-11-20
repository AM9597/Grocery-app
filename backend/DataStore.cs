using System.Collections.Generic;
using backend.Models;

namespace backend
{
    public static class DataStore
    {
        public static class AdminDatabase
        {
            public static List<Admin> Admins = new List<Admin>()
            {
                new Admin
                {
                    AdminID = "1",
                    Name = "Akshay Munot",
                    Mobile = "7756980045",
                    Password = "1234",
                },
            };
        }

        public class Admin
        {
            public string AdminID { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Password { get; set; }
        }

        public static List<Category> Categories = new List<Category>
        {
            new Category { CategoryID = "1", CategoryName = "Grocery" },
            new Category { CategoryID = "2", CategoryName = "Snacks" },
            new Category { CategoryID = "3", CategoryName = "Personal Care" },
            new Category { CategoryID = "4", CategoryName = "Beverages" },
            new Category { CategoryID = "5", CategoryName = "Bakery" },
        };

        public static List<Product> Products = new List<Product>
        {
            // Grocery
            new Product
            {
                ProductID = "101",
                CategoryID = "1",
                ProductName = "Rice 1kg",
                Price = 55,
                ImageUrl = "https://images.pexels.com/photos/4110256/pexels-photo-4110256.jpeg",
            },
            new Product
            {
                ProductID = "102",
                CategoryID = "1",
                ProductName = "Wheat Flour",
                Price = 40,
                ImageUrl = "https://images.pexels.com/photos/6287223/pexels-photo-6287223.jpeg",
            },
            new Product
            {
                ProductID = "103",
                CategoryID = "1",
                ProductName = "Sugar 1kg",
                Price = 45,
                ImageUrl = "https://via.placeholder.com/150?text=Sugar+1kg",
            },
            new Product
            {
                ProductID = "104",
                CategoryID = "1",
                ProductName = "Salt 500g",
                Price = 20,
                ImageUrl = "https://images.pexels.com/photos/2320244/pexels-photo-2320244.jpeg",
            },
            // Snacks
            new Product
            {
                ProductID = "201",
                CategoryID = "2",
                ProductName = "Parle-G",
                Price = 10,
                ImageUrl = "https://images.pexels.com/photos/4168645/pexels-photo-4168645.jpeg",
            },
            new Product
            {
                ProductID = "202",
                CategoryID = "2",
                ProductName = "Lays Chips",
                Price = 20,
                ImageUrl = "https://images.pexels.com/photos/30358849/pexels-photo-30358849.jpeg",
            },
            new Product
            {
                ProductID = "203",
                CategoryID = "2",
                ProductName = "Britannia Cake",
                Price = 15,
                ImageUrl = "https://via.placeholder.com/150?text=Britannia+Cake",
            },
            // Personal Care
            new Product
            {
                ProductID = "301",
                CategoryID = "3",
                ProductName = "Dove Shampoo",
                Price = 120,
                ImageUrl = "https://via.placeholder.com/150?text=Dove+Shampoo",
            },
            new Product
            {
                ProductID = "302",
                CategoryID = "3",
                ProductName = "Colgate Toothpaste",
                Price = 50,
                ImageUrl = "https://images.pexels.com/photos/5612670/pexels-photo-5612670.jpeg",
            },
            new Product
            {
                ProductID = "303",
                CategoryID = "3",
                ProductName = "Lifebuoy Soap",
                Price = 30,
                ImageUrl = "https://via.placeholder.com/150?text=Lifebuoy+Soap",
            },
            // Beverages
            new Product
            {
                ProductID = "401",
                CategoryID = "4",
                ProductName = "Coca Cola 500ml",
                Price = 40,
                ImageUrl = "https://images.pexels.com/photos/11109683/pexels-photo-11109683.jpeg",
            },
            new Product
            {
                ProductID = "402",
                CategoryID = "4",
                ProductName = "Thums Up 500ml",
                Price = 35,
                ImageUrl = "https://images.pexels.com/photos/11109683/pexels-photo-11109683.jpeg",
            },
            new Product
            {
                ProductID = "403",
                CategoryID = "4",
                ProductName = "Minute Maid Orange Juice",
                Price = 50,
                ImageUrl = "https://via.placeholder.com/150?text=Orange+Juice",
            },
            // Bakery
            new Product
            {
                ProductID = "501",
                CategoryID = "5",
                ProductName = "Brown Bread",
                Price = 25,
                ImageUrl = "https://via.placeholder.com/150?text=Brown+Bread",
            },
            new Product
            {
                ProductID = "502",
                CategoryID = "5",
                ProductName = "Buns (Pack of 4)",
                Price = 30,
                ImageUrl = "https://via.placeholder.com/150?text=Buns+4pcs",
            },
            new Product
            {
                ProductID = "503",
                CategoryID = "5",
                ProductName = "Croissant",
                Price = 35,
                ImageUrl = "https://via.placeholder.com/150?text=Croissant",
            },
        };

        public static List<Customer> Customers = new List<Customer>();
    }
}
