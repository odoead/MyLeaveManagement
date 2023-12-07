using Moq;
using MyLeaveManagement.Data;
using MyLeaveManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Tests.controller.mocks
{
    internal class MockLeaveRequestRepository
    {
        public static Mock<LeaveRequestRepository> GetMock()
        {
            var mock = new Mock<LeaveRequestRepository>();

            var allocations = new List<LeaveRequest>()
            {
                new LeaveRequest()
                {
                    Id = 1,
                    RequestingEmployee = new Employee()
                    {
                        Id="ba9581c0",
                        DateJoined=Convert.ToDateTime("02.12.2023 1:32:57"),
                        Email="request@gmail.com",
                        UserName="request@gmail.com",
                        FirstName="firstrequest",
                        LastName="lastrequest"
                    },
                    ApprovedBy=new Employee()
                    {
                        Id="0add2386878f",
                        DateJoined=Convert.ToDateTime("02.12.2023 1:40:57"),
                        Email="approve@gmail.com",
                        UserName="approve@gmail.com",
                        FirstName="firstapprove",
                        LastName="lastapprove"
                    },
                    ApprovedById="0add2386878f",
                    LeaveTypeId=1,
                    RequestingEmpoyeeId="ba9581c0",
                    DateRequested=Convert.ToDateTime("02.13.2023 1:40:57"),
                    DateProvided=Convert.ToDateTime("02.14.2023 1:40:57"),
                    IsApproved=true,
                    EndDate=Convert.ToDateTime("02.16.2023 1:40:57"),
                    StartDate=Convert.ToDateTime("02.15.2023 1:40:57"),
                    LeaveType=new LeaveType()
                    {
                        DateCreated=Convert.ToDateTime("02.12.2023 1:32:57"),
                        DefaultDays=10,
                        Id=1,
                        Name="sick leave"
                    },

                },

            };
            mock.Setup(m => m.CreateAsync(It.IsAny<LeaveRequest>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.DeleteAsync(It.IsAny<LeaveRequest>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
                .Returns((LeaveAllocation Alloc) => allocations.FirstOrDefault(a => a.Id == Alloc.Id));
            mock.Setup(m => m.GetAllAsync())
                .Returns((ICollection<LeaveRequest> allocs) => allocs == allocations);
            mock.Setup(m => m.GetRequestsByEmployeeAsync(It.IsAny<string>()))
                .Returns((string id) => allocations.Where(a => a.RequestingEmployee.Id == id).ToList());
            mock.Setup(m => m.IsExistsAsync(It.IsAny<int>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.SaveAsync())
                .Returns((bool q) => q == true);
            mock.Setup(m => m.UpdateAsync(It.IsAny<LeaveRequest>()))
                .Returns((bool q) => q == true);
            return mock;
        }
    }
}
