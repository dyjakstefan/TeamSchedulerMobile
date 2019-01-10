using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TSM.Annotations;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IAuthService authService => DependencyService.Get<IAuthService>() ?? new AuthService();

        private string email;  

        private string password;

        public Action DisplayInvalidLoginPrompt;

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
            SubmitCommand = new Command(OnSubmit);
        }

        public async void OnSubmit()
        {
            var token = await authService.Login(email, password);
            if (email != "a" || password != "a")
            {
                DisplayInvalidLoginPrompt();
            }
        }
        
    }
}
