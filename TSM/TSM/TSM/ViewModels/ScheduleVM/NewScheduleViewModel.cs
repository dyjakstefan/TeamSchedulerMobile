using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.ScheduleVM
{
    public class NewScheduleViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private string name;

        private int teamId;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                AddScheduleCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command AddScheduleCommand { get; protected set; }

        public NewScheduleViewModel(INavigation navigation, int teamId)
        {
            AddScheduleCommand = new Command(async () => await AddSchedule(), () => !IsBusy);
            Navigation = navigation;
            this.teamId = teamId;
        }

        protected async Task AddSchedule()
        {
            IsBusy = true;
            try
            {
                var schedule = new Schedule { Name = this.Name, TeamId = teamId };
                await apiService.Add(schedule, "schedules");
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
