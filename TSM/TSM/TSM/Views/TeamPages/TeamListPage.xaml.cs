using System;
using Autofac;
using TSM.Helpers;
using TSM.Models;
using TSM.Services;
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
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new TeamListViewModel(App.Container.Resolve<IApiService>(), Navigation);
		    }
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