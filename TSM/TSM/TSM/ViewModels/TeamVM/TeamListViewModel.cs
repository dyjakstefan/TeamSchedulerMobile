using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using TSM.Views;
using Xamarin.Forms;

namespace TSM.ViewModels.TeamVM
{
    public class TeamListViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        public ObservableCollection<Team> Teams { get; set; }

        public Command LoadTeamsCommand { get; set; }

        public Command EditTeamCommand { get; set; }

        public Command DeleteTeamCommand { get; set; }

        public INavigation Navigation { get; set; }

        public TeamListViewModel(INavigation navigation)
        {
            Teams = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await LoadTeams());
            EditTeamCommand = new Command(async () => await EditTeam(), () => !IsBusy);
            DeleteTeamCommand = new Command<Team>(async (team) => await DeleteTeam(team));
            Navigation = navigation;

            MessagingCenter.Subscribe<NewTeamViewModel>(this, "AddTeam", async (obj) => await LoadTeams());
            MessagingCenter.Subscribe<EditTeamViewModel>(this, "EditTeam", async (obj) => await LoadTeams());
        }

        private async Task LoadTeams()
        {
            if (isBusy)
                return;

            IsBusy = true;

            try
            {
                Teams.Clear();
                var teams = await apiService.GetAll<Team>("teams/all");
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

        private async Task EditTeam()
        {
            if (isBusy)
                return;

            await Navigation.PushAsync(new LoginPage());
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
                await apiService.Delete(team.Id, "teams");
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
