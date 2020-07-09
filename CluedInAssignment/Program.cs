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

            IEnumerable<Company> companies;
            try
            {
                companies = JsonConvert.DeserializeObject<IEnumerable<Company>>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Data format invalid");
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine(createCompaniesTableString(companies));
        }

        private static string createCompaniesTableString(IEnumerable<Company> companies)
        {
            string[][] table = createCompaniesTable(companies);

            int n_cols = table[0].Length;
            var colWidths = new int[n_cols];
            for (int i = 0; i < n_cols; i++)
            {
                var col = table.Select(row => row[i]).ToArray();
                colWidths[i] = determineColumnWidth(col);
            }

            var headerRow = new string[]
            {
                "Company name",
                "Website",
                "Contact email",
                "Country",
                "City",
                "Street",
                "VAT no."
            };

            string tableString = "";
            tableString += createRowString(headerRow, colWidths);
            tableString += new string('-', colWidths.Sum() - 2) + '\n';
            foreach (var row in table)
            {
                tableString += createRowString(row, colWidths);
            }
            return tableString;
        }

        private static string[][] createCompaniesTable(IEnumerable<Company> companies, string nullSubstitute="<missing>")
        {
            var table = companies.Select(company =>
                new string[] {
                    company.Name,
                    company.Website,
                    company.ContactEmail,
                    company.Address.Country,
                    company.Address.City,
                    company.Address.Street,
                    company.VatNumber
                }
            ).ToArray();

            // Replace all nulls with substitute value
            for (int row_i = 0; row_i < table.Length; row_i++)
            {
                for (int col_i = 0; col_i < table[0].Length; col_i++)
                {
                    if (table[row_i][col_i] == null)
                    {
                        table[row_i][col_i] = nullSubstitute;
                    }
                }

            }
            
            return table;
        }

        private static int determineColumnWidth(string[] values)
        {
            var maxLength = values.Max(v => v.Length);
            return maxLength + 2;
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
