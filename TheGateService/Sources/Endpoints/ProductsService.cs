using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Security;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Authenticate(ApplyTo.Put | ApplyTo.Post | ApplyTo.Delete)]
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

            return new HttpResult(new ProductResponse { Product = request }, HttpStatusCode.Created);
        }

        public object Put(Product request) {
            Products.Update(request);
            return new HttpResult(HttpStatusCode.NoContent);
        }

        public object Delete(Product request) {
            return Products.Delete(request.Id) ?
                       (object) new HttpResult(HttpStatusCode.OK, "") :
                       HttpError.NotFound("Product {0} was not found, so it could not be deleted.".Fmt(request.Id));
        }
    }
}
