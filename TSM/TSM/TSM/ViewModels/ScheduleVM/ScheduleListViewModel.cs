using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using TSM.Views;
using TSM.Views.SchedulePages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class ScheduleListViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        public Team Team { get; set; }

        public ObservableCollection<Schedule> Schedules { get; set; }

        public Command LoadSchedulesCommand { get; set; }

        public Command OnAddScheduleCommand { get; set; }

        public Command DeleteScheduleCommand { get; set; }

        public INavigation Navigation { get; set; }

        public ScheduleListViewModel(INavigation navigation, Team team)
        {
            Team = team;
            Schedules = new ObservableCollection<Schedule>();
            LoadSchedulesCommand = new Command(async () => await LoadSchedules());
            DeleteScheduleCommand = new Command<Schedule>(async (schedule) => await DeleteSchedule(schedule));
            OnAddScheduleCommand = new Command(async () => await OnAddSchedule(), () => !IsBusy);
            Navigation = navigation;
        }

        private async Task LoadSchedules()
        {
            IsBusy = true;

            try
            {
                Schedules.Clear();
                var schedules = await apiService.GetAll<Schedule>(Team.Id, "schedules/all");
                foreach (var schedule in schedules)
                {
                    Schedules.Add(schedule);
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

        private async Task DeleteSchedule(Schedule schedule)
        {
            if (isBusy)
                return;

            IsBusy = true;
            var shouldDelete = await Application.Current.MainPage.DisplayAlert("Delete", "Do you want to delete that schedule", "Ok", "Cancel");
            if (!shouldDelete)
                return;

            try
            {
                await apiService.Delete(new { ScheduleId = schedule.Id }, "schedules");
                Schedules.Remove(schedule);
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

        private async Task OnAddSchedule()
        {
            await Navigation.PushAsync(new NewSchedulePage(Team.Id));
        }
    }
}
