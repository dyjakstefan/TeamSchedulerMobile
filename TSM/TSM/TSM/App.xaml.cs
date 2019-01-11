using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TSM.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TSM
{
    public partial class App : Application
    {
        public static bool IsUserLoggerdIn { get; set; }

        public App()
        {
            InitializeComponent();

            if (IsUserLoggerdIn)
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new LoginPage();
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
