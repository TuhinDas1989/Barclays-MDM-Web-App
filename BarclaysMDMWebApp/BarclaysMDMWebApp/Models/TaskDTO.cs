using System.ComponentModel.DataAnnotations;

namespace BarclaysMDMWebApp.Models
{
    public class TaskDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task Name is required.")]
        public string? Name { get; set; }

        [Range(1, 10, ErrorMessage = "Task Priority must be between 1 and 10.")]
        public int Priority { get; set; }

        [Required]
        public string Status { get; set; } = "Not Started";
    }
}
