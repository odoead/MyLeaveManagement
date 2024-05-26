using Microsoft.EntityFrameworkCore;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace MyLeaveManagement.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateAsync(LeaveType entity)
        {
            await db.LeaveTypes.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(LeaveType entity)
        {
            db.LeaveTypes.Remove(entity);
            return await SaveAsync();
        }

        public async Task<LeaveType> FindByIdAsync(int id)
        {
            return await db.LeaveTypes.FindAsync(id);
        }

        public async Task<ICollection<LeaveType>> GetAllAsync()
        {
            return await db.LeaveTypes.ToListAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            var exists = await db.LeaveTypes.AnyAsync(a => a.Id == id);
            return exists;
        }

        public async Task<bool> SaveAsync()
        {
            var IsChanged = await db.SaveChangesAsync();
            return IsChanged > 0;
        }

        public async Task<bool> UpdateAsync(LeaveType entity)
        {
            db.Update(entity);
            return await SaveAsync();
        }
    }
}
