using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommercePrototype
{
    public class ProductCatalog
    {
        private List<Product> products;

        public ProductCatalog()
        {
            // Initialize a simple product catalog
            products = new List<Product>();
        }

        public bool AddProduct(string name, double price)
        {
            int newId = products.Count + 1;
            products.Add(new Product { Id = newId, Name = name, Price = price });
            return true; // Product added successfully
        }

        public bool RemoveProduct(int productId)
        {
            var productToRemove = products.Find(p => p.Id == productId);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                return true; // Product removed successfully
            }
            return false; // Product not found
        }

        public List<Product> ListProducts()
        {
            return products; // Return the list of products
        }

        public List<Product> SearchProduct(string name)
        {
            return products.FindAll(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)); // Return found products
        }

        // Making Product class public for testing purposes
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
    }
}
