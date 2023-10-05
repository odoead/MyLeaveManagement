using MyLeaveManagement.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyLeaveManagement.Models
{
    public class LeaveHistoryViewModel
    {
        public int Id { get; set; }

        public EmployeeViewModel RequestingEmployee { get; set; }
        public string RequestingEmpoyeeId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public DetailsTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateProvided { get; set; }
        public bool? IsApproved { get; set; }

        public EmployeeViewModel ApprovedBy { get; set; }
        public string ApprovedById { get; set; }

    }
}
