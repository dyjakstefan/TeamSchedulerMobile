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
    public class EditWorkUnitViewModelTests
    {
        [Fact]
        public void EditWorkUnit_Should_Set_IsBusy_To_False()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var apiService = Substitute.For<IApiService>();
            var workUnits = new List<WorkUnit> {new WorkUnit {DayOfWeek = DayOfWeek.Friday}};
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewModel = new EditWorkUnitViewModel(apiService, navigation, 1, new MemberList(workUnits));

            //Act
            viewModel.EditWorkUnitCommand.Execute(null);

            //Assert
            viewModel.IsBusy.Should().BeFalse();
        }

        [Fact]
        public void EditWorkUnit_Should_Call_Service_Edit_Method_With_EditedWorkUnit()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var apiService = Substitute.For<IApiService>();
            var workUnits = new List<WorkUnit> {new WorkUnit {DayOfWeek = DayOfWeek.Friday}};
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewModel = new EditWorkUnitViewModel(apiService, navigation, 1, new MemberList(workUnits) {MemberId = 1});

            //Act
            viewModel.EditWorkUnitCommand.Execute(null);

            //Assert
            apiService.Received()
                .Update(Arg.Is<WorkUnitListDto>(x => x.ScheduleId == 1 && x.MemberId == 1), "workunit/list");
        }

        [Fact]
        public void DeleteWorkUnit_Should_Delete_Given_WorkUnit_From_List()
        {
            //Arrange
            var navigation = Substitute.For<INavigation>();
            var apiService = Substitute.For<IApiService>();
            var workUnits = new List<WorkUnit> {new WorkUnit {DayOfWeek = DayOfWeek.Friday}};
            apiService.Add(Arg.Any<object>(), Arg.Any<string>()).ReturnsForAnyArgs(Task.CompletedTask);
            var viewModel = new EditWorkUnitViewModel(apiService, navigation, 1, new MemberList(workUnits));

            //Act
            viewModel.OnDeleteWorkUnitCommand.Execute(viewModel.WorkUnits[0]);

            //Assert
            viewModel.WorkUnits.Count.Should().Be(0);
        }
    }
}
