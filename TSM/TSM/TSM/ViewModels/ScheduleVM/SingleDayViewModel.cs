﻿using System;
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

        public ObservableCollection<MemberList> MembersWorkUnitsList { get; set; }

        public Command LoadWorkUnitsCommand { get; set; }

        public Command OnAddWorkUnitCommand { get; set; }

        public INavigation Navigation { get; set; }

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
                    var memberList = new MemberList(workUnits.Where(x => x.MemberId == member.Id).ToList());
                    memberList.FullName = member.User.FullName;
                    memberList.MemberId = member.Id;
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
            await Navigation.PushAsync(new NewWorkUnitPage(Schedule.TeamId, Members, day));
        }
    }

    public class MemberList : List<WorkUnit>
    {
        public string FullName { get; set; }

        public int MemberId { get; set; }

        public List<WorkUnit> WorkUnits => this;

        public MemberList()
        {
        }

        public MemberList(IEnumerable<WorkUnit> workUnits)
        {
            foreach (var unit in workUnits)
                WorkUnits.Add(unit);
        }
    }
}
