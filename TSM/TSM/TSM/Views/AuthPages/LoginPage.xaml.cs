using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.ViewModels;
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
            viewModel = new LoginViewModel(Navigation);
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