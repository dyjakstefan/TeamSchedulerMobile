using TSM.Models;
using TSM.Views.MemberPages;
using TSM.Views.SchedulePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage(Team team)
        {
            InitializeComponent();
            Title = team.Name;
            Children.Add(new ScheduleListPage(team));
            Children.Add(new MemberListPage(team));
        }
    }
}