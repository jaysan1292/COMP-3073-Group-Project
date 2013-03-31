// Project: TheGateService
// Filename: ProductsService.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class ProductService : Service {
        private static ProductDbProvider _provider = new ProductDbProvider();

        public object Get(Product request) {
            return new ProductResponse { Product = _provider.Get(request.Id) };
        }

        public object Get(Products request) {
            return new ProductsResponse { Results = _provider.GetAll() };
        }
    }
}
