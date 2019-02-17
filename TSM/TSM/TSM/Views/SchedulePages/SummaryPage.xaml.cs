using Autofac;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SummaryPage : ContentPage
	{
	    private SummaryViewModel viewModel;

        public SummaryPage (Schedule schedule)
		{
			InitializeComponent ();
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new SummaryViewModel(App.Container.Resolve<IApiService>(), Navigation, schedule.TeamId, schedule.Id);
		    }
		    BindingContext = viewModel;
        }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
            viewModel.LoadWorkHoursCommand.Execute(null);
	        viewModel.LoadMembersCommand.Execute(null);
	    }
    }
}