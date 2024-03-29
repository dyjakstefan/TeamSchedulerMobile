﻿using System;
using System.Collections.Generic;
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
        private readonly IApiService apiService;

        private int hours;

        private JobTitle jobTitle;

        public Member Member { get; set; }

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

        public MemberDetailsViewModel(IApiService apiService, INavigation navigation, Member member)
        {
            Navigation = navigation;
            this.Member = member;
            Hours = member.Hours;
            JobTitle = member.Title;
            EditMemberCommand = new Command(async () => await EditMember(), () => !IsBusy);
            DeleteMemberCommand = new Command(async () => await DeleteMember(), () => !IsBusy);
            this.apiService = apiService;
            Titles = new List<string>();
            Titles.Add(JobTitle.Manager.ToString());
            Titles.Add(JobTitle.Employee.ToString());
        }

        protected async Task EditMember()
        {
            IsBusy = true;
            try
            {
                Member.Hours = Hours;
                Member.Title = JobTitle;
                var memberDto = new MemberDto { Hours = Member.Hours, MemberId = Member.Id, TeamId = Member.TeamId, Title = Member.Title };
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
                var memberDto = new MemberDto { MemberId = Member.Id, TeamId = Member.TeamId };
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
