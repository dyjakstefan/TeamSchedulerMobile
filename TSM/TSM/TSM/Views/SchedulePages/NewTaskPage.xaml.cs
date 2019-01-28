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
	public partial class NewTaskPage : ContentPage
	{
	    private NewTaskViewModel viewModel;

		public NewTaskPage (int scheduleId, List<Member> members)
		{
			InitializeComponent ();
		    InitializeComponent();
		    viewModel = new NewTaskViewModel(Navigation, scheduleId, members);
		    BindingContext = viewModel;
        }

	    protected void OnPickerSelectedIndexChanged(object sender, EventArgs e)
	    {
	        //var picker = sender as Picker;
	        //if (picker.SelectedIndex != -1)
	        //{
	        //    viewModel.JobTitle = (JobTitle)picker.SelectedIndex;
	        //}
	    }
    }
}