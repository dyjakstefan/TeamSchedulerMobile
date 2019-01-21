using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.ViewModels.MemberVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.MemberPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewMemberPage : ContentPage
	{
	    private NewMemberViewModel viewModel;

	    public NewMemberPage(int teamId)
	    {
	        InitializeComponent();
	        viewModel = new NewMemberViewModel(Navigation, teamId);
	        BindingContext = viewModel;
	        NameEntry.Completed += (sender, e) => { viewModel.AddMemberCommand.Execute(null); };
	    }
    }
}