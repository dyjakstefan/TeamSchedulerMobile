using System;
using Autofac;
using TSM.Helpers;
using TSM.Services;
using TSM.ViewModels.AuthVM;
using TSM.ViewModels.WorkUnitVM;
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
        public static IContainer Container { get; set; }

        public App()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new ApiService()).As<IApiService>();
            builder.RegisterInstance(new AuthService()).As<IAuthService>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<NewWorkUnitViewModel>();

            Container = builder.Build();
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            Instance = this;
            Settings.BaseAddress = "http://192.168.75.1:45455/api/";
            InitializeComponent();
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
