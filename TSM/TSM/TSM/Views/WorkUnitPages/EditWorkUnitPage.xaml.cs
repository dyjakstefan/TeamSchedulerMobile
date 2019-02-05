using TSM.Models;
using TSM.ViewModels.WorkUnitVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.WorkUnitPages
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