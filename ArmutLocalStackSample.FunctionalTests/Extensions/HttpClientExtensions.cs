using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace ArmutLocalStackSample.FunctionalTests.Extensions
{
    internal static class HttpClientExtensions
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        internal static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T model)
        {
            var json = JsonSerializer.Serialize<T>(model, SerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return client.PostAsync(requestUri, content);
        }

        internal static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, string requestUri, T model)
        {
            var json = JsonSerializer.Serialize<T>(model, SerializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return client.PatchAsync(requestUri, content);
        }
    }
}