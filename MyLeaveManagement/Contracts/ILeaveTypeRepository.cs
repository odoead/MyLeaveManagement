using MyLeaveManagement.Data;

namespace MyLeaveManagement.Contracts
{
    public interface ILeaveTypeRepository : IReposBase<LeaveType>
    {
        Task<ICollection<LeaveType>> GetEmloyeeByLeaveTypeAsync(int id);
    }
}
