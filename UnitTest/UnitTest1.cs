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

            Assert.That(result, Is.True);

            var product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.That(product, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo("Test Product"));
            Assert.That(product.Price, Is.EqualTo(19.99));
        }

        [Test]
        public void RemoveProduct_ShouldRemoveProductFromCatalog()
        {
            _catalog.AddProduct("Test Product", 19.99);
            var product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.That(product, Is.Not.Null);

            bool result = _catalog.RemoveProduct(product.Id);

            Assert.That(result, Is.True);

            product = _catalog.ListProducts().FirstOrDefault(p => p.Name == "Test Product");
            Assert.That(product, Is.Null);
        }

        [Test]
        public void RemoveProduct_ShouldReturnFalseIfProductNotFound()
        {
            bool result = _catalog.RemoveProduct(999); // Non-existent product ID

            Assert.That(result, Is.False);
        }

        [Test]
        public void ListProducts_ShouldListAllProducts()
        {
            _catalog.AddProduct("Product 1", 10.00);
            _catalog.AddProduct("Product 2", 20.00);

            var products = _catalog.ListProducts();

            Assert.That(products.Count, Is.EqualTo(2));
            Assert.That(products, Has.Some.Property("Name").EqualTo("Product 1"));
            Assert.That(products, Has.Some.Property("Name").EqualTo("Product 2"));
        }

        [Test]
        public void SearchProduct_ShouldFindMatchingProducts()
        {
            _catalog.AddProduct("Product One", 10.00);
            _catalog.AddProduct("Product Two", 20.00);

            var products = _catalog.SearchProduct("One");

            Assert.That(products.Count, Is.EqualTo(1));
            Assert.That(products.First().Name, Is.EqualTo("Product One"));
        }

        [Test]
        public void SearchProduct_ShouldReturnEmptyIfNoMatch()
        {
            _catalog.AddProduct("Product One", 10.00);

            var products = _catalog.SearchProduct("Nonexistent");

            Assert.That(products.Count, Is.EqualTo(0));
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

            Assert.That(result, Is.True);
        }

        [Test]
        public void RegisterUser_ShouldNotAllowDuplicateUsername()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.RegisterUser("testuser", "password456");

            Assert.That(result, Is.False);
        }

        [Test]
        public void AuthenticateUser_ShouldAuthenticateValidUser()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.AuthenticateUser("testuser", "password123");

            Assert.That(result, Is.True);
        }

        [Test]
        public void AuthenticateUser_ShouldNotAuthenticateInvalidPassword()
        {
            _auth.RegisterUser("testuser", "password123");
            bool result = _auth.AuthenticateUser("testuser", "wrongpassword");

            Assert.That(result, Is.False);
        }

        [Test]
        public void AuthenticateUser_ShouldNotAuthenticateNonexistentUser()
        {
            bool result = _auth.AuthenticateUser("nonexistentuser", "password123");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ListUsers_ShouldListAllUsers()
        {
            _auth.RegisterUser("user1", "password1");
            _auth.RegisterUser("user2", "password2");

            var users = _auth.ListUsers();

            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users, Does.Contain("user1"));
            Assert.That(users, Does.Contain("user2"));
        }

        [Test]
        public void ListUsers_ShouldReturnEmptyIfNoUsers()
        {
            var users = _auth.ListUsers();

            Assert.That(users.Count, Is.EqualTo(0));
        }
    }
}
