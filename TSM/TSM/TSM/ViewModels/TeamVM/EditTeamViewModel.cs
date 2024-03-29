﻿using System;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.TeamVM
{
    public class EditTeamViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

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

        public EditTeamViewModel(IApiService apiService, INavigation navigation, Team team)
        {
            this.apiService = apiService;
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
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
