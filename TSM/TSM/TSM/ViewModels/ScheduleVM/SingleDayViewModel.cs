using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private DayOfWeek day;

        public Schedule Schedule { get; set; }

        public List<Member> Members { get; set; }

        public ObservableCollection<Task> Tasks { get; set; }

        public Command LoadTasksCommand { get; set; }

        public Command OnAddTaskCommand { get; set; }

        public INavigation Navigation { get; set; }

        public SingleDayViewModel(INavigation navigation, Schedule schedule, List<Member> members, DayOfWeek day)
        {
            Schedule = schedule;
            Members = members;
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
                var tasks = await apiService.GetAll<Task>($"tasks/{Schedule.Id}/{day}");
                foreach (var task in tasks)
                {
                    task.Member = Members.SingleOrDefault(x => x.Id == task.MemberId);
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
            await Navigation.PushAsync(new NewTaskPage(Schedule.TeamId, Members));
        }
    }
}
