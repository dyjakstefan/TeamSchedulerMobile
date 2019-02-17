using Autofac;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScheduleListPage : ContentPage
	{
        private ScheduleListViewModel viewModel;

        public ScheduleListPage(Team team)
        {
            InitializeComponent();
            using (var scope = App.Container.BeginLifetimeScope())
            {
                viewModel = new ScheduleListViewModel(App.Container.Resolve<IApiService>(), Navigation, team);
            }
            BindingContext = viewModel;
        }

        private async void OnScheduleSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var schedule = args.SelectedItem as Schedule;
            if (schedule == null)
            {
                return;
            }

            await Navigation.PushAsync(new SchedulePage(schedule, viewModel.Team.Members));
            ScheduleListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadSchedulesCommand.Execute(null);
        }
    }
}