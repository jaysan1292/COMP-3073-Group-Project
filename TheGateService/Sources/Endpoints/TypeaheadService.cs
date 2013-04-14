using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;
using ServiceStack.Text;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class TypeaheadService : GateServiceBase {
        private static readonly ProductDbProvider Products = new ProductDbProvider();

        public object Get(Typeahead request) {
            var results = Products.Search(request.Query);

            return results.Count != 0 ?
                       new TypeaheadResponse(results.Select(x => new TypeaheadResponse.NameUrlPair(x.Name, "{0}/products/{1}".Fmt(Request.GetApplicationUrl(), x.Id))).ToList()) :
                       new TypeaheadResponse();
        }
    }
}
