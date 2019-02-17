using Autofac;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.MemberVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.MemberPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MemberListPage : ContentPage
	{
	    private MemberListViewModel viewModel;

	    public MemberListPage(Team team)
	    {
	        InitializeComponent();
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = new MemberListViewModel(App.Container.Resolve<IApiService>(), Navigation, team);
	        }
	        BindingContext = viewModel;
	    }

	    private async void OnMemberSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var member = args.SelectedItem as Member;
	        if (member == null)
	        {
	            return;
	        }

	        await Navigation.PushAsync(new MemberDetailsPage(member));
	        MemberListView.SelectedItem = null;
	    }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadMembersCommand.Execute(null);
        }
    }
}