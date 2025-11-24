using System.ComponentModel.DataAnnotations;

namespace StudyApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<Lesson> Lessons { get; set; } = new();
    }
}
