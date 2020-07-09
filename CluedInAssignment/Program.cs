using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using CluedInAssignment.Models;
using System.Linq;

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

            if (!response.IsSuccessful)
            {
                Console.WriteLine("Error: Request unsuccessful");
                Console.WriteLine(response.ErrorMessage);
                return;
            }

            List<Company> companies;
            try
            {
                companies = JsonConvert.DeserializeObject<List<Company>>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Data format invalid");
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine(createCompaniesTableString(companies));
        }

        private static string createCompaniesTableString(List<Company> companies)
        {
            string[][] table = createCompaniesTable(companies);
            int n_rows = table.Length;
            int n_cols = table[0].Length;

            var colWidths = new int[n_cols];
            for (int i = 0; i < n_cols; i++)
            {
                colWidths[i] = determineColumnWidth(table.Select(row => row[i]));
            }

            string[] headerRow = new string[]
            {
                "Company name",
                "Website",
                "Contact email",
                "VAT no.",
                "Country",
                "City",
                "Street"
            };

            string tableString = "";
            tableString += createRowString(headerRow, colWidths);
            tableString += new string('-', colWidths.Sum()) + '\n';
            foreach (var row in table)
            {
                tableString += createRowString(row, colWidths);
            }
            return tableString;
        }

        private static string[][] createCompaniesTable(List<Company> companies)
        {
            var table = companies.Select(company =>
                new string[] {
                    company.Name,
                    company.WebsiteUrl,
                    company.ContactEmail,
                    company.VatNumber,
                    company.Address.Country,
                    company.Address.City,
                    company.Address.Street
                }
            ).ToArray();
            return table;
        }

        private static int determineColumnWidth(IEnumerable<string> values)
        {
            var maxLength = values.Max(v => v.Length);
            return maxLength + 3;
        }

        private static string createRowString(string[] row, int[] colWidths)
        {
            string rowString = "";
            for (int i = 0; i < row.Length; i++)
            {
                rowString += row[i].PadRight(colWidths[i], ' ');
            }
            rowString += '\n';
            return rowString;
        }

    }
}
