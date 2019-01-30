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
	public partial class NewWorkUnitPage : ContentPage
	{
	    private NewWorkUnitViewModel viewModel;

	    public NewWorkUnitPage(int scheduleId, List<Member> members, DayOfWeek day)
	    {
	        InitializeComponent();
	        InitializeComponent();
	        viewModel = new NewWorkUnitViewModel(Navigation, scheduleId, members, day);
	        BindingContext = viewModel;
	    }
    }
}