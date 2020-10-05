using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArmutLocalStackSample.FunctionalTests.Extensions
{
    internal static class HttpContentExtensions
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        internal static async Task<T> GetAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, SerializerOptions);
        }
    }
}