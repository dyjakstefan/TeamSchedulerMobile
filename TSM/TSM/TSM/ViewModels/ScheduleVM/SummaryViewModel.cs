using System;
using System.Collections.ObjectModel;
using System.Linq;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class SummaryViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int teamId;

        private int scheduleId;

        public ObservableCollection<Member> Members { get; set; }

        public ObservableCollection<WorkingHour> WorkHours { get; set; }

        public Command LoadMembersCommand { get; set; }

        public Command LoadWorkHoursCommand { get; set; }

        public INavigation Navigation { get; set; }

        public SummaryViewModel(INavigation navigation, int teamId, int scheduleId)
        {
            this.teamId = teamId;
            this.scheduleId = scheduleId;
            Members = new ObservableCollection<Member>();
            WorkHours = new ObservableCollection<WorkingHour>();
            LoadMembersCommand = new Command(async () => await LoadMembers());
            LoadWorkHoursCommand = new Command(async () => await LoadWorkHours());
            Navigation = navigation;
        }

        private async Task LoadMembers()
        {
            IsBusy = true;

            try
            {
                Members.Clear();
                var members = await apiService.GetAll<Member>($"members/{teamId}/{scheduleId}");
                foreach (var member in members)
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

        private async Task LoadWorkHours()
        {
            IsBusy = true;

            try
            {
                WorkHours.Clear();
                var workHours = await apiService.GetAll<WorkingHour>($"schedules/report2/{scheduleId}");
                foreach (var workHour in workHours)
                {
                    WorkHours.Add(workHour);
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
    }
}
