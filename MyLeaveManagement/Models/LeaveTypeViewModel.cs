using System.ComponentModel.DataAnnotations;

namespace MyLeaveManagement.Models
{
    public class DetailsTypeViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }






    }
    public class CreateTypeViewModel
    {
        [Required]
        public string Name { get; set; }

    }
}

