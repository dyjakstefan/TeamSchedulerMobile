using System;
using System.Collections.ObjectModel;
using TSM.Models;
using TSM.Services;
using TSM.Views.TeamPages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.TeamVM
{
    public class TeamListViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

        public ObservableCollection<Team> Teams { get; set; }

        public Command LoadTeamsCommand { get; set; }

        public Command EditTeamCommand { get; set; }

        public Command DeleteTeamCommand { get; set; }

        public INavigation Navigation { get; set; }

        public TeamListViewModel(IApiService apiService, INavigation navigation)
        {
            this.apiService = apiService;
            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await LoadTeams());
            EditTeamCommand = new Command<Team>(async (team) => await EditTeam(team), (team) => HasManagerPermissions);
            DeleteTeamCommand = new Command<Team>(async (team) => await DeleteTeam(team), (team) => HasManagerPermissions);
            Navigation = navigation;
        }

        private async Task LoadTeams()
        {
            IsBusy = true;

            try
            {
                Teams.Clear();
                apiService.UpdateAuthorizationHeader();
                var teams = await apiService.GetAll<Team>("teams/all");
                foreach (var team in teams)
                {
                    Teams.Add(team);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EditTeam(Team team)
        {
            if (isBusy)
                return;
            await Navigation.PushAsync(new EditTeamPage(team));
        }

        private async Task DeleteTeam(Team team)
        {
            if (isBusy)
                return;

            IsBusy = true;
            var shouldDelete = await Application.Current.MainPage.DisplayAlert("Delete", "Do you want to delete that team", "Ok", "Cancel");
            if (!shouldDelete)
                return;

            try
            {
                await apiService.Delete(new { Id = team.Id }, "teams");
                Teams.Remove(team);
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
