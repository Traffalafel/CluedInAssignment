using System;
using RestSharp;
using Newtonsoft.Json;

namespace CluedInAssignment
{
    class Program
    {
        static void Main()
        {
            string domain = "https://my.api.mockaroo.com";
            string resource = "cluedin_assignment_2020.json";
            string token = "91bc8540";

            var client = new RestClient(domain);
            client.AddDefaultHeader("x-api-key", token);

            var request = new RestRequest(resource);
            var response = client.Get(request);

            var thing = JsonConvert.DeserializeObject<object>(response.Content);
        }
    }
}
