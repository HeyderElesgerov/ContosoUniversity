using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
