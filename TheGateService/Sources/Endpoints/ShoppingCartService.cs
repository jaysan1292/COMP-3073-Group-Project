using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Authenticate]
    [RequiredRole("User")]
    public class ShoppingCartService : GateServiceBase {
        private static readonly ShoppingCartDbProvider ShoppingCarts = new ShoppingCartDbProvider();

        private long UserId { get { return Convert.ToInt64(this.GetSession().UserAuthId); } }

        public object Get(ShoppingCart request) {
            return new ShoppingCartResponse { Cart = ShoppingCarts.Get(UserId) ?? new ShoppingCart() };
        }

        public object Put(ShoppingCart request) {
            var success = ShoppingCarts.Update(request);

            if (success) return new HttpResult(HttpStatusCode.NoContent, "");
            return new HttpError(HttpStatusCode.BadRequest, "");
        }

        public object Post(ShoppingCart.ShoppingCartItem request) {
            var success = ShoppingCarts.AddToCart(UserId, request.Product.Id, request.Quantity);

            if (success) return new HttpResult(HttpStatusCode.Created, "");
            return new HttpError(HttpStatusCode.BadRequest, "");
        }

        public object Delete(ShoppingCart.ShoppingCartItem request) {
            var success = ShoppingCarts.RemoveFromCart(UserId, request.Product.Id);

            if (success) return new HttpResult(HttpStatusCode.NoContent, "");
            return new HttpError(HttpStatusCode.BadRequest, "");
        }
    }
}
