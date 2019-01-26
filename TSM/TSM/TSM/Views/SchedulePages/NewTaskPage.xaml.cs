using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTaskPage : ContentPage
	{
	    private NewTaskViewModel viewModel;

		public NewTaskPage (int scheduleId)
		{
			InitializeComponent ();
		    InitializeComponent();
		    viewModel = new NewTaskViewModel(Navigation, scheduleId);
		    BindingContext = viewModel;
        }
	}
}