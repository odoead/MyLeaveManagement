using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tests.mocks
{
    internal class MockLeaveAllocationRepository
    {

        public static Mock<ILeaveAllocationRepository> GetMock()
        {
            var mock = new Mock<ILeaveAllocationRepository>();

            var allocations = new List<LeaveAllocation>()
            {
                new LeaveAllocation()
                {
                    Id = 1,
                    DateCreated =Convert.ToDateTime("02.12.2023 1:24:32"),
                    NumberOfDays= 20,
                    EmployeeId= "a3f",
                    Period=2023,
                    LeaveTypeId=1,
                    Employee= new Employee()
                    {
                        Id="a3f",
                        DateJoined=Convert.ToDateTime("02.12.2023 1:32:57"),
                        Email="request@gmail.com",
                        UserName="request@gmail.com",
                        FirstName="firstrequest",
                        LastName="lastrequest"},
                    LeaveType=new LeaveType()
                    {
                        DateCreated=Convert.ToDateTime("02.12.2023 1:32:57"),
                        DefaultDays=10,
                        Id=1,
                        Name="sick leave"
                    },
                }
            };
            var EmptyAllocations = new List<LeaveAllocation>()
            {
                
            };

            mock.Setup(m => m.CheckAllocationAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync((int leavetypeid, string emloyeeid) => EmptyAllocations.Exists(q => q.EmployeeId == emloyeeid
            && q.LeaveTypeId == leavetypeid ));
            mock.Setup(m => m.CreateAsync(It.IsAny<LeaveAllocation>()))
                .ReturnsAsync(true);
            mock.Setup(m => m.DeleteAsync(It.IsAny<LeaveAllocation>()))
                .ReturnsAsync(true);
            mock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
               .ReturnsAsync((int T) => allocations.FirstOrDefault(a => a.Id == T));
            mock.Setup(m => m.GetAllAsync())
                .ReturnsAsync(allocations);


            mock.Setup(m => m.GetLeaveAllocationsByEmloyeeAsync(It.IsAny<string>()))
               .ReturnsAsync((string id) => allocations.Where(a => a.EmployeeId == id).ToList());
            mock.Setup(m => m.GetLeaveAllocationByEmloyeeAndTypeAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync((string id, int type) => allocations.FirstOrDefault(a => a.EmployeeId == id && a.LeaveType.Id == type));
            mock.Setup(m => m.IsExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int T) => allocations.Exists(a => a.Id == T));
            mock.Setup(m => m.SaveAsync())
                .ReturnsAsync(true);
            mock.Setup(m => m.UpdateAsync(It.IsAny<LeaveAllocation>()))
               .ReturnsAsync(true);

            return mock;
        }

    }
}
