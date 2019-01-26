using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewTaskViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private string email;

        private int scheduleId;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
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

        public INavigation Navigation { get; set; }

        public Command AddTaskCommand { get; protected set; }

        public NewTaskViewModel(INavigation navigation, int scheduleId)
        {
            AddTaskCommand = new Command(async () => await AddTask(), () => !IsBusy);
            Navigation = navigation;
            this.scheduleId = scheduleId;
        }

        protected async Task AddTask()
        {
            IsBusy = true;
            try
            {
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
