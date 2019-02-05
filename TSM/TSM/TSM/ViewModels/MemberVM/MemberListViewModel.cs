using System;
using System.Collections.ObjectModel;
using TSM.Models;
using TSM.Services;
using TSM.Views.MemberPages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.MemberVM
{
    public class MemberListViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        public Team Team { get; set; }

        public ObservableCollection<Member> Members { get; set; }

        public Command LoadMembersCommand { get; set; }

        public Command OnAddMemberCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MemberListViewModel(INavigation navigation, Team team)
        {
            Team = team;
            Members = new ObservableCollection<Member>(Team.Members);
            LoadMembersCommand = new Command(async () => await LoadMembers());
            OnAddMemberCommand = new Command(async () => await OnAddMember(), () => !IsBusy);
            Navigation = navigation;
        }

        private async Task LoadMembers()
        {
            IsBusy = true;

            try
            {
                Members.Clear();
                var team = await apiService.Get<Team>(Team.Id, "teams");
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

        private async Task OnAddMember()
        {
            await Navigation.PushAsync(new NewMemberPage(Team.Id));
        }
    }
}
