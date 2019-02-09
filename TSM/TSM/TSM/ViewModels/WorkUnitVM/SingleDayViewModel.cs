using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Helpers;
using TSM.Models;
using TSM.Services;
using TSM.Views.WorkUnitPages;
using Xamarin.Forms;

namespace TSM.ViewModels.WorkUnitVM
{
    public class SingleDayViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private DayOfWeek day;

        public Schedule Schedule { get; set; }

        public List<Member> Members { get; set; }

        public ObservableCollection<MemberList> MembersWorkUnitsList { get; set; }

        public Command LoadWorkUnitsCommand { get; set; }

        public Command OnAddWorkUnitCommand { get; set; }

        public Command OnWorkUnitSelectedCommand { get; set; }

        public INavigation Navigation { get; set; }

        public bool HasCreatorPermissions => Schedule.CreatorId == Settings.UserId;

        public SingleDayViewModel(INavigation navigation, Schedule schedule, List<Member> members, DayOfWeek day)
        {
            Schedule = schedule;
            Members = members;
            this.day = day;
            MembersWorkUnitsList = new ObservableCollection<MemberList>();
            LoadWorkUnitsCommand = new Command(async () => await LoadWorkUnits());
            OnAddWorkUnitCommand = new Command(async () => await OnAddWorkUnit(), () => !IsBusy);
            Navigation = navigation;
        }

        private async Task LoadWorkUnits()
        {
            IsBusy = true;

            try
            {
                MembersWorkUnitsList.Clear();
                var workUnits = await apiService.GetAll<WorkUnit>($"workunit/{Schedule.Id}/{day}");
                foreach (var member in Members)
                {
                    var memberList = new MemberList(workUnits.Where(x => x.MemberId == member.Id).ToList())
                        {
                            FullName = member.User.FullName,
                            MemberId = member.Id
                        };
                    MembersWorkUnitsList.Add(memberList);
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
            if (HasManagerPermissions || HasCreatorPermissions)
            {
                await Navigation.PushAsync(new NewWorkUnitPage(Schedule, Members, day));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Brak dostępu", "Nie masz odpowiednich uprawnień.", "OK");
            }
        }
    }
}
