using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Estudos.DTO;
using Estudos.Interfaces.Services;

namespace Estudos.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static readonly HttpClient _client = new HttpClient();
        
        private static readonly string _baseUrl = "https://jsonplaceholder.typicode.com";
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<IEnumerable<OutboundPost>> GetAllPostsAsync()
        {
            var url = $"{_baseUrl}/users/1/posts";
            
            using var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<IEnumerable<OutboundPost>>(body, _options);
            
            return result;
        }

        public async Task<OutboundPost> GetPostByIdAsync(int id)
        {
            var url = $"{_baseUrl}/posts/{id}";
            
            using var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OutboundPost>(body, _options);
            
            return result;
        }

        public async Task<OutboundPost> AddPostAsync(InboundAddPost data)
        {
            var url = $"{_baseUrl}/posts";

            var body = new StringContent(
                JsonSerializer.Serialize(data, _options),
                Encoding.UTF8,
                "application/json");

            using var response = await _client.PostAsync(url, body);

            response.EnsureSuccessStatusCode();

            var resultBody = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OutboundPost>(resultBody, _options);
            
            return result;
        }

        public async Task<OutboundPost> UpdatePostAsync(int id, InboundUpdatePost data)
        {
            var url = $"{_baseUrl}/posts/{id}";

            var body = new StringContent(
                JsonSerializer.Serialize(data, _options),
                Encoding.UTF8,
                "application/json");

            using var response = await _client.PutAsync(url, body);

            response.EnsureSuccessStatusCode();

            var resultBody = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<OutboundPost>(resultBody, _options);
            
            return result;
        }

        public async Task DeletePostByIdAsync(int id)
        {
            var url = $"{_baseUrl}/posts/{id}";
            
            using var response = await _client.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}
