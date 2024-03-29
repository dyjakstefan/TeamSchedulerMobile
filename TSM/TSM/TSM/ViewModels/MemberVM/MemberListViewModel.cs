﻿using System;
using System.Collections.ObjectModel;
using TSM.Helpers;
using TSM.Models;
using TSM.Services;
using TSM.Views.MemberPages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.MemberVM
{
    public class MemberListViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

        public Team Team { get; set; }

        public ObservableCollection<Member> Members { get; set; }

        public Command LoadMembersCommand { get; set; }

        public Command OnAddMemberCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MemberListViewModel(IApiService apiService, INavigation navigation, Team team)
        {
            this.apiService = apiService;
            Navigation = navigation;
            Team = team;
            Members = new ObservableCollection<Member>(Team.Members);
            LoadMembersCommand = new Command(async () => await LoadMembers());
            OnAddMemberCommand = new Command(async () => await OnAddMember(), () => !IsBusy);
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

                Team.Members = team.Members;
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
            if (Settings.HasManagerPermissions)
            {
                await Navigation.PushAsync(new NewMemberPage(Team.Id));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Brak dostępu", "Nie masz odpowiednich uprawnień.", "OK");
            }
        }
    }
}
