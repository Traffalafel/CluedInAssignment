using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CluedInAssignment.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }
    }
}
