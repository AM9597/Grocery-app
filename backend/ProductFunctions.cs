using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using static backend.DataStore;

namespace backend
{
    public static class ProductFunctions
    {
        static Dictionary<string, List<CartItem>> Carts = new Dictionary<string, List<CartItem>>();

        // -------------------------------------------------------
        // GET /categories
        // -------------------------------------------------------
        [FunctionName("GetCategories")]
        public static IActionResult GetCategories(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categories")] HttpRequest req
        )
        {
            return new OkObjectResult(DataStore.Categories);
        }

        // -------------------------------------------------------
        // GET /products
        // -------------------------------------------------------
        [FunctionName("GetProducts")]
        public static IActionResult GetProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req
        )
        {
            return new OkObjectResult(DataStore.Products);
        }

        // -------------------------------------------------------
        // GET /products/{id}
        // -------------------------------------------------------
        [FunctionName("GetProductById")]
        public static IActionResult GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id}")]
                HttpRequest req,
            string id
        )
        {
            var product = DataStore.Products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return new NotFoundObjectResult("Product not found");

            return new OkObjectResult(product);
        }

        // -------------------------------------------------------
        // POST /products
        // -------------------------------------------------------
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> AddProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var newProduct = JsonConvert.DeserializeObject<Product>(body);

            newProduct.ProductID = Guid.NewGuid().ToString();
            DataStore.Products.Add(newProduct);

            return new OkObjectResult(newProduct);
        }

        // -------------------------------------------------------
        // PUT /products/{id}
        // -------------------------------------------------------
        [FunctionName("UpdateProduct")]
        public static async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products/{id}")]
                HttpRequest req,
            string id
        )
        {
            var existing = DataStore.Products.FirstOrDefault(p => p.ProductID == id);

            if (existing == null)
                return new NotFoundObjectResult("Product not found");

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<Product>(body);

            existing.ProductName = updated.ProductName;
            existing.Price = updated.Price;
            existing.CategoryID = updated.CategoryID;
            existing.ImageUrl = updated.ImageUrl;
            existing.Description = updated.Description;
            existing.IsAvailable = updated.IsAvailable;

            return new OkObjectResult(existing);
        }

        // -------------------------------------------------------
        // DELETE /products/{id}
        // -------------------------------------------------------
        [FunctionName("DeleteProduct")]
        public static IActionResult DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{id}")]
                HttpRequest req,
            string id
        )
        {
            var product = DataStore.Products.FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return new NotFoundObjectResult("Product not found");

            DataStore.Products.Remove(product);
            return new OkObjectResult($"Deleted product {id}");
        }

        // ====================== CART ======================

        [FunctionName("GetCart")]
        public static IActionResult GetCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cart/{customerId}")]
                HttpRequest req,
            string customerId
        )
        {
            if (!Carts.ContainsKey(customerId))
                Carts[customerId] = new List<CartItem>();

            return new OkObjectResult(Carts[customerId]);
        }

        [FunctionName("AddToCart")]
        public static async Task<IActionResult> AddToCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cart/{customerId}/add")]
                HttpRequest req,
            string customerId
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CartItem>(body);

            if (!Carts.ContainsKey(customerId))
                Carts[customerId] = new List<CartItem>();

            var cart = Carts[customerId];

            var existing = cart.FirstOrDefault(c => c.Product.ProductID == input.Product.ProductID);

            if (existing != null)
                existing.Qty += input.Qty;
            else
                cart.Add(input);

            return new OkObjectResult(cart);
        }

        [FunctionName("UpdateCartQty")]
        public static async Task<IActionResult> UpdateCartQty(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cart/{customerId}/update")]
                HttpRequest req,
            string customerId
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CartItem>(body);

            if (!Carts.ContainsKey(customerId))
                Carts[customerId] = new List<CartItem>();

            var cart = Carts[customerId];

            var item = cart.FirstOrDefault(c => c.Product.ProductID == input.Product.ProductID);

            if (item != null)
            {
                if (input.Qty <= 0)
                    cart.Remove(item);
                else
                    item.Qty = input.Qty;
            }

            return new OkObjectResult(cart);
        }

        [FunctionName("RemoveFromCart")]
        public static IActionResult RemoveFromCart(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "delete",
                Route = "cart/{customerId}/remove/{productId}"
            )]
                HttpRequest req,
            string customerId,
            string productId
        )
        {
            if (!Carts.ContainsKey(customerId))
                Carts[customerId] = new List<CartItem>();

            var cart = Carts[customerId];
            Carts[customerId] = cart.Where(c => c.Product.ProductID != productId).ToList();

            return new OkObjectResult(Carts[customerId]);
        }

        [FunctionName("ClearCart")]
        public static IActionResult ClearCart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "cart/{customerId}/clear")]
                HttpRequest req,
            string customerId
        )
        {
            if (!Carts.ContainsKey(customerId))
                Carts[customerId] = new List<CartItem>();

            Carts[customerId].Clear();

            return new OkObjectResult(Carts[customerId]);
        }

        // ====================== CUSTOMER AUTH ======================

        [FunctionName("RegisterCustomer")]
        public static async Task<IActionResult> RegisterCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/register")]
                HttpRequest req
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(body);

            var exists = DataStore.Customers.Any(c => c.Mobile == customer.Mobile);
            if (exists)
                return new BadRequestObjectResult("Mobile number already registered");

            customer.CustomerID = Guid.NewGuid().ToString();
            DataStore.Customers.Add(customer);

            return new OkObjectResult(customer);
        }

        [FunctionName("LoginCustomer")]
        public static async Task<IActionResult> LoginCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "auth/login")]
                HttpRequest req
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Customer>(body);

            var customer = DataStore.Customers.FirstOrDefault(c =>
                c.Mobile == input.Mobile && c.Password == input.Password
            );

            if (customer == null)
                return new UnauthorizedResult();

            return new OkObjectResult(customer);
        }

        // ====================== MANAGEMENT (ADMIN) ======================

        [FunctionName("LoginManagement")]
        public static async Task<IActionResult> LoginManagement(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "management/login")]
                HttpRequest req
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Admin>(body);

            var admin = AdminDatabase.Admins.FirstOrDefault(a =>
                a.Mobile == input.Mobile && a.Password == input.Password
            );

            if (admin == null)
                return new OkObjectResult(new { isValid = false });

            return new OkObjectResult(new { isValid = true, admin });
        }

        [FunctionName("AddManagement")]
        public static async Task<IActionResult> AddManagement(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "management")]
                HttpRequest req
        )
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Admin>(body);

            Admin newAdmin = new Admin
            {
                AdminID = Guid.NewGuid().ToString(),
                Name = data.Name,
                Mobile = data.Mobile,
                Password = data.Password,
            };

            AdminDatabase.Admins.Add(newAdmin);

            return new OkObjectResult(newAdmin);
        }

        [FunctionName("GetManagement")]
        public static IActionResult GetManagement(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "management")] HttpRequest req
        )
        {
            return new OkObjectResult(AdminDatabase.Admins);
        }
    }
}
