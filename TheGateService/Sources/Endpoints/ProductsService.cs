using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Security;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Authenticate]
    [RequiredPermission(ApplyTo.Put | ApplyTo.Post, Permissions.CanManageProducts)]
    [RequiredPermission(ApplyTo.Delete, Permissions.CanDeleteProducts)]
    public class ProductService : Service {
        private static readonly ProductDbProvider Products = new ProductDbProvider();

        public object Get(Product request) {
            var p = Products.Get(request.Id);
            if (p == null) throw HttpError.NotFound("Product {0} was not found.".Fmt(request.Id));
            return new ProductResponse { Product = p };
        }

        public object Get(Products request) {
            return new ProductsResponse { Results = Products.GetAll() };
        }

        public object Post(Product request) {
            var id = Products.Create(request);
            request.Id = id;

            // TODO: return HTTP Status 201: Created
            return new ProductResponse { Product = request };
        }

        public object Put(Product request) {
            Products.Update(request);
            // TODO: return HTTP Status 204: No Content
            return null;
        }

        public object Delete(Product request) {
            Products.Delete(request.Id);
            // TODO: return HTTP Status 200: OK
            return null;
        }
    }
}
