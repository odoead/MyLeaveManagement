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
        public bool Create(LeaveType entity)
        {
            db.leaveTypes.Add(entity);
            return save();
        }

        public bool delete(LeaveType entity)
        {
            db.leaveTypes.Remove(entity);
            return save();
        }

        public LeaveType findByID(int id)
        {
            return db.leaveTypes.Find();
        }

        public ICollection<LeaveType> GetAll()
        {
            return db.leaveTypes.ToList();
        }

        public ICollection<LeaveType> GetEmloyeeByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            var IsChanged= db.SaveChanges();
            return IsChanged>0;

        }

        public bool update(LeaveType entity)
        {
            db.Update(entity);
            return save();
        }
    }
}
