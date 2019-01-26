using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TSM.Helpers;
using TSM.Models;
using Task = System.Threading.Tasks.Task;

namespace TSM.Services
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient client;

        public TeamService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            client.BaseAddress = new Uri(Settings.BaseAddress);
        }

        public Task<Team> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> GetAllForUser()
        {
            var response = await client.GetAsync("teams/all");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<Team>>(await response.Content.ReadAsStringAsync());
        }

        public async Task Add(Team team)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "teams")
            {
                Content = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Team team)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "teams")
            {
                Content = new StringContent(JsonConvert.SerializeObject(team), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "teams")
            {
                Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
            };
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
