using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServiceStack.ServiceClient.Web;

namespace TheGateTests {
    public class TestBase {
        public const string ServiceUrl = "http://localhost:3073";
        public static JsonServiceClient Client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) {
            TestHelper.ResetDatabase();
            Client = new JsonServiceClient(ServiceUrl) {
                StoreCookies = true,
            };
        }
    }
}