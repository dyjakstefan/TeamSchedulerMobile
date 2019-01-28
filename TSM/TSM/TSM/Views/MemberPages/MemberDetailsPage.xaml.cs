using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Enums;
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
		    Title = member.User.FullName;
		    viewModel = new MemberDetailsViewModel(Navigation, member);
		    BindingContext = viewModel;
		    TitlePicker.SelectedIndex = (int)viewModel.JobTitle;
		}

	    protected void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	    {
	        var picker = sender as Picker;
	        if (picker.SelectedIndex != -1)
	        {
	            viewModel.JobTitle = (JobTitle)picker.SelectedIndex;
	        }
	    }
    }
}