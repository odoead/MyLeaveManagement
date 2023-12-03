using System.ComponentModel.DataAnnotations;

namespace MyLeaveManagement.Data
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
