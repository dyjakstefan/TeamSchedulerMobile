using Autofac;
using TSM.Services;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewSchedulePage : ContentPage
	{
	    private NewScheduleViewModel viewModel;

	    public NewSchedulePage(int teamId)
	    {
	        InitializeComponent();
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = new NewScheduleViewModel(App.Container.Resolve<IApiService>(), Navigation, teamId);
	        }
	        BindingContext = viewModel;
	        NameEntry.Completed += (sender, e) => { viewModel.AddScheduleCommand.Execute(null); };
	    }
    }
}