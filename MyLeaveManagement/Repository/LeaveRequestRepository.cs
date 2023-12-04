using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using System.Data.Entity;

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
            return await saveAsync();
        }

        public async Task<bool> deleteAsync(LeaveRequest entity)
        {
            db.LeaveRequests.Remove(entity);
            return await saveAsync();
        }

        public async Task<LeaveRequest> findByIDAsync(int id)
        {
            return await db.LeaveRequests
                .Include(q => q.RequestingEmployee).
                Include(q => q.ApprovedById).Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<LeaveRequest>> GetAllAsync()
        {
            return await db.LeaveRequests
                .Include(q => q.RequestingEmployee).Include(q => q.ApprovedById).Include(q => q.LeaveType)
                .ToListAsync();
        }



        public async Task<ICollection<LeaveRequest>> GetRequestsByEmployeeAsync(string employeeId)
        {
            var request = await GetAllAsync();

            return request.Where(q => q.RequestingEmpoyeeId == employeeId).ToList(); ;
        }

        public async Task<bool> isExistsAsync(int id)
        {
            var exists = await db.LeaveRequests.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> saveAsync()
        {
            var IsChanged = await db.SaveChangesAsync();
            return IsChanged > 0;

        }
        public async Task<bool> updateAsync(LeaveRequest entity)
        {
            db.Update(entity);
            return await saveAsync();
        }
    }
}
