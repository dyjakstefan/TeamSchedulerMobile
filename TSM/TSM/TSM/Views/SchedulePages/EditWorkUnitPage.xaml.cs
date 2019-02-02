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
	public partial class EditWorkUnitPage : ContentPage
	{
	    private EditWorkUnitViewModel viewModel;

	    public EditWorkUnitPage(int scheduleId, MemberList memberList)
	    {
	        InitializeComponent();
	        viewModel = new EditWorkUnitViewModel(Navigation, scheduleId, memberList);
	        BindingContext = viewModel;
	    }
    }
}