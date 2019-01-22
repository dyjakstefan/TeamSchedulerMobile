using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using TSM.Views.MemberPages;
using Xamarin.Forms;

namespace TSM.ViewModels.MemberVM
{
    public class MemberListViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int teamId;

        public ObservableCollection<Member> Members { get; set; }

        public Command LoadMembersCommand { get; set; }

        public Command OnAddMemberCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MemberListViewModel(INavigation navigation, Team team)
        {
            teamId = team.Id;
            Members = new ObservableCollection<Member>(team.Members);
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

        private async Task OnAddMember()
        {
            await Navigation.PushAsync(new NewMemberPage(teamId));
        }
    }
}
