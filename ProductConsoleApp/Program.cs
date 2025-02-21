using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: ProductManagementConsole <CSV_FILE_PATH>");
            return;
        }

        string csvFilePath = args[0];

        if (!File.Exists(csvFilePath))
        {
            Console.WriteLine($"Error: File '{csvFilePath}' not found.");
            return;
        }

        List<Product> products = ReadCsv(csvFilePath);

        if (products.Count == 0)
        {
            Console.WriteLine("No valid products found in the CSV file.");
            return;
        }

        await SendDataToApi(products);
    }

    static List<Product> ReadCsv(string filePath)
    {
        var products = new List<Product>();
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values.Length == 4)
                {
                    products.Add(new Product
                    {
                        ProductName = values[0],
                        ProductCode = values[1],
                        CategoryName = values[2],
                        CategoryCode = values[3]
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV: {ex.Message}");
        }
        return products;
    }

    static async Task SendDataToApi(List<Product> products)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:44352/");

            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/products-bulk", products);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully added all products.");
                }
                else
                {
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add products. Status: {response.StatusCode}, Error: {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }
    }
}

class Product
{
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string CategoryName { get; set; }
    public string CategoryCode { get; set; }
}
