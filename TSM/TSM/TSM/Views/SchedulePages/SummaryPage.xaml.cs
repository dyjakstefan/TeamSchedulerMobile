using TSM.Models;
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
		    viewModel = new SummaryViewModel(Navigation, schedule.TeamId);
		    BindingContext = viewModel;
        }

	    private void OnMemberSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        MemberListView.SelectedItem = null;
	    }

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        viewModel.LoadMembersCommand.Execute(null);
	    }
    }
}