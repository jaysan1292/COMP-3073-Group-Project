using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

using TheGateService.Database;

namespace TheGateService.Endpoints {
    [Authenticate]
    [RequiredRole("User")]
    public class ShoppingCartService : Service {
        private static readonly ShoppingCartDbProvider ShoppingCarts = new ShoppingCartDbProvider();

        public object Get(ShoppingCart request) {
            var session = this.GetSession();
            var id = Convert.ToInt64(session.UserAuthId);
            return new ShoppingCartResponse { Cart = ShoppingCarts.Get(id) };
        }

        public object Post(ShoppingCart.ShoppingCartItem request) {
            return new HttpResult(HttpStatusCode.OK, "");
        }
    }
}
