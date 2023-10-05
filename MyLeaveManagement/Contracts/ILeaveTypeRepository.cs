using MyLeaveManagement.Data;

namespace MyLeaveManagement.Contracts
{
    public interface ILeaveTypeRepository: IReposBase<LeaveType>
    {
        ICollection<LeaveType> GetEmloyeeByLeaveType(int id);
    }
}
