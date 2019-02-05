using TSM.ViewModels.TeamVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTeamPage : ContentPage
	{
	    private NewTeamViewModel viewModel;

		public NewTeamPage ()
		{
			InitializeComponent ();
            viewModel = new NewTeamViewModel(Navigation);
		    BindingContext = viewModel;
		    NameEntry.Completed += (sender, e) => { viewModel.AddTeamCommand.Execute(null); };
        }
	}
}