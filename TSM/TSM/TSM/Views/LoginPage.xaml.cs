using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.ViewModels;
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
            viewModel = new LoginViewModel();
		    BindingContext = viewModel;
		    Email.Completed += (sender, e) => { Password.Focus(); };
		    Password.Completed += (sender, e) => { viewModel.SubmitCommand.Execute(null); };
		}
	}
}