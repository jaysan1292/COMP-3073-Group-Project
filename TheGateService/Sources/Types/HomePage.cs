// Project: TheGateService
// Filename: HomePage.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

using TheGateService.Endpoints;
using TheGateService.Extensions;

namespace TheGateService.Types {
    [Route("/home")]
    public class HomePage {
        public static List<Product> FeaturedItems { get; set; }

        static HomePage() {
            FeaturedItems = new List<Product>();
            // Get 3 random items from the All Products list
            Product temp;
            while (!FeaturedItems.Contains((temp = ProductService.SampleProducts.GetRandom())) &&
                   FeaturedItems.Count < 3)
                FeaturedItems.Add(temp);
        }
    }
}
