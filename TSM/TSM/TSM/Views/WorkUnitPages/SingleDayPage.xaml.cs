using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.WorkUnitVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.WorkUnitPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SingleDayPage : ContentPage
	{
	    private SingleDayViewModel viewModel;

		public SingleDayPage (Schedule schedule, List<Member> members, DayOfWeek day)
		{
			InitializeComponent ();
            var polish = new CultureInfo("pl-PL");
		    Title = polish.DateTimeFormat.DayNames[(int)day];
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new SingleDayViewModel(App.Container.Resolve<IApiService>(), Navigation, schedule, members, day);
		    }
            BindingContext = viewModel;
		}

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
	        viewModel.LoadWorkUnitsCommand.Execute(null);
	    }

	    private async void OnWorkUnitSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var workUnit = args.SelectedItem as WorkUnit;
            if (workUnit == null || !(viewModel.HasManagerPermissions || viewModel.HasCreatorPermissions))
            {
                WorkUnitsListView.SelectedItem = null;
                return;
            }

            var memberList = viewModel.MembersWorkUnitsList.SingleOrDefault(x => x.MemberId == workUnit.MemberId);
            await Navigation.PushAsync(new EditWorkUnitPage(viewModel.Schedule.Id, memberList));
            WorkUnitsListView.SelectedItem = null;
        }
    }
}