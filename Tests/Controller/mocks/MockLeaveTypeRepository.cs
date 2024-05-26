using Moq;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace Tests.mocks
{
    internal class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMock()
        {
            var mock = new Mock<ILeaveTypeRepository>();
            var _LeaveTypes = new List<LeaveType>()
            {
                new LeaveType()
                {
                    DateCreated = Convert.ToDateTime("02.12.2023 1:32:57"),
                    DefaultDays = 10,
                    Id = 1,
                    Name = "sick leave"
                }
            };
            mock.Setup(m => m.GetAllAsync()).ReturnsAsync(_LeaveTypes);
            mock.Setup(m => m.CreateAsync(It.IsAny<LeaveType>())).ReturnsAsync(true);
            mock.Setup(m => m.DeleteAsync(It.IsAny<LeaveType>())).ReturnsAsync(true);
            mock.Setup(m => m.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int T) => _LeaveTypes.FirstOrDefault(a => a.Id == T));
            mock.Setup(m => m.IsExistsAsync(It.IsAny<int>()))
                .ReturnsAsync((int T) => _LeaveTypes.Exists(a => a.Id == T));
            mock.Setup(m => m.SaveAsync()).ReturnsAsync(true);
            mock.Setup(m => m.UpdateAsync(It.IsAny<LeaveType>())).ReturnsAsync(true);
            return mock;
        }
    }
}
