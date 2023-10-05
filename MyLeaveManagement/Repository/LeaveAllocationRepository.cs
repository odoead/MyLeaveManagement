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
        public bool Create(LeaveAllocation entity)
        {
            db.LeaveAllocations.Add(entity);
            return save();
        }

        public bool delete(LeaveAllocation entity)
        {
            db.LeaveAllocations.Remove(entity);
            return save();
        }

        public LeaveAllocation findByID(int id)
        {
            return db.LeaveAllocations.Find();
        }

        public ICollection<LeaveAllocation> GetAll()
        {
            return db.LeaveAllocations.ToList();
        }

        public ICollection<LeaveAllocation> GetEmloyeeByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            var IsChanged = db.SaveChanges();
            return IsChanged > 0;

        }

        public bool update(LeaveAllocation entity)
        {
            db.Update(entity);
            return save();
        }
    }
}
