using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.WorkUnitVM
{
    public class EditWorkUnitViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int scheduleId;

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                EditWorkUnitCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<WorkUnit> WorkUnits { get; set; }

        public MemberList MemberList { get; set; }

        public INavigation Navigation { get; set; }

        public Command EditWorkUnitCommand { get; protected set; }

        public Command OnAddWorkUnitEntryCommand { get; protected set; }

        public Command OnDeleteWorkUnitCommand { get; protected set; }

        public EditWorkUnitViewModel(INavigation navigation, int scheduleId, MemberList memberList)
        {
            MemberList = memberList;
            EditWorkUnitCommand = new Command(async () => await EditWorkUnit(), () => !IsBusy);
            OnDeleteWorkUnitCommand = new Command(DeleteWorkUnit);
            Navigation = navigation;
            this.scheduleId = scheduleId;

            WorkUnits = new ObservableCollection<WorkUnit>(memberList.WorkUnits);
        }

        protected async Task EditWorkUnit()
        {
            IsBusy = true;

            try
            {
                var workUnitListDto = new WorkUnitListDto
                {
                    MemberId = MemberList.MemberId,
                    ScheduleId = scheduleId,
                    WorkUnits = new List<WorkUnit>(WorkUnits)
                };
                await apiService.Update(workUnitListDto, "workunit/list");
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
