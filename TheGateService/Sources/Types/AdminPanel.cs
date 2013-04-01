using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace TheGateService.Types
{
    [Route("/admin")]
    public class AdminPanel
    {
        public List<Product> Products { get; set; }
    }
}