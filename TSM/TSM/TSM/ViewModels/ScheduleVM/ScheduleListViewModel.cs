using System;
using System.Collections.ObjectModel;
using System.Linq;
using TSM.Helpers;
using TSM.Models;
using TSM.Services;
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

        public Command OnEditScheduleCommand { get; set; }

        public INavigation Navigation { get; set; }

        public ScheduleListViewModel(INavigation navigation, Team team)
        {
            Team = team;
            Schedules = new ObservableCollection<Schedule>();
            LoadSchedulesCommand = new Command(async () => await LoadSchedules());
            DeleteScheduleCommand = new Command<Schedule>(async (schedule) => await DeleteSchedule(schedule), (x) => !IsBusy);
            OnEditScheduleCommand = new Command<Schedule>(async (schedule) => await EditSchedule(schedule), (x) => !IsBusy);
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
                schedules = schedules.OrderBy(x => !x.IsActive).ThenBy(x => x.StartAt).ToList();
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
            if (!(HasManagerPermissions || schedule.CreatorId == Settings.UserId))
            {
                await Application.Current.MainPage.DisplayAlert("Brak dostępu", "Nie masz wystarczających uprawnień.", "Ok");
                return;
            }

            IsBusy = true;
            var shouldDelete = await Application.Current.MainPage.DisplayAlert(schedule.Name, "Czy napewno chcesz usunąć ten plan?", "Tak", "Nie");
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

        private async Task EditSchedule(Schedule schedule)
        {
            if (HasManagerPermissions || schedule.CreatorId == Settings.UserId)
            {
                await Navigation.PushAsync(new EditSchedulePage(schedule));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Brak dostępu", "Nie masz wystarczających uprawnień.","Ok");
            }
        }

        private async Task OnAddSchedule()
        {
            await Navigation.PushAsync(new NewSchedulePage(Team.Id));
        }
    }
}