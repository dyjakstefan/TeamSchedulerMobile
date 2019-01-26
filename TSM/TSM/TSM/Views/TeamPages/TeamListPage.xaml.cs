using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.ViewModels;
using TSM.ViewModels.TeamVM;
using TSM.Views.MemberPages;
using TSM.Views.SchedulePages;
using TSM.Views.TeamPages;
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

	    private async void OnTeamSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var team = args.SelectedItem as Team;
	        if (team == null)
	        {
	            return;
            }

	        await Navigation.PushAsync(new MainPage(team));
	        TeamListView.SelectedItem = null;
	    }

	    private async void OnAddTeamClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new NewTeamPage());
	    }

	    private async void OnEditTeamClicked(object sender, EventArgs e)
	    {
	        var menuItem = (MenuItem)sender;
	        var team = (Team)menuItem.CommandParameter;
            await Navigation.PushAsync(new EditTeamPage(team));
	    }

	    private void OnDeleteTeamClicked(object sender, EventArgs e)
	    {
            var menuItem = (MenuItem)sender;
            var team = (Team)menuItem.CommandParameter;
	        viewModel.DeleteTeamCommand.Execute(team);
	    }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
            viewModel.LoadTeamsCommand.Execute(null);
	    }
	}
}