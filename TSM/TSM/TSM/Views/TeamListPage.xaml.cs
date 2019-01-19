using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamListPage : ContentPage
	{
	    private TeamListViewModel viewModel;

		public TeamListPage ()
		{
			InitializeComponent ();
            viewModel = new TeamListViewModel(Navigation);
		    BindingContext = viewModel;
		}

	    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var team = args.SelectedItem as Team;
	        if (team == null)
	        {
	            return;
            }

	        await Navigation.PushAsync(new LoginPage());
	        TeamListView.SelectedItem = null;
	    }

	    private async void AddTeam_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new NewTeamPage());
	    }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();

	        if (viewModel.Teams.Count == 0)
	        {
                viewModel.LoadTeamsCommand.Execute(null);
	        }
	    }
	}
}