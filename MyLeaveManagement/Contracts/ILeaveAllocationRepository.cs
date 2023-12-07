using MyLeaveManagement.Data;

namespace MyLeaveManagement.Contracts
{
    public interface ILeaveAllocationRepository: IReposBase<LeaveAllocation>
    {
        Task<bool> CheckAllocationAsync(int leavetypeid, string emloyeeid);
        Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmloyeeAsync(string emloyeeid);
        Task<LeaveAllocation> GetLeaveAllocationByEmloyeeAndTypeAsync(string emloyeeid,int leaveTypeId);

    }
}
