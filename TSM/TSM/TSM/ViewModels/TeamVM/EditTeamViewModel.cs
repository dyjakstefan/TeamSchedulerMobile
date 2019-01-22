using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.TeamVM
{
    public class EditTeamViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private string name;

        private Team team;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                EditTeamCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command EditTeamCommand { get; protected set; }

        public EditTeamViewModel(INavigation navigation, Team team)
        {
            EditTeamCommand = new Command(async () => await EditTeam(), () => !IsBusy);
            Navigation = navigation;
            this.team = team;
            Name = team.Name;
        }

        protected async Task EditTeam()
        {
            IsBusy = true;
            try
            {
                team.Name = Name;
                await apiService.Update(team, "teams");
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Team with that name already exists.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
