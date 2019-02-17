using Autofac;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.TeamVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.TeamPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTeamPage : ContentPage
    {
        private EditTeamViewModel viewModel;

        public EditTeamPage(Team team)
        {
            InitializeComponent();
            Title = $"Edytuj {team.Name}";
            using (var scope = App.Container.BeginLifetimeScope())
            {
                viewModel = new EditTeamViewModel(App.Container.Resolve<IApiService>(), Navigation, team);
            }
            BindingContext = viewModel;
            NameEntry.Completed += (sender, e) => { viewModel.EditTeamCommand.Execute(null); };
        }
    }
}