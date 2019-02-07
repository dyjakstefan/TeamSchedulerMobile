using System;
using TSM.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TSM.Views;
using TSM.Views.TeamPages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TSM
{
    public partial class App : Application
    {
        public static App Instance;

        public App()
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            Instance = this;
            InitializeComponent();
            Settings.BaseAddress = "http://192.168.0.103:45455/api/";
            CleanNavigation();
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

        public void CleanNavigation()
        {
            if (Settings.IsAuthenticated)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
