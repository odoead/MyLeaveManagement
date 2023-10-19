using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLeaveManagement.Data.Configuration;
using MyLeaveManagement.Models;

namespace MyLeaveManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LeaveHistoryConfiguration());
            base.OnModelCreating(builder);


           /* builder.Entity<LeaveHistory>().HasKey(x => new { x.RequestingEmpoyeeId, x.ApprovedById });
            builder.Entity<LeaveHistory>()
                .HasOne(x=>x.RequestingEmployee)
                .WithMany(z=> z.RequestingEmployee)*/
        }

        public DbSet <Employee> Employees { get; set; }
        public DbSet<LeaveHistory> leaveHistories { get; set; }
        public DbSet<LeaveType>leaveTypes  { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<MyLeaveManagement.Models.LeaveTypeViewModel>? DetailsTypeViewModel { get; set; }
    }
}