using Microsoft.EntityFrameworkCore;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace MyLeaveManagement.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateAsync(LeaveRequest entity)
        {
            await db.LeaveRequests.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(LeaveRequest entity)
        {
            db.LeaveRequests.Remove(entity);
            return await SaveAsync();
        }

        public async Task<LeaveRequest> FindByIdAsync(int id)
        {
            var leaveRequest = await db
                .LeaveRequests.Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveRequest;
        }

        public async Task<ICollection<LeaveRequest>> GetAllAsync()
        {
            var leaveRequests = await db
                .LeaveRequests.Include(q => q.RequestingEmployee)
                .Include(q => q.ApprovedBy)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<ICollection<LeaveRequest>> GetRequestsByEmployeeAsync(string employeeId)
        {
            var request = await GetAllAsync();
            return request.Where(q => q.RequestingEmpoyeeId == employeeId).ToList();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            var exists = await db.LeaveRequests.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> SaveAsync()
        {
            var IsChanged = await db.SaveChangesAsync();
            return IsChanged > 0;
        }

        public async Task<bool> UpdateAsync(LeaveRequest entity)
        {
            db.Update(entity);
            return await SaveAsync();
        }
    }
}
