using System;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.TeamVM
{
    public class NewTeamViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

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

        public NewTeamViewModel(IApiService apiService, INavigation navigation)
        {
            this.apiService = apiService;
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
                await Navigation.PopAsync();
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
    }
}
