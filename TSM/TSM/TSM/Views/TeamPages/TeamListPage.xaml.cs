using System;
using TSM.Helpers;
using TSM.Models;
using TSM.ViewModels.TeamVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.TeamPages
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

	    private async void OnTeamSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var team = args.SelectedItem as Team;
	        if (team == null)
	        {
	            return;
            }

            Settings.UpdatePermissions(team.Members);
	        await Navigation.PushAsync(new TeamPage(team));
	        TeamListView.SelectedItem = null;
	    }

	    private async void OnAddTeamClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new NewTeamPage());
	    }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
            viewModel.LoadTeamsCommand.Execute(null);
	    }
	}
}