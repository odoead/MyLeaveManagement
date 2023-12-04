using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace MyLeaveManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            Context=context;
        }
        private ApplicationDbContext Context { get; set; }

        private IGenericRepositoryBase<LeaveType> leaveTypes;


        private IGenericRepositoryBase<LeaveRequest> leaveRequests;


        private IGenericRepositoryBase<LeaveAllocation> leaveAllocations;
        public IGenericRepositoryBase<LeaveType> LeaveTypes 
            => leaveTypes ?? new GenericRepository<LeaveType>(Context);

        public IGenericRepositoryBase<LeaveRequest> LeaveRequests 
            => leaveRequests ?? new GenericRepository<LeaveRequest>(Context);

        public IGenericRepositoryBase<LeaveAllocation> LeaveAllocations 
            => leaveAllocations ??new GenericRepository<LeaveAllocation>(Context);

        /*public void Dispose(bool dispose)
        {
            if(dispose==true)
            {
                Context.Dispose();
            }
            GC.SuppressFinalize(this);
        }*/

        public async Task saveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                Context.Dispose();
            }
        }
    }
}
