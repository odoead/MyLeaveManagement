﻿using System.ComponentModel.DataAnnotations;

namespace MyLeaveManagement.Data
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public int DefaultDays { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? TestDaysSTR { get; set; }
    }
}
