using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels
{
    public class TeamListViewModel : BaseViewModel
    {
        private ITeamService teamService => DependencyService.Get<ITeamService>() ?? new TeamService();

        public ObservableCollection<Team> Teams { get; set; }

        public Command LoadTeamsCommand { get; set; }

        public INavigation Navigation { get; set; }

        public TeamListViewModel(INavigation navigation)
        {
            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await LoadTeams());
            Navigation = navigation;

            MessagingCenter.Subscribe<NewTeamViewModel>(this, "AddTeam", async (obj) => await LoadTeams());
        }

        private async Task LoadTeams()
        {
            if (isBusy)
                return;

            IsBusy = true;

            try
            {
                Teams.Clear();
                var teams = await teamService.GetAllForUser();
                foreach (var team in teams)
                {
                    Teams.Add(team);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
