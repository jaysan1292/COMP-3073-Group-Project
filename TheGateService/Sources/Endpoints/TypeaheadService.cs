using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

using TheGateService.Database;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    [DataContract]
    [Route("/typeahead", "GET")]
    public class Typeahead {
        [DataMember(Name = "q")]
        public string Query { get; set; }
    }

    public class TypeaheadResponse : ResponseBase {
        public TypeaheadResponse() {
            Results = new List<NameUrlPair>();
        }

        public TypeaheadResponse(List<NameUrlPair> results) {
            Results = results;
        }

        public List<NameUrlPair> Results { get; set; }

        public class NameUrlPair {
            public string Name { get; set; }
            public string Url { get; set; }

            public NameUrlPair(string name, string url) {
                Name = name;
                Url = url;
            }
        }
    }

    public class TypeaheadService : Service {
        private static readonly ProductDbProvider Products = new ProductDbProvider();

        public object Get(Typeahead request) {
            var results = Products.Search(request.Query);

            return results.Count != 0 ?
                       new TypeaheadResponse(results.Select(x => new TypeaheadResponse.NameUrlPair(x.Name, "{0}/products/{1}".Fmt(Request.GetApplicationUrl(), x.Id))).ToList()) :
                       new TypeaheadResponse();
        }
    }
}
