using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TSM.Annotations;
using TSM.Extensions;
using TSM.Helpers;
using TSM.Models;
using TSM.Services;
using TSM.Views;
using Xamarin.Forms;

namespace TSM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IAuthService authService => DependencyService.Get<IAuthService>() ?? new AuthService();

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

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                SubmitCommand.ChangeCanExecute();
            }
        }

        public INavigation Navigation;

        public Command SubmitCommand { get; protected set; }

        public LoginViewModel(INavigation navigation)
        {
            SubmitCommand = new Command(async () => await OnSubmit(), () => !IsBusy);
            Navigation = navigation;
        }

        public async Task OnSubmit()
        {
            IsBusy = true;
            try
            {
                var jwt = await authService.Login(email, password);
                Settings.AccessToken = jwt.Token;
                Settings.AccessTokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(jwt.Expires).LocalDateTime;
                Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.Last());
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Login, try again", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
    }
}
