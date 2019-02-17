using Autofac;
using TSM.Services;
using TSM.ViewModels.TeamVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.TeamPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTeamPage : ContentPage
	{
	    private NewTeamViewModel viewModel;

		public NewTeamPage ()
		{
			InitializeComponent ();
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new NewTeamViewModel(App.Container.Resolve<IApiService>(), Navigation);
		    }
		    BindingContext = viewModel;
		    NameEntry.Completed += (sender, e) => { viewModel.AddTeamCommand.Execute(null); };
        }
	}
}