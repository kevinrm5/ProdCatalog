using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the CSV file path.");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        await ImportCsvData(filePath, httpClient);
    }

    static async Task ImportCsvData(string filePath, HttpClient client)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<ProductCsvModel>();

        foreach (var record in records)
        {
            var response = await client.PostAsJsonAsync("products", new
            {
                Name = record.ProductName,
                Code = record.ProductCode,
                CategoryCode = record.CategoryCode
            });

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to add product {record.ProductCode}. Error: {await response.Content.ReadAsStringAsync()}");
            }
            else
            {
                Console.WriteLine($"Successfully added: {record.ProductName}");
            }
        }
    }
}

public class ProductCsvModel
{
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string CategoryName { get; set; }
    public string CategoryCode { get; set; }
}
