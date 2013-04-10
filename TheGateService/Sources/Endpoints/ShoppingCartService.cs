using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace TheGateService.Endpoints {
    [Authenticate]
    [RequiredRole("User")]
    public class ShoppingCartService : Service {
        public object Get(ShoppingCart request) {
            return new ShoppingCartResponse { Cart = new ShoppingCart { } };
        }

        public object Post(ShoppingCart.ShoppingCartItem request) {
            return new HttpResult(HttpStatusCode.OK, "");
        }
    }
}
