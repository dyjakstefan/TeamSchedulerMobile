using System;
using System.Linq;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Helpers;
using TSM.Services;
using TSM.Views;
using TSM.Views.TeamPages;
using Xamarin.Forms;

namespace TSM.ViewModels.AuthVM
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService authService;

        private string firstName;

        private string lastName;

        private string email;

        private string password;

        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

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
                RegisterCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation;

        public Command RegisterCommand { get; protected set; }

        public RegisterViewModel(IAuthService authService, INavigation navigation)
        {
            Navigation = navigation;
            this.authService = authService;
            RegisterCommand = new Command(async () => await OnRegisterClicked(), () => !IsBusy);
        }

        public async Task OnRegisterClicked()
        {
            IsBusy = true;
            try
            {
                var jwt = await authService.CreateUser(new UserDto
                {
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password
                });
                Settings.AccessToken = jwt.Token;
                Settings.AccessTokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(jwt.Expires).LocalDateTime;
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
