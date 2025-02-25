using System.Net.Http;
using System.Threading.Tasks;

namespace ProductConsoleApp.Helpers
{
    public static class ApiHelper
    {
        public static async Task<bool> ProductExists(HttpClient client, string productCode)
        {
            HttpResponseMessage response = await client.GetAsync($"api/products/{productCode}");
            return response.IsSuccessStatusCode;
        }
    }
}
