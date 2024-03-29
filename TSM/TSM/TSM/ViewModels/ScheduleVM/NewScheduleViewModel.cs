﻿using System;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewScheduleViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

        private string name;

        private string description;

        private DateTime startAt;

        private DateTime endAt;

        private TimeSpan startOfWorkingTime;

        private TimeSpan endOfWorkingTime;

        private int teamId;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public DateTime StartAt
        {
            get { return startAt; }
            set { SetProperty(ref startAt, value); }
        }

        public DateTime EndAt
        {
            get { return endAt; }
            set { SetProperty(ref endAt, value); }
        }

        public TimeSpan StartOfWorkingTime
        {
            get { return startOfWorkingTime; }
            set { SetProperty(ref startOfWorkingTime, value); }
        }

        public TimeSpan EndOfWorkingTime
        {
            get { return endOfWorkingTime; }
            set { SetProperty(ref endOfWorkingTime, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                AddScheduleCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command AddScheduleCommand { get; protected set; }

        public NewScheduleViewModel(IApiService apiService, INavigation navigation, int teamId)
        {
            Navigation = navigation;
            this.teamId = teamId;
            AddScheduleCommand = new Command(async () => await AddSchedule(), () => !IsBusy);
            this.apiService = apiService;
            StartAt = DateTime.Now;
            EndAt = DateTime.Now.AddDays(7);
            StartOfWorkingTime = new TimeSpan(8, 0, 0);
            EndOfWorkingTime = new TimeSpan(16, 0, 0);
        }

        protected async Task AddSchedule()
        {
            IsBusy = true;
            try
            {
                var schedule = new Schedule
                {
                    Name = this.Name,
                    TeamId = teamId,
                    Description = this.Description,
                    StartAt = this.StartAt,
                    EndAt = this.EndAt,
                    StartOfWorkingTime = this.StartOfWorkingTime,
                    EndOfWorkingTime = this.EndOfWorkingTime
                };
                await apiService.Add(schedule, "schedules");
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
