using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreateDt { get; set; } = DateTime.Now;
        public DateTime UpdateDt { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        [Required]
        public Guid InstructorId { get; set; }
        public User Instructor { get; set; }
        public string CourseImageUrl { get; set; }
        public string Tags { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
    }
}
