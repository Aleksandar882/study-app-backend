using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyApp.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public int CourseId { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }

        public int Order { get; set; }

    }
}
