using System;
using System.Collections.Generic;
using System.Text;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewTaskViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int scheduleId;

        private Member selectedMember;

        private TSM.Models.Task task;

        public Member SelectedMember
        {
            get { return selectedMember; }
            set { SetProperty(ref selectedMember, value); }
        }

        public TSM.Models.Task Task
        {
            get { return task; }
            set { SetProperty(ref task, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                AddTaskCommand.ChangeCanExecute();
            }
        }

        public List<Member> Members { get; set; }

        public INavigation Navigation { get; set; }

        public Command AddTaskCommand { get; protected set; }

        public NewTaskViewModel(INavigation navigation, int scheduleId, List<Member> members)
        {
            Members = members;
            AddTaskCommand = new Command(async () => await AddTask(), () => !IsBusy);
            Navigation = navigation;
            this.scheduleId = scheduleId;
        }

        protected async Task AddTask()
        {
            IsBusy = true;
            try
            {
                var a = SelectedMember;
                //var member = new MemberDto { Email = this.Email, TeamId = teamId };
                //await apiService.Add(member, "members");
                //await Navigation.PopAsync();
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
