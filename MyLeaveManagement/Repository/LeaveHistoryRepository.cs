using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;

namespace MyLeaveManagement.Repository
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly ApplicationDbContext db;

        public LeaveHistoryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool Create(LeaveHistory entity)
        {
            db.leaveHistories.Add(entity);
            return save();
        }

        public bool delete(LeaveHistory entity)
        {
            db.leaveHistories.Remove(entity);
            return save();
        }

        public LeaveHistory findByID(int id)
        {
            return db.leaveHistories.Find();
        }

        public ICollection<LeaveHistory> GetAll()
        {
            return db.leaveHistories.ToList();
        }

        public ICollection<LeaveHistory> GetEmloyeeByLeaveHistory(int id)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            var IsChanged = db.SaveChanges();
            return IsChanged > 0;

        }

        public bool update(LeaveHistory entity)
        {
            db.Update(entity);
            return save();
        }
    }
}
