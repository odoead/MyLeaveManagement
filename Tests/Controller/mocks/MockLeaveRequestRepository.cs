using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tests.mocks
{
    internal class MockLeaveRequestRepository
    {
        public static Mock<ILeaveRequestRepository> GetMock()
        {
            var mock = new Mock<ILeaveRequestRepository>();

            var requests = new List<LeaveRequest>()
            {
                new LeaveRequest()
                {
                    Id = 1,
                    RequestingEmployee = new Employee()
                    {
                        Id="ba9581c0",
                        DateJoined=Convert.ToDateTime("02.12.2023 1:24:32"),
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
                    /*DateRequested=DateTime.ParseExact("02.12.2023 01:40:58","MM.dd.yyyy HH:mm:ss",CultureInfo.InvariantCulture),*/
                  /*DateRequested=DateTime.ParseExact("02.13.2023 1:40:58","MM.dd.yyyy HH:mm:ss", new CultureInfo("uk-UA")),*/
                  DateRequested=Convert.ToDateTime("02.12.2023 01:40:58"),
                    DateProvided=Convert.ToDateTime("03.12.2023 1:41:59"),
                    IsApproved=true,
                    EndDate=Convert.ToDateTime("05.12.2023 1:40:11"),
                    StartDate=Convert.ToDateTime("07.12.2023 1:40:17"),
                    LeaveType=new LeaveType()
                    {
                        DateCreated=Convert.ToDateTime("02.12.2023 1:32:27"),
                        DefaultDays=10,
                        Id=1,
                        Name="sick leave"
                    },

                },

            };
            mock.Setup(m => m.CreateAsync(It.IsAny<LeaveRequest>()))
                .ReturnsAsync(true);
            mock.Setup(m => m.DeleteAsync(It.IsAny<LeaveRequest>()))
                .ReturnsAsync( true);
            mock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int T) => requests.FirstOrDefault(a => a.Id == T));
            mock.Setup(m => m.GetAllAsync())
                .ReturnsAsync(requests);
            mock.Setup(m => m.GetRequestsByEmployeeAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => requests.Where(a => a.RequestingEmpoyeeId == id).ToList());
            mock.Setup(m => m.IsExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int T) => requests.Exists(a => a.Id == T));
            mock.Setup(m => m.SaveAsync())
                .ReturnsAsync(true);
            mock.Setup(m => m.UpdateAsync(It.IsAny<LeaveRequest>()))
                .ReturnsAsync(true);
            return mock;
        }
    }
}
