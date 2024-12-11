using System.ComponentModel.DataAnnotations;

namespace iLearn.Data.Models
{
    public class Lecture
    {
        [Key]
        public Guid LectureId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string ContentUrl { get; set; }
        public DateTime CreateDt { get; set; } = DateTime.Now;
        [Required]
        public Guid CourseId { get; set; }

    }
}
