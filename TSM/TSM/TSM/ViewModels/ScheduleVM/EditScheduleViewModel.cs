using System;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.ScheduleVM
{
    public class EditScheduleViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

        private string name;

        private string description;

        private DateTime startAt;

        private DateTime endAt;

        private TimeSpan startOfWorkingTime;

        private TimeSpan endOfWorkingTime;

        private int scheduleId;

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
                EditScheduleCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command EditScheduleCommand { get; protected set; }

        public EditScheduleViewModel(IApiService apiService, INavigation navigation, Schedule schedule)
        {
            Navigation = navigation;
            StartAt = schedule.StartAt;
            EndAt = schedule.EndAt;
            Name = schedule.Name;
            Description = schedule.Description;
            scheduleId = schedule.Id;
            EndOfWorkingTime = schedule.EndOfWorkingTime;
            StartOfWorkingTime = schedule.StartOfWorkingTime;
            this.apiService = apiService;
            EditScheduleCommand = new Command(async () => await EditSchedule(), () => !IsBusy);
        }

        protected async Task EditSchedule()
        {
            IsBusy = true;
            try
            {
                var schedule = new Schedule
                {
                    Name = this.Name,
                    Id = scheduleId,
                    Description = this.Description,
                    StartAt = this.StartAt,
                    EndAt = this.EndAt,
                    StartOfWorkingTime = this.StartOfWorkingTime,
                    EndOfWorkingTime = this.EndOfWorkingTime
                };
                await apiService.Update(schedule, "schedules");
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
