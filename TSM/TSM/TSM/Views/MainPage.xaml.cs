using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item != null)
            {
                if (item.Id == 2)
                {
                    Settings.Logout();
                    App.Instance.CleanNavigation();
                    return;
                }

                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                MasterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}