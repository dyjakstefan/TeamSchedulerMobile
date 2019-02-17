using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TSM.Helpers;

namespace TSM.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            client.BaseAddress = new Uri(Settings.BaseAddress);
        }

        public bool UpdateAuthorizationHeader()
        {
            if (!string.IsNullOrWhiteSpace(Settings.AccessToken))
                return false;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            return true;

        }

        public async Task<T> Get<T>(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Get<T>(int id, string url)
        {
            var response = await client.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<T>> GetAll<T>(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<T>> GetAll<T>(int id, string url)
        {
            var response = await client.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
        }

        public async Task Add<T>(T payload, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update<T>(T payload, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete<T>(T payload, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
