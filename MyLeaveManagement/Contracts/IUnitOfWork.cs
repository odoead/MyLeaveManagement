using MyLeaveManagement.Data;

namespace MyLeaveManagement.Contracts
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepositoryBase<LeaveType> LeaveTypes { get; }
        IGenericRepositoryBase<LeaveRequest> LeaveRequests { get; }
        IGenericRepositoryBase<LeaveAllocation> LeaveAllocations { get; }

        Task saveAsync();
    }
}
