using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;

namespace TSM.Services
{
    public interface ITeamService
    {
        Task<Team> Get(int id);
        Task<List<Team>> GetAllForUser();
        Task Add(Team team);
        Task Update(Team team);
        Task Delete(int id);
    }
}
