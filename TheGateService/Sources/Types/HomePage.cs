using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

using TheGateService.Database;
using TheGateService.Extensions;

namespace TheGateService.Types {
    [Route("/home")]
    public class HomePage {
        public static List<Product> FeaturedItems { get; set; }

        static HomePage() {
            var prods = new ProductDbProvider().GetAll();
            FeaturedItems = new List<Product>();
            // Get 3 random items from the All Products list
            Product temp;
            while (!FeaturedItems.Contains((temp = prods.GetRandom())) &&
                   FeaturedItems.Count < 3)
                FeaturedItems.Add(temp);
        }
    }
}
