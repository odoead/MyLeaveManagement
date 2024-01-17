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
    public class LeaveRequestTests_EF
    {
        private readonly Mock<ILeaveAllocationRepository> mockLeaveAllocRepo;
        private readonly Mock<ILeaveTypeRepository> mockLeaveTypeRepository;
        private readonly Mock<ILeaveRequestRepository> mockLeaveRequestRepo;
        private readonly LeaveRequestController leaveRequestController;
        private readonly Mock<UserManager<Employee>> mockUser;
        public LeaveRequestTests_EF()
        {
            mockLeaveAllocRepo = MockLeaveAllocationRepository.GetMock();
            mockLeaveTypeRepository = MockLeaveTypeRepository.GetMock();
            mockUser = MockUserManager.GetMock();
            mockLeaveRequestRepo = MockLeaveRequestRepository.GetMock();
            var mapper = GetMapper();
            leaveRequestController = new LeaveRequestController(mapper, mockUser.Object,
            mockLeaveRequestRepo.Object, mockLeaveTypeRepository.Object,
            mockLeaveAllocRepo.Object);

        }

        public IMapper GetMapper()
        {
            var mappingProfile = new Maps();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public async Task Index_ModelType_IsLeaveReqestVM()
        {
            //ARRANGE


            //ACT
            var result = await leaveRequestController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<AdminLeaveRequestViewViewModel>(viewResult.Model);
        }
        [Fact]
        public async Task Index_Model_ReturnExact()
        {
            //ARRANGE
            var expectReq = new AdminLeaveRequestViewViewModel { };


            //ACT
            var result = await leaveRequestController.Index() as ViewResult;

            //ASSERT
            Assert.NotNull(leaveRequestController);
            var viewResult = Assert.IsType<ViewResult>(result);
            var request = Assert.IsType<AdminLeaveRequestViewViewModel>(viewResult.Model);
            Assert.Equal(1, request.TotalRequests);
        }
        [Fact]
        public async Task CreateGET_ActionExecutes_ReturnsViewForCreate()
        {
            //ARRANGE

            //ACT
            var result = await leaveRequestController.Create() as ViewResult;
            //ASSERT

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<CreateLeaveRequestViewModel>(viewResult.Model);


        }

        

            



    }
}
