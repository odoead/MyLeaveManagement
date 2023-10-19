﻿using MyLeaveManagement.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

    }
}
