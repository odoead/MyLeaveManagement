using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeaveManagement.Data
{
    public class LeaveRequest
    {


        [Key]
        public int Id { get; set; }

        [ForeignKey("RequestingEmpoyeeId")]
        public Employee RequestingEmployee { get; set; }
        public string RequestingEmpoyeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateProvided { get; set; }
        public bool? IsApproved { get; set; }
        [ForeignKey("ApprovedById")]
        public Employee ApprovedBy { get; set; }
        public string ApprovedById { get; set; }




    }
}
