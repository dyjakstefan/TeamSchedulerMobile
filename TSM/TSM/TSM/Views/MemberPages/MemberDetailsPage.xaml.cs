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
	public partial class MemberDetailsPage : ContentPage
	{
	    private MemberDetailsViewModel viewModel;

		public MemberDetailsPage (Member member)
		{
		    InitializeComponent();
		    viewModel = new MemberDetailsViewModel(Navigation, member);
		    BindingContext = viewModel;
		    HoursEntry.Completed += (sender, e) => { viewModel.EditMemberCommand.Execute(null); };
        }
	}
}