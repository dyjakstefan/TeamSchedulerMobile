using System;
using Autofac;
using TSM.Services;
using TSM.ViewModels.AuthVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    private LoginViewModel viewModel;

		public LoginPage ()
		{
		    InitializeComponent();
		    using (var scope = App.Container.BeginLifetimeScope())
		    {
		        viewModel = new LoginViewModel(App.Container.Resolve<IAuthService>(), Navigation);
		    }

		    BindingContext = viewModel;
		    Email.Completed += (sender, e) => { Password.Focus(); };
		    Password.Completed += (sender, e) => { viewModel.SubmitCommand.Execute(null); };
		}

	    private async void SignUpClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new RegisterPage());
	    }

    }
}