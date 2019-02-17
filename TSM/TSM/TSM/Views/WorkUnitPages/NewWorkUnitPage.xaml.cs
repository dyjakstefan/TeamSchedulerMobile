using System;
using System.Collections.Generic;
using Autofac;
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

	    public NewWorkUnitPage(Schedule schedule, List<Member> members, DayOfWeek day)
	    {
	        InitializeComponent();
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = App.Container.Resolve<NewWorkUnitViewModel>();
	        }
	        viewModel.Initialize(Navigation, schedule, members, day);
	        BindingContext = viewModel;
	    }
    }
}