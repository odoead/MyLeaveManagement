using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Controllers;
using MyLeaveManagement.Data;
using MyLeaveManagement.Mappings;
using MyLeaveManagement.Models;
using MyLeaveManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Controller.mocks;
using Tests.mocks;

namespace Tests.Controller
{
    public class LeaveAllocationTests__EF
    {
        private readonly Mock<ILeaveAllocationRepository> mockLeaveAllocRepo;
        private readonly Mock<ILeaveTypeRepository> mockLeaveTypeRepository;
        private readonly LeaveAllocationController leaveAllocController;
        private readonly Mock<UserManager<Employee>> mockUser;
        public LeaveAllocationTests__EF()
        {
            mockLeaveAllocRepo = MockLeaveAllocationRepository.GetMock();
            mockLeaveTypeRepository = MockLeaveTypeRepository.GetMock();
            mockUser = MockUserManager.GetMock();
            var mapper = GetMapper();
            leaveAllocController =
                new LeaveAllocationController(mapper, mockLeaveAllocRepo.Object, mockLeaveTypeRepository.Object, mockUser.Object);

        }

        public IMapper GetMapper()
        {
            var mappingProfile = new Maps();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public async Task Index_ModelType_IsCreateLeaveAllocationVM()
        {
            //ARRANGE


            //ACT
            var result = await leaveAllocController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CreateLeaveAllocationViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task SetLeave_ActionExecutes_CreatedLeaveForEachEmployee()
        {
            //ARRANGE
            LeaveAllocation? leaveType = null;
            int id = 1;
            mockLeaveAllocRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveAllocation>()))
                .Callback<LeaveAllocation>(x => leaveType = x);

            //ACT
            var result = await leaveAllocController.SetLeave(id) as ViewResult;
            //ASSERT
            mockLeaveAllocRepo.Verify(x => x.CreateAsync(It.IsAny<LeaveAllocation>()), Times.Exactly(1));
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ListEmployees_ReturnsViewWithModel()
        {
            // ARRANGE


            //ACT
            var result = await leaveAllocController.ListEmployees();

            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<EmployeeViewModel>>(viewResult.Model);
        }

        [Fact]
        public async Task Details_ReturnsViewWithModel()
        {
            // ARRANGE


            //ACT
            var result = await leaveAllocController.Details("a3f");

            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ViewAllocationsViewModel>(viewResult.Model);

        }

        [Fact]
        public async Task Details_ReturnsExactNumberOfAlloc()
        {
            // ARRANGE


            //ACT
            var result = await leaveAllocController.Details("a3f");

            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ViewAllocationsViewModel>(viewResult.Model);
            Assert.NotNull(model);
            Assert.NotEmpty(model.leaveAllocationViewModel);
            Assert.Equal(1, model.leaveAllocationViewModel.Count);
        }

        [Fact]
        public async Task EditGET_ActionExecutes_ReturnsViewForEdit()
        {
            //ARRANGE
            int id = 1;

            //ACT
            var result = await leaveAllocController.Edit(id) as ViewResult;
            //ASSERT

            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<EditLeaveAllocationViewModel>(viewResult.Model);
            Assert.Equal(1, leave.id);

        }
        [Fact]
        public async Task EditPOST_ValidModel_RedirectsToDetails()
        {
            // ARRANGE
            /*EditLeaveAllocationViewModel EditLeaveAllocationVM;
            LeaveType? alloc = null;
           *//* mockLeaveAllocRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveAllocation>()))
                .Callback<LeaveType>(x => alloc = x);*/

            var leaveAllocVM = new EditLeaveAllocationViewModel()
            {
                LeaveType = new LeaveTypeViewModel
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 10,
                    Id = 1,
                    Name = "sick leave"
                },
                Employee = new EmployeeViewModel
                {
                    Id = "a3f",
                    DateJoined = Convert.ToDateTime("02.12.2023 1:32:57"),
                    Email = "request@gmail.com",
                    UserName = "request@gmail.com",
                    FirstName = "firstrequest",
                    LastName = "lastrequest",
                },
                EmployeeId = "a3f",
                id = 1,
                NumberOfDays = 5,

            };
            //ACT
            var result = await leaveAllocController.Edit(leaveAllocVM);

            //ASSERT
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            Assert.Equal("a3f", redirectToActionResult.RouteValues["id"]);
        }


        [Fact]
        public async Task EditPOST_InvalidModelState_UpdateNeverExecutes()
        {
            //ARRANGE
            var leaveAllocVM = new EditLeaveAllocationViewModel()
            {
                LeaveType = new LeaveTypeViewModel
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 2222,
                    Id = 1,
                    Name = "sick leave"
                },
                Employee = new EmployeeViewModel
                {
                    Id = "a3f",
                    DateJoined = Convert.ToDateTime("02.12.2023 1:32:57"),
                    Email = "request@gmail.com",
                    UserName = "request@gmail.com",
                    FirstName = "firstrequest",
                    LastName = "lastrequest",
                },
                EmployeeId = "a3f",
                id = 1,
                NumberOfDays = 5,

            };
            leaveAllocController.ModelState.AddModelError("DefaultDays", "Enter valid days");

            //ACT
            var result = await leaveAllocController.Edit(leaveAllocVM);
            //ASSERT
            mockLeaveAllocRepo.Verify(x => x.UpdateAsync(It.IsAny<LeaveAllocation>()), Times.Never);
        }

        [Fact]
        public async Task EditPOST_ErrorWhileUpdating()
        {
            //ARRANGE
            var leaveAllocVM = new EditLeaveAllocationViewModel()
            {
                LeaveType = new LeaveTypeViewModel
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 10,
                    Id = 1,
                    Name = "sick leave"
                },
                Employee = new EmployeeViewModel
                {
                    Id = "a3f",
                    DateJoined = Convert.ToDateTime("02.12.2023 1:32:57"),
                    Email = "request@gmail.com",
                    UserName = "request@gmail.com",
                    FirstName = "firstrequest",
                    LastName = "lastrequest",
                },
                EmployeeId = "a3f",
                id = 1,
                NumberOfDays = 5,

            };
            leaveAllocController.ModelState.AddModelError("", "Error while editing");

            //ACT
            var result = await leaveAllocController.Edit(leaveAllocVM)as ViewResult;
            //ASSERT

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EditLeaveAllocationViewModel>(viewResult.Model);
            

        }





    }
}
