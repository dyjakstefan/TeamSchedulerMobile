using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.TeamVM
{
    public class NewTeamViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private string name;

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
                AddTeamCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command AddTeamCommand { get; protected set; }

        public NewTeamViewModel(INavigation navigation)
        {
            AddTeamCommand = new Command(async () => await AddTeam(), () => !IsBusy);
            Navigation = navigation;
        }

        protected async Task AddTeam()
        {
            IsBusy = true;
            try
            {
                var team = new Team {Name = this.Name};
                await apiService.Add(team, "teams");
                MessagingCenter.Send(this, "AddTeam");
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
