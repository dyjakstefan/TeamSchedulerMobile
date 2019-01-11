using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TSM.Annotations;
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

        public ICommand SubmitCommand { protected set; get; }

        public LoginViewModel()
        {
            SubmitCommand = new Command(async () => await OnSubmit());
        }

        public async Task OnSubmit()
        {
            try
            {
                var token = await authService.Login(email, password);
                token.CreatedAt = DateTime.Now;
                await LocalDatabase.InsertSingle(token);
                App.IsUserLoggerdIn = true;
                Application.Current.MainPage = new MainPage();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid Login, try again", "OK");
            }
        }
        
    }
}
