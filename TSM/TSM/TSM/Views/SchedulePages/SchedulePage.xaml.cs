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
        public SchedulePage (Schedule schedule, List<Member> members)
        {
            InitializeComponent();
            Title = schedule.Name;
            Children.Add(new SummaryPage(schedule));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Monday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Tuesday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Wednesday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Thursday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Friday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Saturday));
            Children.Add(new SingleDayPage(schedule, members, DayOfWeek.Sunday));
        }
    }
}