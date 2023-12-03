﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLeaveManagement.Data
{
    public class LeaveAllocation
    {
        

        [Key]
        public int id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period {  get; set; }
    }
}
