using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CluedInAssignment.Models
{
    public class Company
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string WebsiteUrl { get; set; }

        [JsonProperty(PropertyName = "vatNumber")]
        public string VatNumber { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}
