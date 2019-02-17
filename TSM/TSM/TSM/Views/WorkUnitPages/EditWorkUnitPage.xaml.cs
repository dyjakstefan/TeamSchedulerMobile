using Autofac;
using TSM.Models;
using TSM.Services;
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
	        using (var scope = App.Container.BeginLifetimeScope())
	        {
	            viewModel = new EditWorkUnitViewModel(App.Container.Resolve<IApiService>(), Navigation, scheduleId, memberList);
	        }
            BindingContext = viewModel;
	    }
    }
}