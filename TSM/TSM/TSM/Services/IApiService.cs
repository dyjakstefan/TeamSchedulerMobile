using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace TSM.Services
{
    public interface IApiService
    {
        Task<T> Get<T>(string url);
        Task<T> Get<T>(int id, string url);
        Task<List<T>> GetAll<T>(string url);
        Task<List<T>> GetAll<T>(int id, string url);
        Task Add<T>(T payload, string url);
        Task Update<T>(T payload, string url);
        Task Delete<T>(T payload, string url);
    }
}
