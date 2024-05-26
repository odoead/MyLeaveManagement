namespace MyLeaveManagement.Models
{
    public class LeaveAllocationViewModel
    {
        public int id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }

    public class CreateLeaveAllocationViewModel
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeViewModel>? LeaveTypes { get; set; }
    }

    public class EditLeaveAllocationViewModel
    {
        public int id { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public string EmployeeId { get; set; }
        public int NumberOfDays { get; set; }
    }

    public class ViewAllocationsViewModel
    {
        public EmployeeViewModel employeeViewModel { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationViewModel> leaveAllocationViewModel { get; set; }
    }
}
