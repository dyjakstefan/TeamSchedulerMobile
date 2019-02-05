using System;
using System.Collections.ObjectModel;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class SummaryViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int teamId;

        public ObservableCollection<Member> Members { get; set; }

        public Command LoadMembersCommand { get; set; }

        public INavigation Navigation { get; set; }

        public SummaryViewModel(INavigation navigation, int teamId)
        {
            this.teamId = teamId;
            Members = new ObservableCollection<Member>();
            LoadMembersCommand = new Command(async () => await LoadMembers());
            Navigation = navigation;
        }

        private async Task LoadMembers()
        {
            IsBusy = true;

            try
            {
                Members.Clear();
                var team = await apiService.Get<Team>(teamId, "teams");
                foreach (var member in team.Members)
                {
                    Members.Add(member);
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
    }
}
