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
	        viewModel = new NewScheduleViewModel(Navigation, teamId);
	        BindingContext = viewModel;
	        NameEntry.Completed += (sender, e) => { viewModel.AddScheduleCommand.Execute(null); };
	    }
    }
}