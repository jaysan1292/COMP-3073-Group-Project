using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServiceStack.Common.ServiceClient.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

using TheGateService.Types;

namespace TheGateTests {
    [TestClass]
    public class ProductServiceTests {
        private const string ServiceUrl = "http://localhost:3073";
        private static JsonServiceClient _client;

        public TestContext TestContext { get; set; }

        #region Additional test attributes

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext) {
            _client = new JsonServiceClient(ServiceUrl) {
                StoreCookies = true,
            };
            _client.Post(new Auth {
                UserName = "jsmith@example.com",
                Password = "123456",
            });
            TestHelper.ResetDatabase();
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void ClassCleanup() { }

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestInitialize() { }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void TestCleanup() {
            // Reset the database before running each test to ensure a consistent state
            TestHelper.ResetDatabase();
        }

        #endregion

        [TestMethod]
        public void TestGetProduct() {
            var expected = new Product {
                Id = 1,
                Name = "Video Card",
                Description = "Video card for PC",
                Quantity = 37,
                Price = new decimal(159.99),
                Featured = true,
                Showcase = true,
            };
            var response = _client.Get(new Product { Id = 1 });
            TestContext.WriteLine("Expected: {0}\n", expected.Dump());
            TestContext.WriteLine("Actual: {0}\n", response.Product.Dump());
            Assert.AreEqual(expected, response.Product);
        }

        [TestMethod]
        public void TestGetNonExistentProduct() {
            Action<long> x = value => {
                try {
                    _client.Get(new Product { Id = value });
                } catch (WebServiceException e) {
                    Assert.IsTrue(e.Message.Is("Not Found", "Bad Request"),
                                  "Failed accessing product {0}. Message received: {1}", value, e.Message);
                }
            };
            x(9001);
            x(-1);
        }

        [TestMethod]
        public void TestGetProducts() {
            var response = _client.Get(new Products());
            TestContext.WriteLine("{0}", response.Dump());
            Assert.IsTrue(response.Results.Count == 8);
        }

        [TestMethod]
        public void TestAddProduct() {
            var expected = new Product {
                Name = "37.65 GHz Processor",
                Description = "A CPU for the future!",
                Quantity = 12,
                Price = new decimal(599.99),
                Featured = false,
                Showcase = false,
            };
            var newprod = _client.Post(expected);
            expected.Id = newprod.Product.Id;
            var test = _client.Get(new Product { Id = expected.Id });
            Assert.AreEqual(expected, test.Product);
        }

        [TestMethod]
        public void TestUpdateProduct() {
            var expected = new Product {
                Id = 1,
                Name = "Video Card (modified)",
                Description = "Video card for PC",
                Quantity = 38,
                Price = new decimal(159.99),
                Featured = true,
                Showcase = false,
            };
            _client.Put(expected);

            var updated = _client.Get(new Product { Id = 1 });
            Assert.AreEqual(expected, updated.Product);
        }

        [TestMethod]
        [ExpectedException(typeof(WebServiceException))]
        public void TestUpdateProductBadQuantity() {
            var expected = new Product {
                Id = 1,
                Name = "Video Card (modified)",
                Description = "Video card for PC",
                Quantity = -1,
                Price = new decimal(159.99),
                Featured = true,
                Showcase = false,
            };
            _client.Put(expected);
        }

        [TestMethod]
        public void TestDeleteProduct() {
            _client.Delete(new Product { Id = 8 });
            try {
                _client.Get(new Product { Id = 8 });
            } catch (WebServiceException e) {
                Assert.IsTrue(e.Message.Is("Not Found"));
            }
        }

        [TestMethod]
        public void TestDeleteNonExistentProduct() {
            Action<long> x = value => {
                try {
                    _client.Delete(new Product { Id = value });
                } catch (WebServiceException e) {
                    Assert.IsTrue(e.Message.Is("Not Found", "Bad Request"),
                                  "Failed deleting product {0}. Message received: {1}", value, e.Message);
                }
            };
            x(9001);
            x(-1);
        }
    }
}
