using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServiceStack.Common.ServiceClient.Web;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;

namespace TheGateTests {
    [TestClass]
    public class AuthenticationTests : TestBase {
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        [ClassInitialize]
        public new static void ClassInitialize(TestContext context) {
            TestBase.ClassInitialize(context);
        }

        [ClassCleanup]
        public static void ClassCleanup() { }

        [TestInitialize]
        public void TestInitialize() { }

        [TestCleanup]
        public void TestCleanup() {
            Client.Post(new Auth { provider = "logout" });
        }

        #endregion

        [TestMethod]
        public void TestAuthenticateWithCorrectCredentials() {
            const string username = "jsmith@example.com";
            const string password = "123456";

            var response = Client.Post(new Auth { UserName = username, Password = password });

            Assert.IsTrue(!response.SessionId.IsNullOrEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(WebServiceException))]
        public void TestAuthenticateWithIncorrectEmail() {
            const string username = "jsmith@example.cpm";
            const string password = "123456";

            Client.Post(new Auth { UserName = username, Password = password });
        }

        [TestMethod]
        [ExpectedException(typeof(WebServiceException))]
        public void TestAuthenticateWithIncorrectPassword() {
            const string username = "jsmith@example.com";
            const string password = "qwertyuiop";

            Client.Post(new Auth { UserName = username, Password = password });
        }
    }
}
