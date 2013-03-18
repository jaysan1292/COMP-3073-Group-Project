// Project: TheGateService
// Filename: ProductsService.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;

using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class ProductService : Service {
        public static List<Product> SampleProducts = new List<Product> {
            new Product { Id = 1, Name = "Product 1", Description = "Product 1 Description", Price = (decimal) 57.99 },
            new Product { Id = 2, Name = "Product 2", Description = "Product 2 Description", Price = (decimal) 99.99 },
            new Product { Id = 3, Name = "Product 3", Description = "Product 3 Description", Price = (decimal) 17.99 },
            new Product { Id = 4, Name = "Product 4", Description = "Product 4 Description", Price = (decimal) 24.99 },
            new Product { Id = 5, Name = "Product 5", Description = "Product 5 Description", Price = (decimal) 37.99 },
            new Product { Id = 6, Name = "Product 6", Description = "Product 6 Description", Price = (decimal) 62.99 },
            new Product { Id = 7, Name = "Product 7", Description = "Product 7 Description", Price = (decimal) 65.99 },
            new Product { Id = 8, Name = "Product 8", Description = "Product 8 Description", Price = (decimal) 97.99 },
            new Product { Id = 9, Name = "Product 9", Description = "Product 9 Description", Price = (decimal) 11.99 },
        };

        public object Get(Product request) {
            return new ProductResponse { Product = SampleProducts[(int) request.Id - 1] };
        }

        public object Get(Products request) {

            return new ProductsResponse { Results = SampleProducts };
        }
    }
}
