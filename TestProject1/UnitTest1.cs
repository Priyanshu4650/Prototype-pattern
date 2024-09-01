using NUnit.Framework;
using EcommercePrototype;
using System.Linq;

namespace EcommercePrototype.Tests
{
    [TestFixture]
    public class ProductCatalogTests
    {
        private ProductCatalog _catalog;

        [SetUp]
        public void Setup()
        {
            _catalog = new ProductCatalog();
        }

        [Test]
        public void AddProduct_ShouldAddProductToCatalog()
        {
            bool result = _catalog.AddProduct("Test Product", 19.99);

            Assert.IsTrue(result);
            var product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.IsNotNull(product);
            Assert.AreEqual("Test Product", product.Name);
            Assert.AreEqual(19.99, product.Price);
        }

        [Test]
        public void RemoveProduct_ShouldRemoveProductFromCatalog()
        {
            _catalog.AddProduct("Test Product", 19.99);
            var product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.IsNotNull(product);

            bool result = _catalog.RemoveProduct(product.Id);

            Assert.IsTrue(result);
            product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.IsNull(product);
        }

        [Test]
        public void RemoveProduct_ShouldReturnFalseIfProductNotFound()
        {
            bool result = _catalog.RemoveProduct(999); // Non-existent product ID

            Assert.IsFalse(result);
        }

        [Test]
        public void ListProducts_ShouldListAllProducts()
        {
            _catalog.AddProduct("Product 1", 10.00);
            _catalog.AddProduct("Product 2", 20.00);

            var products = _catalog.ListProducts();

            Assert.AreEqual(2, products.Count);
            Assert.IsTrue(products.Any(p => p.Name == "Product 1"));
            Assert.IsTrue(products.Any(p => p.Name == "Product 2"));
        }

        [Test]
        public void SearchProduct_ShouldFindMatchingProducts()
        {
            _catalog.AddProduct("Product One", 10.00);
            _catalog.AddProduct("Product Two", 20.00);

            var products = _catalog.SearchProduct("One");

            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Product One", products.First().Name);
        }

        [Test]
        public void SearchProduct_ShouldReturnEmptyIfNoMatch()
        {
            _catalog.AddProduct("Product One", 10.00);

            var products = _catalog.SearchProduct("Nonexistent");

            Assert.AreEqual(0, products.Count);
        }
    }

    [TestFixture]
    public class UserAuthenticationTests
    {
        private UserAuthentication _auth;

        [SetUp]
        public void Setup()
        {
            _auth = new UserAuthentication();
        }

        [Test]
        public void RegisterUser_ShouldAddUserToDatabase()
        {
            bool result = _auth.RegisterUser("testuser", "password123");

            Assert.IsTrue(result);
        }

        [Test]
        public void RegisterUser_ShouldNotAllowDuplicateUsername()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.RegisterUser("testuser", "password456");

            Assert.IsFalse(result);
        }

        [Test]
        public void AuthenticateUser_ShouldAuthenticateValidUser()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.AuthenticateUser("testuser", "password123");

            Assert.IsTrue(result);
        }

        [Test]
        public void AuthenticateUser_ShouldNotAuthenticateInvalidPassword()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.AuthenticateUser("testuser", "wrongpassword");

            Assert.IsFalse(result);
        }

        [Test]
        public void AuthenticateUser_ShouldNotAuthenticateNonexistentUser()
        {
            bool result = _auth.AuthenticateUser("nonexistentuser", "password123");

            Assert.IsFalse(result);
        }

        [Test]
        public void ListUsers_ShouldListAllUsers()
        {
            _auth.RegisterUser("user1", "password1");
            _auth.RegisterUser("user2", "password2");

            var users = _auth.ListUsers();

            Assert.AreEqual(2, users.Count);
            Assert.Contains("user1", users);
            Assert.Contains("user2", users);
        }

        [Test]
        public void ListUsers_ShouldReturnEmptyIfNoUsers()
        {
            var users = _auth.ListUsers();

            Assert.AreEqual(0, users.Count);
        }
    }
}
