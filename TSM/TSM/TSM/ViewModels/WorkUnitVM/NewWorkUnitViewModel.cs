using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using TSM.Dto;

namespace TSM.ViewModels.WorkUnitVM
{
    public class NewWorkUnitViewModel : BaseViewModel
    {
        private IApiService apiService;

        private Schedule schedule;

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

        public NewWorkUnitViewModel(IApiService apiService)
        {
            this.apiService = apiService;
            AddWorkUnitCommand = new Command(async () => await AddWorkUnit(), () => !IsBusy && SelectedMember != null);
            OnAddWorkUnitEntryCommand = new Command(AddWorkUnitEntry, () => !IsBusy);
            OnDeleteWorkUnitCommand = new Command(DeleteWorkUnit);
        }

        public void Initialize(INavigation navigation, Schedule schedule, List<Member> members, DayOfWeek day)
        {
            Members = members;
            Navigation = navigation;
            this.schedule = schedule;
            this.day = day;
            WorkUnits = new ObservableCollection<WorkUnit>();
            AddWorkUnitEntry();
        }

        protected async Task AddWorkUnit()
        {
            IsBusy = true;

            try
            {
                var workUnitListDto = new WorkUnitListDto
                {
                    MemberId = SelectedMember.Id,
                    ScheduleId = schedule.Id,
                    WorkUnits = new List<WorkUnit>(WorkUnits)
                };
                await apiService.Add(workUnitListDto, "workunit/list");
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "Operacja nie powiodła się.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void AddWorkUnitEntry()
        {
            WorkUnits.Add(new WorkUnit { Start = schedule.StartOfWorkingTime, End = schedule.EndOfWorkingTime, DayOfWeek = day});
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
