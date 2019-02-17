using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using TSM.Dto;
using TSM.Models;
using TSM.Services;
using TSM.ViewModels.WorkUnitVM;
using Xamarin.Forms;
using Xunit;

namespace TSM.UnitTests
{
    public class SingleDayViewModelTests
    {
        [Fact]
        public void LoadWorkUnit_Should_Set_IsBusy_To_False()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewmodel = new SingleDayViewModel(apiService, navigation, schedule, new List<Member>(), DayOfWeek.Monday);

            //Act
            viewmodel.LoadWorkUnitsCommand.Execute(null);

            //Assert
            viewmodel.IsBusy.Should().BeFalse();
        }

        [Fact]
        public void LoadWorkUnit_Should_Set_WorkUnits()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            var workUnits = new List<WorkUnit>
            {
                new WorkUnit() {DayOfWeek = DayOfWeek.Monday, MemberId = 1}
            };
            var members = new List<Member>
            {
                new Member {Id = 1, User = new User {FirstName = "test", LastName = "test"}}
            };
            apiService.GetAll<WorkUnit>(Arg.Any<string>()).ReturnsForAnyArgs(workUnits);
            var viewmodel = new SingleDayViewModel(apiService, navigation, schedule, members, DayOfWeek.Monday);

            //Act
            viewmodel.LoadWorkUnitsCommand.Execute(null);

            //Assert
            viewmodel.MembersWorkUnitsList.Count.Should().Be(1);
            viewmodel.MembersWorkUnitsList[0].WorkUnits.Should().BeEquivalentTo(workUnits);
        }

        [Fact]
        public void AddWorkUnit_Should_Call_Service_Get_Method_With_Proper_Url()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var schedule = new Schedule() { Id = 1 };
            var apiService = Substitute.For<IApiService>();
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewModel = new SingleDayViewModel(apiService, navigation, schedule, new List<Member>(), DayOfWeek.Monday);

            //Act
            viewModel.LoadWorkUnitsCommand.Execute(null);

            //Assert
            apiService.Received()
                .GetAll<WorkUnit>($"workunit/{schedule.Id}/{DayOfWeek.Monday}");
        }
    }
}
