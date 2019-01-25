using System;
using TSM.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TSM.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TSM
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();
            Settings.BaseAddress = "http://192.168.1.65:45455/api/";

            if (string.IsNullOrWhiteSpace(Settings.AccessToken) && Settings.AccessTokenExpirationDate < DateTime.Now)
            {
                MainPage = new NavigationPage(new TeamListPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
