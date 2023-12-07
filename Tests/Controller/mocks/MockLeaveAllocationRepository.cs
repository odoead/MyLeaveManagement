using Moq;
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

        public static Mock<LeaveAllocationRepository> GetMock()
        {
            var mock = new Mock<LeaveAllocationRepository>();

            var allocations = new List<LeaveAllocation>()
            {
                new LeaveAllocation()
                {
                    Id = 1,
                    DateCreated =Convert.ToDateTime( "02.12.2023 1:24:32"),
                    NumberOfDays= 20,
                    EmployeeId= "a3fb221e-c2b8-4e64-9d78-0a0427822ad7",
                    Period=2023,
                    LeaveTypeId=1,
                    Employee= new Employee()
                    {
                        Id="ba9581c0",
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

            mock.Setup(m => m.CheckAllocationAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.CreateAsync(It.IsAny<LeaveAllocation>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.DeleteAsync(It.IsAny<LeaveAllocation>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
               .Returns((LeaveAllocation Alloc) => allocations.FirstOrDefault(a => a.Id == Alloc.Id));
            mock.Setup(m => m.GetAllAsync())
                .Returns((ICollection<LeaveAllocation> allocs) => allocs == allocations);
            mock.Setup(m => m.GetLeaveAllocationsByEmloyeeAsync(It.IsAny<string>()))
               .Returns((string id) => allocations.Where(a => a.Employee.Id == id).ToList());
            mock.Setup(m => m.GetLeaveAllocationsByEmloyeeAndTypeAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns((string id, int type) => allocations.Where(a => a.Employee.Id == id && a.LeaveType.Id == type).ToList());
            mock.Setup(m => m.IsExistsAsync(It.IsAny<int>()))
                .Returns((bool q) => q == true);
            mock.Setup(m => m.SaveAsync())
                .Returns((bool q) => q == true);
            mock.Setup(m => m.UpdateAsync(It.IsAny<LeaveAllocation>()))
               .Returns((bool q) => q == true);

            return mock;
        }

    }
}
