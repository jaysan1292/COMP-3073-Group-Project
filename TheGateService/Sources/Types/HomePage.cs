using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [Route("/home")]
    public class HomePage {
        public List<Product> Featured;
        public List<Product> Showcase;

        public HomePage() {
            Featured = new List<Product>();
            Showcase = new List<Product>();
        }
    }
}
