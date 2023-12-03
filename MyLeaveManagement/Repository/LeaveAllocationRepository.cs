using Microsoft.EntityFrameworkCore;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace MyLeaveManagement.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CheckAllocationAsync(int leavetypeid, string emloyeeid)
        {
            var period = DateTime.Now.Year;
            var a = await GetAllAsync();
            return a.Where(q => q.EmployeeId == emloyeeid
            && q.LeaveTypeId == leavetypeid && q.Period == period).Any();
        }

        public async Task<bool> CreateAsync(LeaveAllocation entity)
        {
            await db.LeaveAllocations.AddAsync(entity);
            return await saveAsync();
        }

        public async Task<bool> deleteAsync(LeaveAllocation entity)
        {
            db.LeaveAllocations.Remove(entity);
            return await saveAsync();
        }

        public async Task<LeaveAllocation> findByIDAsync(int id)
        {
            return await db.LeaveAllocations.Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q=>q.id==id);
        }

        public async Task<ICollection<LeaveAllocation>> GetAllAsync()
        {
            return await db.LeaveAllocations.Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .ToListAsync();
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmloyeeAsync(string emloyeeid)
        {
            var period = DateTime.Now.Year;
            var a = await GetAllAsync();
            return  a.Where(q => q.EmployeeId == emloyeeid && q.Period == period).ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationsByEmloyeeAndTypeAsync(string emloyeeid, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            var allocations = await GetAllAsync();
            return allocations.FirstOrDefault(q => q.EmployeeId == emloyeeid
            && q.Period == period && q.LeaveTypeId == leaveTypeId);

        }

        public async Task<bool> isExistsAsync(int id)
        {
            return await db.LeaveAllocations.AnyAsync(q => q.id == id);
        }

        public async Task<bool> saveAsync()
        {
            var IsChanged = await db.SaveChangesAsync();
            return IsChanged > 0;

        }

        public async Task<bool> updateAsync(LeaveAllocation entity)
        {
            //db.LeaveAllocations.Update(entity);
            db.Update(entity);
            return await saveAsync();
        }
    }
}
