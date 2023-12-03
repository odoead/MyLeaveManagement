using MyLeaveManagement.Data;

namespace MyLeaveManagement.Contracts
{
    public interface ILeaveRequestRepository: IReposBase<LeaveRequest>
    {
        Task<ICollection<LeaveRequest>> GetRequestsByEmployeeAsync(string employeeId);
    }
}
