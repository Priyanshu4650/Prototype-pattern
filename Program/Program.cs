using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommercePrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // User Authentication Prototype
            var authModule = new UserAuthentication();
            authModule.RegisterUser("newUser", "newPassword");
            authModule.AuthenticateUser("newUser", "newPassword");
            authModule.ListUsers();

            // Product Catalog Prototype
            var catalogModule = new ProductCatalog();
            catalogModule.ListProducts();
            catalogModule.AddProduct("Smartwatch", 69);
            catalogModule.SearchProduct("smart");
            catalogModule.RemoveProduct(2);
            catalogModule.ListProducts();
        }
    }
}

