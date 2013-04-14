using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
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
}
