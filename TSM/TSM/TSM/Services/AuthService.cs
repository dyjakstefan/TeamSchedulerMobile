using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TSM.Dto;

namespace TSM.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.BaseAddress = new Uri("http://192.168.1.65:45455/api/");
        }

        public async Task CreateUser(UserDto user)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "users")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                Debug.WriteLine("Response: {0}", response.StatusCode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<JwtDto> Login(string email, string password)
        {
            var guid = Guid.NewGuid();
            var request = new HttpRequestMessage(HttpMethod.Post, "users/login")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new {email, password, guid}), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<JwtDto>(await response.Content.ReadAsStringAsync());
        }
    }
}
