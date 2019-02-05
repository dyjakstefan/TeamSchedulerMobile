using System;
using System.Collections.Generic;
using TSM.Models;
using TSM.ViewModels.WorkUnitVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.WorkUnitPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewWorkUnitPage : ContentPage
	{
	    private NewWorkUnitViewModel viewModel;

	    public NewWorkUnitPage(int scheduleId, List<Member> members, DayOfWeek day)
	    {
	        InitializeComponent();
	        viewModel = new NewWorkUnitViewModel(Navigation, scheduleId, members, day);
	        BindingContext = viewModel;
	    }
    }
}