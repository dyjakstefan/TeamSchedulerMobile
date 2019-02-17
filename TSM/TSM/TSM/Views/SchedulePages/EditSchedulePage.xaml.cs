using Autofac;
using TSM.Models;
using TSM.Services;
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
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = new EditScheduleViewModel(App.Container.Resolve<IApiService>(), Navigation, schedule);
	        }
	        BindingContext = viewModel;
	        NameEntry.Completed += (sender, e) => { viewModel.EditScheduleCommand.Execute(null); };
	    }
    }
}