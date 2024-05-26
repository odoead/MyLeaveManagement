using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Controllers;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;

namespace Tests
{
    public class testsGpt
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfLeaveTypeViewModels()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<ILeaveTypeRepository>();
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType()
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 10,
                    Id = 1,
                    Name = "sick leave"
                }
                /* initialize your LeaveType objects here */
            };
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(leaveTypes);
            var leaveTypeViewModels = new List<LeaveTypeViewModel>
            {
                new LeaveTypeViewModel()
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 10,
                    Id = 1,
                    Name = "sick leave"
                }
                /* initialize your LeaveTypeViewModel objects here */
            };
            mapperMock
                .Setup(mapper =>
                    mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(
                        It.IsAny<List<LeaveType>>()
                    )
                )
                .Returns(leaveTypeViewModels);
            var controller = new LeaveTypesController(mapperMock.Object, repositoryMock.Object);
            // Act
            var result = await controller.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<LeaveTypeViewModel>>(viewResult.Model);
            Assert.Equal(leaveTypeViewModels.Count, model.Count);
        }
    }
}
