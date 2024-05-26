using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Controllers;
using MyLeaveManagement.Data;
using MyLeaveManagement.Mappings;
using MyLeaveManagement.Models;
using Tests.mocks;

namespace Tests
{
    public class LeaveTypeTests__EF
    {
        private readonly Mock<ILeaveTypeRepository> mockLeaveTypeRepo;
        private readonly LeaveTypesController leaveTypesController;

        public LeaveTypeTests__EF()
        {
            mockLeaveTypeRepo = MockLeaveTypeRepository.GetMock();
            var mapper = GetMapper();
            leaveTypesController = new LeaveTypesController(mapper, mockLeaveTypeRepo.Object);
        }

        public IMapper GetMapper()
        {
            var mappingProfile = new Maps();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        [Fact]
        public async Task Index_ModelType_IsLeaveTypeVM()
        {
            //ARRANGE
            //ACT
            var result = await leaveTypesController.Index() as ViewResult;
            //ASSERT
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<List<LeaveTypeViewModel>>(viewResult.Model);
        }

        [Fact]
        public async Task Index_Model_ReturnExactList()
        {
            //ARRANGE
            //ACT
            var result = await leaveTypesController.Index() as ViewResult;
            //ASSERT
            Assert.NotNull(leaveTypesController);
            var viewResult = Assert.IsType<ViewResult>(result);
            var leaves = Assert.IsType<List<LeaveTypeViewModel>>(viewResult.Model);
            Assert.Equal(1, leaves.Count);
        }

        [Fact]
        public void CreateGET_ActionExecutes_ReturnsViewForCreate()
        {
            //ARRANGE
            //ACT
            var result = leaveTypesController.Create();
            //ASSERT
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreatePOST_InvalidModelState_ReturnsView()
        {
            //ARRANGE
            leaveTypesController.ModelState.AddModelError("DefaultDays", "Enter valid days");
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 40,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            var result = await leaveTypesController.Create(leaveTypeVM);
            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<LeaveTypeViewModel>(viewResult.Model);
            Assert.Equal(leaveTypeVM.Id, leave.Id);
            Assert.Equal(leaveTypeVM.DefaultDays, leave.DefaultDays);
        }

        [Fact]
        public async Task CreatePOST_InvalidModelState_CreateLeaveTypeNeverExecutes()
        {
            //ARRANGE
            leaveTypesController.ModelState.AddModelError("DefaultDays", "Enter valid days");
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 40,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            var result = await leaveTypesController.Create(leaveTypeVM);
            //ASSERT
            mockLeaveTypeRepo.Verify(x => x.CreateAsync(It.IsAny<LeaveType>()), Times.Never);
        }

        [Fact]
        public async Task CreatePOST_ModelStateValid_CreateLeaveTypeCalledOnce()
        {
            //ARRANGE
            LeaveType? leaveType = null;
            mockLeaveTypeRepo
                .Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Callback<LeaveType>(x => leaveType = x);
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 10,
                Id = 1,
                Name = "sick leave",
            };
            //ACT
            await leaveTypesController.Create(leaveTypeVM);
            //ASSERT
            mockLeaveTypeRepo.Verify(x => x.CreateAsync(It.IsAny<LeaveType>()), Times.Once);
            Assert.Equal(leaveType.Id, leaveTypeVM.Id);
            Assert.Equal(leaveType.DefaultDays, leaveTypeVM.DefaultDays);
            Assert.Equal(leaveType.Name, leaveTypeVM.Name);
        }

        [Fact]
        public async Task CreatePOST_ActionExecuted_RedirectsToIndexAction()
        {
            //ARRANG
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 10,
                Id = 1,
                Name = "sick leave",
            };
            //ACT
            var result = await leaveTypesController.Create(leaveTypeVM);
            //ASSERT
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Details_ModelType_IsLeaveTypeVM()
        {
            //ARRANGE
            int id = 1;
            //ACT
            var result = await leaveTypesController.Details(id);
            //ASSERT
            mockLeaveTypeRepo.Verify(x => x.IsExistsAsync(id), Times.Once);
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<LeaveTypeViewModel>(viewResult.Model);
        }

        [Fact]
        public async Task Details_Model_ReturnExactType()
        {
            //ARRANGE
            int id = 1;
            //ACT
            var result = await leaveTypesController.Details(id);
            //ASSERT
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<LeaveTypeViewModel>(viewResult.Model);
            Assert.Equal(1, leave.Id);
        }

        [Fact]
        public async Task Details_WrongModel_ReturnNotFound()
        {
            //ARRANGE
            int id = 10;
            //ACT
            var result = await leaveTypesController.Details(id);
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditGET_ActionExecutes_ReturnsViewForEdit()
        {
            //ARRANGE
            int id = 1;
            //ACT
            var result = await leaveTypesController.Edit(id) as ViewResult;
            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<LeaveTypeViewModel>(viewResult.Model);
            Assert.Equal(1, leave.Id);
        }

        [Fact]
        public async Task EditGET_InvalidModelState_ReturnsNotFound()
        {
            //ARRANGE
            int id = 10;
            //ACT
            var result = await leaveTypesController.Details(id);
            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditPOST_InvalidModelState_ReturnsView()
        {
            //ARRANGE
            leaveTypesController.ModelState.AddModelError("DefaultDays", "Enter valid days");
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 40,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            var result = await leaveTypesController.Edit(leaveTypeVM);
            //ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var leave = Assert.IsType<LeaveTypeViewModel>(viewResult.Model);
            Assert.Equal(leaveTypeVM.Id, leave.Id);
            Assert.Equal(leaveTypeVM.DefaultDays, leave.DefaultDays);
        }

        [Fact]
        public async Task EditPOST_InvalidModelState_UpdateNeverExecutes()
        {
            //ARRANGE
            leaveTypesController.ModelState.AddModelError("DefaultDays", "Enter valid days");
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 40,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            var result = await leaveTypesController.Edit(leaveTypeVM);
            //ASSERT
            mockLeaveTypeRepo.Verify(x => x.UpdateAsync(It.IsAny<LeaveType>()), Times.Never);
        }

        [Fact]
        public async Task EditPOST_ModelStateValid_EitLeaveTypeCalledOnce()
        {
            //ARRANGE
            LeaveType? leaveType = null;
            mockLeaveTypeRepo
                .Setup(r => r.UpdateAsync(It.IsAny<LeaveType>()))
                .Callback<LeaveType>(x => leaveType = x);
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 10,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            await leaveTypesController.Edit(leaveTypeVM);
            //ASSERT
            mockLeaveTypeRepo.Verify(x => x.UpdateAsync(It.IsAny<LeaveType>()), Times.Once);
            Assert.Equal(leaveType.Id, leaveTypeVM.Id);
            Assert.Equal(leaveType.DefaultDays, leaveTypeVM.DefaultDays);
            Assert.Equal(leaveType.Name, leaveTypeVM.Name);
        }

        [Fact]
        public async Task EitPOST_ActionExecuted_RedirectsToIndexAction()
        {
            //ARRANG
            var leaveTypeVM = new LeaveTypeViewModel()
            {
                DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                DefaultDays = 10,
                Id = 1,
                Name = "sick leave"
            };
            //ACT
            var result = await leaveTypesController.Edit(leaveTypeVM);
            //ASSERT
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
