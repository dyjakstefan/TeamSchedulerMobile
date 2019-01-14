using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TSM.Dto;
using TSM.Services;
using TSM.Views;
using Xamarin.Forms;

namespace TSM.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private IAuthService authService => DependencyService.Get<IAuthService>() ?? new AuthService();

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

        public INavigation Navigation;

        public ICommand RegisterCommand { get; protected set; }

        public RegisterViewModel(INavigation navigation)
        {
            RegisterCommand = new Command(async () => await OnRegisterClicked());
            Navigation = navigation;
        }

        public async Task OnRegisterClicked()
        {
            try
            {
                var token = await authService.CreateUser(new UserDto {Email = email, FirstName = firstName, LastName = lastName, Password = password});
                token.CreatedAt = DateTime.Now;
                await LocalDatabase.InsertSingle(token);
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.Last());
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid credentials.", "OK");
            }
        }
    }
}
