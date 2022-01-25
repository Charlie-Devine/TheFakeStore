using TheFakeStore.Services.Abstract;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace TheFakeStore.Services.Concrete
{
    public class HttpService : IHttpService
    {
        private HttpClient _HttpClient;

        public HttpService(HttpClient HttpClient)
        {
            _HttpClient = HttpClient ?? throw new ArgumentNullException(nameof(HttpClient));
        }


        public async Task<T> Get<T>(string uri)
        {
            var request = CreateRequest(HttpMethod.Get, uri);
            return await SendRequest<T>(request);

            
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);
            return await SendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Put, uri);
            return await SendRequest<T>(request);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object value = null)
        {
            var request = new HttpRequestMessage(method, uri);
            if (value != null)
                request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json" );
            return request;
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            using var response = await _HttpClient.SendAsync(request);

            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error["message"]);
            }

            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            options.Converters.Add(new JsonStringEnumConverter());
            return await response.Content.ReadFromJsonAsync<T>(options);

        }

    }
}
