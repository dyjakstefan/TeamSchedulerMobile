using Autofac;
using TSM.Services;
using TSM.ViewModels.AuthVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
	    private RegisterViewModel viewModel;

		public RegisterPage ()
		{
			InitializeComponent ();
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new RegisterViewModel(App.Container.Resolve<IAuthService>(), Navigation);
		    }
            BindingContext = viewModel;
		    FirstName.Completed += (sender, e) => { LastName.Focus(); };
		    LastName.Completed += (sender, e) => { Email.Focus(); };
		    Email.Completed += (sender, e) => { Password.Focus(); };
		    Password.Completed += (sender, e) => { viewModel.RegisterCommand.Execute(null); };
        }
	}
}