using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Enums;
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