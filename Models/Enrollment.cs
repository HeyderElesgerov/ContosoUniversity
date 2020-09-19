using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int ID { get; set; }
        public int StudentId{ get; set; }
        public int CourseId{ get; set; }

        [DisplayFormat(NullDisplayText = "No Grade")]
        public Grade? Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
