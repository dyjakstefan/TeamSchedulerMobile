using System;
using Autofac;
using TSM.Enums;
using TSM.Models;
using TSM.Services;
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
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new MemberDetailsViewModel(App.Container.Resolve<IApiService>(), Navigation, member);
		    }
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