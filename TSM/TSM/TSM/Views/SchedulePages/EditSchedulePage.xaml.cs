using TSM.Models;
using TSM.ViewModels.ScheduleVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSchedulePage : ContentPage
	{
	    private EditScheduleViewModel viewModel;

	    public EditSchedulePage(Schedule schedule)
	    {
	        InitializeComponent();
	        viewModel = new EditScheduleViewModel(Navigation, schedule);
	        BindingContext = viewModel;
	        NameEntry.Completed += (sender, e) => { viewModel.EditScheduleCommand.Execute(null); };
	    }
    }
}