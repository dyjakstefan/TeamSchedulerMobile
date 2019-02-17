using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.WorkUnitVM;
using Xamarin.Forms;
using Xunit;

namespace TSM.UnitTests
{
    public class NewWorkUnitViewModelTests
    {
        [Fact]
        public void AddWorkUnit_Should_Set_IsBusy_To_False()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewmodel = new NewWorkUnitViewModel(apiService);
            viewmodel.Initialize(navigation, schedule, new List<Member>(), DayOfWeek.Monday);
            viewmodel.SelectedMember = new Member() { Id = 1, UserId = 1 };

            //Act
            viewmodel.AddWorkUnitCommand.Execute(null);

            //Assert
            viewmodel.IsBusy.Should().BeFalse();
        }

        [Fact]
        public void AddWorkUnit_Should_Call_Service_Add_Method_With_AddedWorkUnit()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewModel = new NewWorkUnitViewModel(apiService);
            viewModel.Initialize(navigation, schedule, new List<Member>(), DayOfWeek.Monday);
            var selectedMember = new Member() { Id = 1 };
            viewModel.SelectedMember = selectedMember;

            //Act
            viewModel.AddWorkUnitCommand.Execute(null);

            //Assert
            apiService.Received()
                .Add(Arg.Is<WorkUnitListDto>(x => x.ScheduleId == schedule.Id && x.MemberId == selectedMember.Id), "workunit/list");
        }

        [Fact]
        public void AddWorkUnitEntry_Should_Add_New_WorkUnit_To_List()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            var viewModel = new NewWorkUnitViewModel(apiService);
            viewModel.Initialize(navigation, schedule, new List<Member>(), DayOfWeek.Monday);
            var quantity = viewModel.WorkUnits.Count;

            //Act
            viewModel.OnAddWorkUnitEntryCommand.Execute(null);

            //Assert
            viewModel.WorkUnits.Count.Should().Be(2).And.BeGreaterThan(quantity);
        }

        [Fact]
        public void DeleteWorkUnit_Should_Delete_Given_WorkUnit_From_List()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            var viewModel = new NewWorkUnitViewModel(apiService);
            viewModel.Initialize(navigation, schedule, new List<Member>(), DayOfWeek.Monday);

            //Act
            viewModel.OnDeleteWorkUnitCommand.Execute(viewModel.WorkUnits[0]);

            //Assert
            viewModel.WorkUnits.Count.Should().Be(0);
        }
    }
}
