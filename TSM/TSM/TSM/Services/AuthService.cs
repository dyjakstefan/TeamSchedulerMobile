using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TSM.Dto;
using TSM.Helpers;
using TSM.Models;

namespace TSM.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.BaseAddress = new Uri(Settings.BaseAddress);
        }

        public async Task<Jwt> CreateUser(UserDto user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "users")
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Jwt>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Jwt> Login(string email, string password)
        {
            var guid = Guid.NewGuid();
            var request = new HttpRequestMessage(HttpMethod.Post, "users/login")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new {email, password, guid}), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Jwt>(await response.Content.ReadAsStringAsync());
        }
    }
}
