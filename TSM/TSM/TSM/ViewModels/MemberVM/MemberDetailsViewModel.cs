using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using Xamarin.Forms;

namespace TSM.ViewModels.MemberVM
{
    public class MemberDetailsViewModel : BaseViewModel
    {
        private IApiService apiService => DependencyService.Get<IApiService>() ?? new ApiService();

        private int hours;

        private Member member;

        public int Hours
        {
            get { return hours; }
            set { SetProperty(ref hours, value); }
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
        }

        protected async Task EditMember()
        {
            IsBusy = true;
            try
            {
                member.Hours = Hours;
                var memberDto = new MemberDto { Hours = hours, MemberId = member.Id, TeamId = member.TeamId };
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
