using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.MemberVM
{
    public class NewMemberViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private string email;

        private int teamId;

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

        public INavigation Navigation { get; set; }

        public Command AddMemberCommand { get; protected set; }

        public NewMemberViewModel(INavigation navigation, int teamId)
        {
            AddMemberCommand = new Command(async () => await AddMember(), () => !IsBusy);
            Navigation = navigation;
            this.teamId = teamId;
        }

        protected async Task AddMember()
        {
            IsBusy = true;
            try
            {
                var member = new MemberDto { Email = this.Email, TeamId = teamId };
                await apiService.Add(member, "members");
                MessagingCenter.Send(this, "AddMember");
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
