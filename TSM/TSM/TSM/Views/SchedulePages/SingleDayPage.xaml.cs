using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SingleDayPage : ContentPage
	{
	    private SingleDayViewModel viewModel;

		public SingleDayPage (Schedule schedule, List<Member> members, DayOfWeek day)
		{
			InitializeComponent ();
		    Title = day.ToString();
		    viewModel = new SingleDayViewModel(Navigation, schedule, members, day);
		    BindingContext = viewModel;
		}

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
	        viewModel.LoadTasksCommand.Execute(null);
	    }

	    private async void OnTaskSelected(object sender, SelectedItemChangedEventArgs args)
	    {
	        //var team = args.SelectedItem as Team;
	        //if (team == null)
	        //{
	        //    return;
	        //}

	        //await Navigation.PushAsync(new EditTaskPage(team));
	        //TasksListView.SelectedItem = null;
	    }
    }
}