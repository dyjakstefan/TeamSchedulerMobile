using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;

namespace TSM.Services
{
    public interface IApiService
    {
        Task<T> Get<T>(int id, string url);
        Task<List<T>> GetAll<T>(string url);
        Task Add<T>(T payload, string url);
        Task Update<T>(T payload, string url);
        Task Delete<T>(T payload, string url);
    }
}
