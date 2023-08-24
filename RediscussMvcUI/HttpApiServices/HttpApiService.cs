using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace AhlatciProject.MvcUi.Areas.Admin.HttpApiServices
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HttpApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<T> DeleteData<T>(string endPoint, string token = null)
        {

            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{baseAddress.Value}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                }
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
                JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> GetData<T>(string endPoint, string token = null)
        {
            var client = _httpClientFactory.CreateClient();

            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseAddress.Value}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                },
            };
  
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response =
                JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PostData<T>(string endPoint, string jsonData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{baseAddress.Value}{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PostData<T>(string endPoint, HttpContent customData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{baseAddress.Value}{endPoint}"),
                Headers =
                {
                    { HeaderNames.Accept, customData.Headers.ContentType.MediaType }
                },
                Content = customData
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var responseMessage = await client.PostAsync(new Uri($"{baseAddress.Value}/{endPoint}"), customData);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PutData<T>(string endPoint, string jsonData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");

            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{baseAddress.Value}/{endPoint}"),
                Headers =
                {
                    {HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.SendAsync(requestMessage);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PutData<T>(string endPoint, HttpContent customData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");
            endPoint = endPoint.TrimStart('/');

            var responseMessage = await client.PutAsync(endPoint, customData);

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PatchData<T>(string endPoint, HttpContent customData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var baseAddress = _configuration.GetSection("ApiServices:BaseAddress");
            endPoint = endPoint.TrimStart('/');

            var requestUri = new Uri($"{baseAddress.Value}/{endPoint}");

            var responseMessage = await client.PatchAsync(requestUri, customData);
            

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response;
        }

    }
}
