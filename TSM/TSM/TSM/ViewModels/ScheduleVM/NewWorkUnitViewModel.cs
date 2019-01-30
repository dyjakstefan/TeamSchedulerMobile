using System;
using System.Collections.Generic;
using System.Text;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewWorkUnitViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int scheduleId;

        private DayOfWeek day;

        private Member selectedMember;

        private WorkUnit workUnit;

        private TimeSpan selectedStartTime;

        private TimeSpan selectedEndTime;

        public Member SelectedMember
        {
            get { return selectedMember; }
            set { SetProperty(ref selectedMember, value); }
        }

        public WorkUnit WorkUnit
        {
            get { return workUnit; }
            set { SetProperty(ref workUnit, value); }
        }

        public TimeSpan SelectedStartTime
        {
            get { return selectedStartTime; }
            set { SetProperty(ref selectedStartTime, value); }
        }

        public TimeSpan SelectedEndTime
        {
            get { return selectedEndTime; }
            set { SetProperty(ref selectedEndTime, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                AddWorkUnitCommand.ChangeCanExecute();
            }
        }

        public List<Member> Members { get; set; }

        public INavigation Navigation { get; set; }

        public Command AddWorkUnitCommand { get; protected set; }

        public NewWorkUnitViewModel(INavigation navigation, int scheduleId, List<Member> members, DayOfWeek day)
        {
            Members = members;
            AddWorkUnitCommand = new Command(async () => await AddWorkUnit(), () => !IsBusy);
            Navigation = navigation;
            this.scheduleId = scheduleId;
            this.day = day;
        }

        protected async Task AddWorkUnit()
        {
            IsBusy = true;
            try
            {
                var workUnit = new WorkUnit
                {
                    DayOfWeek = day,
                    Start = SelectedStartTime,
                    End = SelectedEndTime,
                    MemberId = SelectedMember.Id,
                    ScheduleId = scheduleId
                };
                await apiService.Add(workUnit, "workunit");
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
