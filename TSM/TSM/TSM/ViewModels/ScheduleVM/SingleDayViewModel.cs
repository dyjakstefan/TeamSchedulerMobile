using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.Services;
using TSM.Views.SchedulePages;
using Xamarin.Forms;
using Task = TSM.Models.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class SingleDayViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int scheduleId;

        private DayOfWeek day;

        public ObservableCollection<Task> Tasks { get; set; }

        public Command LoadTasksCommand { get; set; }

        public Command OnAddTaskCommand { get; set; }

        public INavigation Navigation { get; set; }

        public SingleDayViewModel(INavigation navigation, Schedule schedule, DayOfWeek day)
        {
            scheduleId = schedule.Id;
            this.day = day;
            Tasks = new ObservableCollection<Task>();
            LoadTasksCommand = new Command(async () => await LoadTasks());
            OnAddTaskCommand = new Command(async () => await OnAddTask(), () => !IsBusy);
            Navigation = navigation;
        }

        private async System.Threading.Tasks.Task LoadTasks()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();
                var tasks = await apiService.GetAll<Task>($"tasks/{scheduleId}/{day}");
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
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

        private async System.Threading.Tasks.Task OnAddTask()
        {
            await Navigation.PushAsync(new NewTaskPage(scheduleId));
        }
    }
}
