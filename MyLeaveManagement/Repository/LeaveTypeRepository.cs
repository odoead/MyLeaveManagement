using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using System.Data.Entity;

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
            return await saveAsync();
        }

        public async Task<bool> deleteAsync(LeaveType entity)
        {
            db.LeaveTypes.Remove(entity);
            return await saveAsync();
        }

        public async Task<LeaveType> findByIDAsync(int id)
        {
            return await db.LeaveTypes.FindAsync(id);
        }

        public async Task<ICollection<LeaveType>> GetAllAsync()
        {
            return db.LeaveTypes.ToList();
        }

        public Task<ICollection<LeaveType>> GetEmloyeeByLeaveTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> isExistsAsync(int id)
        {
            var exists = await db.LeaveTypes.AnyAsync(a => a.Id == id);
            return exists;
        }



        public async Task<bool> saveAsync()
        {
            var IsChanged =await db.SaveChangesAsync();
            return IsChanged > 0;

        }

        public async Task<bool> updateAsync(LeaveType entity)
        {
            db.Update(entity);
            return await saveAsync();
        }
    }
}
