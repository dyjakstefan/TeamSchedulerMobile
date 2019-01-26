using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSM.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TSM.Views.SchedulePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : TabbedPage
    {
        public SchedulePage (Schedule schedule)
        {
            InitializeComponent();
            Children.Add(new SummaryPage(schedule));
            Children.Add(new SingleDayPage(schedule, DayOfWeek.Monday));
        }
    }
}