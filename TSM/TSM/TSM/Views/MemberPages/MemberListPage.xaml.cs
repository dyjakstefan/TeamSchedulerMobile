using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
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
	        viewModel = new MemberListViewModel(Navigation, team);
	        BindingContext = viewModel;
	    }

	    private async void OnMemberSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        var member = args.SelectedItem as Member;
	        if (member == null)
	        {
	            return;
	        }

	        await Navigation.PushAsync(new LoginPage());
	        MemberListView.SelectedItem = null;
	    }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Members.Count == 0)
            {
                viewModel.LoadMembersCommand.Execute(null);
            }
        }
    }
}