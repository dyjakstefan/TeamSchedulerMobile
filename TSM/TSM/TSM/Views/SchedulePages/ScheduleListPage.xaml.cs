using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.ViewModels.MemberVM;
using TSM.ViewModels.ScheduleVM;
using TSM.Views.MemberPages;
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
            viewModel = new ScheduleListViewModel(Navigation, team);
            BindingContext = viewModel;
        }

        private async void OnScheduleSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var schedule = args.SelectedItem as Schedule;
            if (schedule == null)
            {
                return;
            }

            await Navigation.PushAsync(new SchedulePage(schedule));
            ScheduleListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadSchedulesCommand.Execute(null);
        }
    }
}