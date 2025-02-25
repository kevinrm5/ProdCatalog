using System.Net.Http.Json;
using ProductConsoleApp.Helpers;
using ProductModel;
namespace ProductConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string csvFilePath = args[0];

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine($"Error: File '{csvFilePath}' not found.");
                return;
            }

            List<ProductCategoryDto> products = ReadCsv(csvFilePath);

            if (products.Count == 0)
            {
                Console.WriteLine("No valid products found in the CSV file.");
                return;
            }

            Console.Write("Continue on error? (yes/no): ");
            string userChoice = Console.ReadLine()?.Trim().ToLower();
            bool continueOnError = userChoice == "yes";

            await SendDataToApi(products, continueOnError);
        }

        static async Task SendDataToApi(List<ProductCategoryDto> products, bool continueOnError)
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
                        Logger.LogError($"Failed to add products. Status: {response.StatusCode}, Error: {errorMsg}");

                        if (!continueOnError)
                        {
                            Console.WriteLine("Stopping import due to error.");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Exception occurred: {ex.Message}");

                    if (!continueOnError)
                    {
                        Console.WriteLine("Stopping import due to exception.");
                        return;
                    }
                }
            }
        }

        public static List<ProductCategoryDto> ReadCsv(string csvFilePath)
        {
            var products = new List<ProductCategoryDto>();
            var lines = File.ReadAllLines(csvFilePath).Skip(1);

            foreach (var line in lines)
            {
                var values = line.Split(',');

                if (values.Length != 4)
                {
                    Console.WriteLine($"Skipping invalid row: {line}");
                    continue;
                }

                products.Add(new ProductCategoryDto
                {
                    ProductName = values[0].Trim(),
                    ProductCode = values[1].Trim(),
                    CategoryName = values[2].Trim(),
                    CategoryCode = values[3].Trim()
                });
            }

            return products;
        }
    }
}