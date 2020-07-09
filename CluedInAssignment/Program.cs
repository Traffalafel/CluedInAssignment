using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using CluedInAssignment.Models;

namespace CluedInAssignment
{
    class Program
    {
        private static string companiesTable(List<Company> companies)
        {
            int width = 73;
            string table = "";
            
            // Header
            table += string.Format(" {0,-40} {1,-20} {2,-10}\n", "Company name", "Website URL", "VAT number");
            table += new string('-', width);
            table += "\n";
            
            foreach (var company in companies)
            {
                table += string.Format(" {0,-40} {1,-20} {2,-10}\n", company.Name, company.WebsiteUrl, company.VatNumber);
            }
            return table;
        }

        static void Main()
        {
            string domain = "https://my.api.mockaroo.com";
            string resource = "cluedin_assignment_2020.json";
            string token = "91bc8540";

            var client = new RestClient(domain);
            client.AddDefaultHeader("x-api-key", token);

            var request = new RestRequest(resource);
            var response = client.Get(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine("Error: Server response invalid");
                return;
            }

            List<Company> companies;
            try
            {
                companies = JsonConvert.DeserializeObject<List<Company>>(response.Content);
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Data invalid");
                return;
            }

            Console.WriteLine(companiesTable(companies));

        }
    }
}
