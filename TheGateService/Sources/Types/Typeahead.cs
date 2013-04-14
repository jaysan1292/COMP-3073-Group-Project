using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [DataContract]
    [Route("/typeahead", "GET")]
    public class Typeahead {
        [DataMember(Name = "q")]
        public string Query { get; set; }
    }
}
