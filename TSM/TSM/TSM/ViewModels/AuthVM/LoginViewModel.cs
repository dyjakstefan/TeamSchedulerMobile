using System;
using System.Linq;
using TSM.Helpers;
using TSM.Services;
using TSM.Views;
using TSM.Views.TeamPages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TSM.ViewModels.AuthVM
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService authService;

        private string email;  

        private string password;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                SubmitCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation { get; set; }

        public Command SubmitCommand { get; protected set; }

        public LoginViewModel(IAuthService authService, INavigation navigation)
        {
            this.authService = authService;
            Navigation = navigation;
            SubmitCommand = new Command(async () => await OnSubmit(), () => !IsBusy);
        }

        public async Task OnSubmit()
        {
            IsBusy = true;
            try
            {
                var jwt = await authService.Login(email, password);
                if (jwt == null)
                {
                    throw new Exception("Empty token.");
                }
                Settings.AccessToken = jwt.Token;
                Settings.AccessTokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(jwt.Expires).LocalDateTime;
                Settings.UserId = jwt.UserId;
                Application.Current.MainPage = new MainPage();
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
