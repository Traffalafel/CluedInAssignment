﻿using Newtonsoft.Json;
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
        public string Website { get; set; }

        [JsonProperty(PropertyName = "contactEmail")]
        public string ContactEmail { get; set; }

        [JsonProperty(PropertyName = "vatNumber")]
        public string VatNumber { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}
