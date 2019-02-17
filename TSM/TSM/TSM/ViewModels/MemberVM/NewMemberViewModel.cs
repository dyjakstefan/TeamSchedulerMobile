using System;
using System.Collections.Generic;
using TSM.Dto;
using TSM.Enums;
using TSM.Services;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.MemberVM
{
    public class NewMemberViewModel : BaseViewModel
    {
        private readonly IApiService apiService;

        private string email;

        private int teamId;

        private int hours;

        private JobTitle jobTitle;

        public int Hours
        {
            get { return hours; }
            set { SetProperty(ref hours, value); }
        }

        public JobTitle JobTitle
        {
            get { return jobTitle; }
            set { SetProperty(ref jobTitle, value); }
        }

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
                AddMemberCommand.ChangeCanExecute();
            }
        }

        public List<string> Titles { get; set; }

        public INavigation Navigation { get; set; }

        public Command AddMemberCommand { get; protected set; }

        public NewMemberViewModel(IApiService apiService, INavigation navigation, int teamId)
        {
            this.apiService = apiService;
            Navigation = navigation;
            this.teamId = teamId;
            AddMemberCommand = new Command(async () => await AddMember(), () => !IsBusy);
            Titles = new List<string>();
            Titles.Add(JobTitle.Manager.ToString());
            Titles.Add(JobTitle.Employee.ToString());
        }

        protected async Task AddMember()
        {
            IsBusy = true;
            try
            {
                var member = new MemberDto { Email = email, TeamId = teamId, Hours = hours, Title = jobTitle};
                await apiService.Add(member, "members");
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
