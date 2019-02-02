﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using TSM.Dto;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewWorkUnitViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int scheduleId;

        private DayOfWeek day;

        private Member selectedMember;

        public Member SelectedMember
        {
            get { return selectedMember; }
            set
            {
                SetProperty(ref selectedMember, value);
                AddWorkUnitCommand.ChangeCanExecute();
            }
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

        public ObservableCollection<WorkUnit> WorkUnits { get; set; }

        public List<Member> Members { get; set; }

        public INavigation Navigation { get; set; }

        public Command AddWorkUnitCommand { get; protected set; }

        public Command OnAddWorkUnitEntryCommand { get; protected set; }

        public Command OnDeleteWorkUnitCommand { get; protected set; }

        public NewWorkUnitViewModel(INavigation navigation, int scheduleId, List<Member> members, DayOfWeek day)
        {
            Members = members;
            AddWorkUnitCommand = new Command(async () => await AddWorkUnit(), () => !IsBusy && SelectedMember != null);
            OnAddWorkUnitEntryCommand = new Command(AddWorkUnitEntry, () => !IsBusy);
            OnDeleteWorkUnitCommand = new Command(DeleteWorkUnit);
            Navigation = navigation;
            this.scheduleId = scheduleId;
            this.day = day;

            WorkUnits = new ObservableCollection<WorkUnit>();
            AddWorkUnitEntry();
        }

        protected async Task AddWorkUnit()
        {
            IsBusy = true;

            try
            {
                foreach (var workUnit in WorkUnits)
                {
                    workUnit.DayOfWeek = day;
                    workUnit.MemberId = SelectedMember.Id;
                    workUnit.ScheduleId = scheduleId;
                    await apiService.Add(workUnit, "workunit");
                }
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

        protected void AddWorkUnitEntry()
        {
            WorkUnits.Add(new WorkUnit { Start = new TimeSpan(0, 8, 0, 0), End = new TimeSpan(0, 16, 0, 0) });
        }

        protected void DeleteWorkUnit(object obj)
        {
            var workUnit = obj as WorkUnit;
            if (workUnit != null && WorkUnits.Contains(workUnit))
            {
                WorkUnits.Remove(workUnit);
            }
        }
    }
}
