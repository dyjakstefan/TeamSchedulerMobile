using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Enums;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.MemberVM
{
    public class MemberDetailsViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int hours;

        private JobTitle jobTitle;

        private Member member;

        public int Hours
        {
            get { return hours; }
            set { SetProperty(ref hours, value); }
        }

        public JobTitle JobTitle
        {
            get { return jobTitle; }
            set { SetProperty(ref jobTitle, value); }
        }

        public new bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                EditMemberCommand.ChangeCanExecute();
                DeleteMemberCommand.ChangeCanExecute();
            }
        }

        public List<string> Titles { get; set; }

        public INavigation Navigation { get; set; }

        public Command EditMemberCommand { get; protected set; }

        public Command DeleteMemberCommand { get; protected set; }

        public MemberDetailsViewModel(INavigation navigation, Member member)
        {
            EditMemberCommand = new Command(async () => await EditMember(), () => !IsBusy);
            DeleteMemberCommand = new Command(async () => await DeleteMember(), () => !IsBusy);
            Navigation = navigation;
            this.member = member;
            Hours = member.Hours;
            JobTitle = member.Title;
            Titles = new List<string>();
            Titles.Add(JobTitle.Manager.ToString());
            Titles.Add(JobTitle.Employee.ToString());
        }

        protected async Task EditMember()
        {
            IsBusy = true;
            try
            {
                member.Hours = Hours;
                member.Title = JobTitle;
                var memberDto = new MemberDto { Hours = member.Hours, MemberId = member.Id, TeamId = member.TeamId, Title = member.Title };
                await apiService.Update(memberDto, "members");
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async Task DeleteMember()
        {
            IsBusy = true;
            var shouldDelete = await Application.Current.MainPage.DisplayAlert("Delete", "Do you want to delete that team", "Ok", "Cancel");
            if (!shouldDelete)
                return;

            try
            {
                var memberDto = new MemberDto { MemberId = member.Id, TeamId = member.TeamId };
                await apiService.Delete(memberDto, "members");
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
