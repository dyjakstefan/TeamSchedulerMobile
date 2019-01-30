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

namespace TSM.ViewModels.ScheduleVM
{
    public class SingleDayViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private DayOfWeek day;

        public Schedule Schedule { get; set; }

        public List<Member> Members { get; set; }

        public ObservableCollection<WorkUnit> WorkUnits { get; set; }

        public Command LoadWorkUnitsCommand { get; set; }

        public Command OnAddWorkUnitCommand { get; set; }

        public INavigation Navigation { get; set; }

        public SingleDayViewModel(INavigation navigation, Schedule schedule, List<Member> members, DayOfWeek day)
        {
            Schedule = schedule;
            Members = members;
            this.day = day;
            WorkUnits = new ObservableCollection<WorkUnit>();
            LoadWorkUnitsCommand = new Command(async () => await LoadWorkUnits());
            OnAddWorkUnitCommand = new Command(async () => await OnAddWorkUnit(), () => !IsBusy);
            Navigation = navigation;
        }

        private async Task LoadWorkUnits()
        {
            IsBusy = true;

            try
            {
                WorkUnits.Clear();
                var workUnits = await apiService.GetAll<WorkUnit>($"workunit/{Schedule.Id}/{day}");
                foreach (var workUnit in workUnits)
                {
                    workUnit.Member = Members.SingleOrDefault(x => x.Id == workUnit.MemberId);
                    WorkUnits.Add(workUnit);
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

        private async Task OnAddWorkUnit()
        {
            await Navigation.PushAsync(new NewWorkUnitPage(Schedule.TeamId, Members, day));
        }
    }
}
