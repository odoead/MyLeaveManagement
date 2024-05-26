using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyLeaveManagement.Data.Configuration
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(lh => lh.Id);
            builder
                .HasOne(lh => lh.RequestingEmployee)
                .WithMany()
                .HasForeignKey(lh => lh.RequestingEmpoyeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();
            builder.Property(lh => lh.StartDate).IsRequired();
            builder.Property(lh => lh.EndDate).IsRequired();
            builder
                .HasOne(lh => lh.LeaveType)
                .WithMany()
                .HasForeignKey(lh => lh.LeaveTypeId)
                .IsRequired();
            builder.Property(lh => lh.DateRequested).IsRequired();
            builder.Property(lh => lh.DateProvided).IsRequired();
            builder.Property(lh => lh.IsApproved);
            builder
                .HasOne(lh => lh.ApprovedBy)
                .WithMany()
                .HasForeignKey(lh => lh.ApprovedById)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
