using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {
        public int InstructorId { get; set; }

        [Required]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }
    }
}
