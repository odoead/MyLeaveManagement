using System.ComponentModel.DataAnnotations;

namespace MyLeaveManagement.Models
{
    public class LeaveTypeViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }






    }
    
}

