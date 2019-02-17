using System;
using Autofac;
using TSM.Enums;
using TSM.Services;
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
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = new NewMemberViewModel(App.Container.Resolve<IApiService>(), Navigation, teamId);
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